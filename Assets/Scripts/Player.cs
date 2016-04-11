using UnityEngine;
using System.Collections;
using FullInspector;

public class Player : BaseBehavior
{
    private TileManager tileManager;
    public GameStateManager gameStateManager;

    public Camera camera;

    public Position pos;

    public TileType playerTileType;

    protected void Awake()
    {
        base.Awake();
        pos = GetComponent<Position>();
    }

    protected void Start()
    {
        tileManager = TileManager.Instance;
    }

    protected void Update()
    {
        int tempPosX = pos.X, tempPosY = pos.Y;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) tempPosX--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) tempPosX++;
        if (Input.GetKeyDown(KeyCode.UpArrow)) tempPosY++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) tempPosY--;
        Move(tempPosX, tempPosY);

        // Only for debugging purposes. Tests the color-filling algorithm.
        if (Input.GetKeyDown(KeyCode.G))
        {
            tileManager.FillTilesUsingContours(pos.X, pos.Y, playerTileType);
        }

        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
    }

    public bool Move(int x, int y)
    {
        if (x == 0 || x == tileManager.width - 1 || y == 0 || y == tileManager.height - 1) return false;
        tileManager.SetTileType(x, y, playerTileType);
        pos.X = x;
        pos.Y = y;
        return true;
    }
}
