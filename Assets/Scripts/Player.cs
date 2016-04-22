﻿using UnityEngine;
using System.Collections;
using FullInspector;

public class Player : BaseBehavior
{
    private TileManager tileManager;

    public new Camera camera;

    public Position pos;

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
    }

    protected void Update()
    {
        int tempPosX = pos.X, tempPosY = pos.Y;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) tempPosX--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) tempPosX++;
        if (Input.GetKeyDown(KeyCode.UpArrow)) tempPosY++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) tempPosY--;

        if (tempPosX != pos.X || tempPosY != pos.Y)
            Move(tempPosX, tempPosY);

        var camPos = camera.transform.position;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camPos.z);
    }

    public bool Move(int x, int y)
    {
        if (x == 0 || x == tileManager.width - 1 || y == 0 || y == tileManager.height - 1) return false;
        pos.X = x;
        pos.Y = y;
        tileManager.SetTileTypeAndUpdate(x, y, playerTileType);
        return true;
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
        // TODO: if health is zero or below then die
    }
}
