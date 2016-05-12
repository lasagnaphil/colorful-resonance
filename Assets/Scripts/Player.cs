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

    protected void Update()
    {
        tempPos.x = pos.X; tempPos.y = pos.Y;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) tempPos.x--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) tempPos.x++;
        if (Input.GetKeyDown(KeyCode.UpArrow)) tempPos.y++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) tempPos.y--;

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
            return sequence;
        }

        pos.X = x;
        pos.Y = y;
        
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
        Health -= damage;
        // TODO: if health is zero or below then die
    }

    public void RevertTurn()
    {
        pos.X = prevPos.x;
        pos.Y = prevPos.y;
    }
}
