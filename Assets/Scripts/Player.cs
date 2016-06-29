using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private TileManager tileManager;

    private Animator animator;
    private MobileInputManager mobileInputManager;

    public SoundManager soundManager;

    public new Camera camera;

    public Position pos;
    public Vector2i prevPos;
    public Vector2i tempPos;

    public TileColor playerTileColor;
    public int MaxHealth;
    public int Health;
    public int Blinkable;
    public int Difficulty;
    
    public ParticleSystem auraParticle;
    public ParticleSystem damagedParticle;
    
    public GameObject[] arrowObjects;
    
    protected void Awake()
    {
        pos = GetComponent<Position>();
        animator = GetComponent<Animator>();
        mobileInputManager = FindObjectOfType<MobileInputManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    protected void Start()
    {
        tileManager = TileManager.Instance;
        if (playerTileColor != TileColor.None)
            tileManager.SetTileColor(pos.X, pos.Y, playerTileColor);
        UpdateAuraColor();
        damagedParticle.gameObject.SetActive(false);
        GameStateManager.Instance.PlayerTurn += () => OnTurn(tempPos.x, tempPos.y);
        
        ArrowInactive();
    }
    
    public void Restart()
    {
        Health = MaxHealth;
        animator.SetBool("IsDead", false);
        playerTileColor = TileColor.None;
        UpdateAuraColor();    
        damagedParticle.gameObject.SetActive(false);
        
        ArrowInactive();
    }

    void ArrowActive()
    {
        foreach (var arrowObject in arrowObjects)
        {
            int arrowPosX = (int)arrowObject.transform.position.x;
            int arrowPosY = (int)arrowObject.transform.position.y;
            
            if (tileManager.GetTileType(arrowPosX, arrowPosY) == TileType.Normal)
                arrowObject.SetActive(true);
        }
    }
    
    void ArrowInactive()
    {
        foreach (var arrowObject in arrowObjects)
        {
            arrowObject.SetActive(false);
        }
    }

    public void CameraUpdate()
    {
        // Move camera position to player
        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
    }
    public void GameUpdate()
    {
        tempPos.x = pos.X; tempPos.y = pos.Y;
        if ((Input.GetKey(KeyCode.Space) || GetBlinkButtonState()) && (Blinkable == 0))
        {   
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {                
                TurnLeft();
                tempPos.x = tempPos.x - 3;
                if (!(PositionCheck()))
                    tempPos.x = tempPos.x + 3;
                else
                	Blinkable = Difficulty;
                //else
                //    soundManager.Play(SoundManager.Sounds.Blink);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                TurnRight();
                tempPos.x = tempPos.x + 3;
                if (!(PositionCheck()))
                    tempPos.x = tempPos.x - 3;
                else
                	Blinkable = Difficulty;
                //else
                //    soundManager.Play(SoundManager.Sounds.Blink);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tempPos.y = tempPos.y + 3;
                if (!(PositionCheck()))
                    tempPos.y = tempPos.y - 3;
                else
                	Blinkable = Difficulty;
                //else
                //    soundManager.Play(SoundManager.Sounds.Blink);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                tempPos.y = tempPos.y - 3;
                if (!(PositionCheck()))
                    tempPos.y = tempPos.y + 3;
                else
                	Blinkable = Difficulty;
                //else
                //    soundManager.Play(SoundManager.Sounds.Blink);
            }
            
        }
        else
        {
            ArrowInactive();
            if (!Input.GetKey(KeyCode.Space)){
	            if (Input.GetKeyDown(KeyCode.LeftArrow) || GetDirectionSetBySwipe() == "Left")
	            {
	                TurnLeft();
	                tempPos.x--;
	                if (!(PositionCheck())) tempPos.x++;
	                else soundManager.Play(SoundManager.Sounds.Move1);
	            }
	            else if (Input.GetKeyDown(KeyCode.RightArrow) || GetDirectionSetBySwipe() == "Right")
	            {
	                TurnRight();
	                tempPos.x++;
	                if (!(PositionCheck())) tempPos.x--;
	                else soundManager.Play(SoundManager.Sounds.Move1);
	            }
	            else if (Input.GetKeyDown(KeyCode.UpArrow) || GetDirectionSetBySwipe() == "Up")
	            {
	                tempPos.y++;
	                if (!(PositionCheck())) tempPos.y--;
	                else soundManager.Play(SoundManager.Sounds.Move1);
	            }
	            else if (Input.GetKeyDown(KeyCode.DownArrow) || GetDirectionSetBySwipe() == "Down")
	            {
	                tempPos.y--;
	                if (!(PositionCheck())) tempPos.y++;
	                else soundManager.Play(SoundManager.Sounds.Move1);
	            }
            }            
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && (Blinkable == 0))
        {
            ArrowActive();
        }
        
        if ((Input.GetKeyUp(KeyCode.Space)) ||
            (Input.GetKeyDown(KeyCode.LeftArrow)) ||
            (Input.GetKeyDown(KeyCode.RightArrow)) ||
            (Input.GetKeyDown(KeyCode.UpArrow)) ||
            (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            ArrowInactive();
        }

        if (tempPos.x != pos.X || tempPos.y != pos.Y)
        {
            // if moved, next turn
            GameStateManager.Instance.NextTurn();
        }

        
    }

    public Sequence OnTurn(int x, int y)
    {
        Sequence sequence = DOTween.Sequence();

        // Store the previous location
        prevPos = pos.GetVector2i();

        if (tileManager.GetTileData(x, y).color == TileColor.None ||
            tileManager.GetTileData(x, y).type == TileType.Wall)
            return sequence;


        Monster foundMonster = GameStateManager.Instance.CheckMonsterPosition(x, y);
        if (foundMonster != null)
        {
            ApplyDamage(foundMonster.DamageToPlayer);
            foundMonster.moveCancelled = true;
            sequence.Append(pos.AnimatedMove(x, y, 0.2f).OnPlay(() =>
            {
                animator.SetTrigger("Hit");
                StartCoroutine(PlayDamageEffect());
            }));
            sequence.Append(pos.AnimatedMove(prevPos.x, prevPos.y, 0.2f));
            return sequence;
        }

        sequence.Append(pos.AnimatedMove(x, y, 0.2f).OnPlay(() =>
        {
            animator.SetTrigger("Move");
        }));
                
        // Consume the orb after the player paints the current color
        Orb foundOrb = GameStateManager.Instance.CheckOrbPosition(x, y);
        if (foundOrb != null)
        {
            if (playerTileColor != foundOrb.Color)
            {
                if (foundOrb.Color == TileColor.Red)
                {
                    soundManager.PlayBackground(SoundManager.Sounds.Red);
                }
                else if (foundOrb.Color == TileColor.Blue)
                {
                    soundManager.PlayBackground(SoundManager.Sounds.Blue);
                }
                else if (foundOrb.Color == TileColor.Yellow)
                {
                    soundManager.PlayBackground(SoundManager.Sounds.Yellow);
                }
            }
            playerTileColor = foundOrb.Color;
            UpdateAuraColor();
        }
        
        if ((playerTileColor != TileColor.None) && (foundOrb == null))
        {
            bool fillingExecuted = tileManager.SetTileColorAndFill(x, y, playerTileColor);
            if (fillingExecuted) soundManager.Play(SoundManager.Sounds.TileActivate);
            
            tileManager.GetTile(x, y).PlaySubEffect();
        }

        if (Blinkable != 0) Blinkable -= 1;

        return sequence;
    } // end of OnTurn

    public void ApplyDamage(int damage)
    {
        if (Health > 0) Health -= damage;
        soundManager.Play(SoundManager.Sounds.Hit);
    }

    public void RevertTurn()
    {
        pos.X = prevPos.x;
        pos.Y = prevPos.y;
    }

    private bool PositionCheck()
    {
        if (TileManager.Instance.GetTileType(tempPos.x, tempPos.y) == TileType.Wall || TileManager.Instance.GetTileType(tempPos.x, tempPos.y) == TileType.None)
            return false;
        else return true;
    }

    bool GetBlinkButtonState()
    {
        if (mobileInputManager == null)
            return false;
        
        return mobileInputManager.isBlinkButtonClicked;
    }

    string GetDirectionSetBySwipe()
    {
        if (mobileInputManager == null)
            return "";
        
        return mobileInputManager.destDirection;
    }
    
    public void UpdateAuraColor()
    {
        if (playerTileColor == TileColor.None || Health <= 0)
            auraParticle.startColor = new Color(0, 0, 0, 0);
        else if (playerTileColor == TileColor.Black)
            auraParticle.startColor = new Color(0, 0, 0, 1);
        else if (playerTileColor == TileColor.White)
            auraParticle.startColor = new Color(1, 1, 1, 1);
        else if (playerTileColor == TileColor.Red)
            auraParticle.startColor = new Color(249f/255f, 123f/255f, 188f/255f, 1);
        else if (playerTileColor == TileColor.Blue)
            auraParticle.startColor = new Color(84f/255f, 202f/255f, 249f/255f, 1);
        else if (playerTileColor == TileColor.Yellow)
            auraParticle.startColor = new Color(253f/255f, 249f/255f, 87f/255f, 1);
    }
    
    void TurnLeft()
    {
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    }
    
    void TurnRight()
    {
        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }
    
    IEnumerator PlayDamageEffect()
    {
        damagedParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        damagedParticle.gameObject.SetActive(false);
    }
}
