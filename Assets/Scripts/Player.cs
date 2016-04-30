using UnityEngine;
using System.Collections;
using FullInspector;

public class Player : BaseBehavior
{
    private TileManager tileManager;

    public new Camera camera;

    public Position pos;
    public Vector2i prevPos;
    public Vector2i tempPos;

    public TileType playerTileType;
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        pos = GetComponent<Position>();
    }

    protected void Start()
    {
        tileManager = TileManager.Instance;
        tileManager.SetTileType(pos.X, pos.Y, playerTileType);
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
            // Store the previous location
            prevPos = pos.GetVector2i();
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
        if (tileManager.GetTileType(x, y) == TileType.None ||
            tileManager.GetTileType(x, y) == TileType.Black)
            return false;

        Monster foundMonster = GameStateManager.Instance.CheckMonsterPosition(x, y);
        if (foundMonster != null)
        {
            ApplyDamage(foundMonster.DamageToPlayer);
            return false;
        }

        pos.X = x;
        pos.Y = y;
        tileManager.SetTileTypeAndFill(x, y, playerTileType);
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
