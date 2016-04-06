using UnityEngine;
using System.Collections;
using FullInspector;

[RequireComponent(typeof(Position))]
public class Player : BaseBehavior
{
    private TileManager tileManager;

    [SerializeField] private Camera camera;

    [HideInInspector]
    public Position pos;

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
        int prevPosX = pos.X, prevPosY = pos.Y;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) pos.X--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) pos.X++;
        if (Input.GetKeyDown(KeyCode.UpArrow)) pos.Y++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) pos.Y--;
        if (pos.X == 0 || pos.X == tileManager.width - 1 || pos.Y == 0 || pos.Y == tileManager.height - 1)
        {
            pos.X = prevPosX;
            pos.Y = prevPosY;
        }

        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
    }
}
