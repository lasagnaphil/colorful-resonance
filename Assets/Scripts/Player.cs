using UnityEngine;
using System.Collections;

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
        tileManager.SetTileData(pos.X, pos.Y, playerTileColor);
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
        /*
        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
        */
    }

    public bool OnTurn(int x, int y)
    {
        // Store the previous location
        prevPos = pos.GetVector2i();

        if (tileManager.GetTileType(x, y).color == TileColor.None ||
            tileManager.GetTileType(x, y).type == TileType.Wall)
            return false;

        Monster foundMonster = GameStateManager.Instance.CheckMonsterPosition(x, y);
        if (foundMonster != null)
        {
            ApplyDamage(foundMonster.DamageToPlayer);
            return false;
        }

        Orb foundOrb = GameStateManager.Instance.CheckOrbPosition(x, y);
        if (foundOrb != null)
        {
            playerTileColor = foundOrb.Color;
            Destroy(foundOrb.gameObject);
        }

        pos.X = x;
        pos.Y = y;
        tileManager.SetTileDataAndFill(x, y, playerTileColor);
        return true;
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
