using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private TileManager tileManager;

    public new Camera camera;

    public Position pos;
    public Vector2i prevPos;
    public Vector2i tempPos;

    public TileColor playerTileColor;
    public int MaxHealth;
    public int Health;

    protected void Awake()
    {
        pos = GetComponent<Position>();
    }

    protected void Start()
    {
        tileManager = TileManager.Instance;
        if (playerTileColor != TileColor.None)
            tileManager.SetTileColor(pos.X, pos.Y, playerTileColor);
        GameStateManager.Instance.PlayerTurn += () => OnTurn(tempPos.x, tempPos.y);
    }

    public void GameUpdate()
    {
        tempPos.x = pos.X; tempPos.y = pos.Y;
        if (Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tempPos.x = tempPos.x - 3;
                if (!(PositionCheck()))
                    tempPos.x = tempPos.x + 3;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                tempPos.x = tempPos.x + 3;
                if (!(PositionCheck()))
                    tempPos.x = tempPos.x - 3;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tempPos.y = tempPos.y + 3;
                if (!(PositionCheck()))
                    tempPos.y = tempPos.y - 3;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                tempPos.y = tempPos.y - 3;
                if (!(PositionCheck()))
                    tempPos.y = tempPos.y + 3;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tempPos.x--;
                if (!(PositionCheck())) tempPos.x++;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                tempPos.x++;
                if (!(PositionCheck())) tempPos.x--;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tempPos.y++;
                if (!(PositionCheck())) tempPos.y--;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                tempPos.y--;
                if (!(PositionCheck())) tempPos.y++;
            }
        }

        if (tempPos.x != pos.X || tempPos.y != pos.Y)
        {
            // if moved, next turn
            GameStateManager.Instance.NextTurn();
        }

        // Move camera position to player
        
        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
        
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
            sequence.Append(pos.AnimatedMove(x, y, 0.2f));
            sequence.Append(pos.AnimatedMove(prevPos.x, prevPos.y, 0.2f));
            return sequence;
        }

        sequence.Append(pos.AnimatedMove(x, y, 0.2f));
        
        // Consume the orb after the player paints the current color
        Orb foundOrb = GameStateManager.Instance.CheckOrbPosition(x, y);
        if (foundOrb != null)
        {
            playerTileColor = foundOrb.Color;
        }
        
        if ((playerTileColor != TileColor.None) && (foundOrb == null))
            tileManager.SetTileColorAndFill(x, y, playerTileColor);

        return sequence;
    }

    public void ApplyDamage(int damage)
    {
        if (Health > 0) Health -= damage;
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
}
