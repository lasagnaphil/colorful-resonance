using UnityEngine;
using System.Collections;
using FullInspector;

[RequireComponent(typeof(Position))]
public class Player : BaseBehavior
{
    [HideInInspector]
    public Position pos;

    protected void Awake()
    {
        base.Awake();
        pos = GetComponent<Position>();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) pos.X--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) pos.X++;
        if (Input.GetKeyDown(KeyCode.UpArrow)) pos.Y++;
        if (Input.GetKeyDown(KeyCode.DownArrow)) pos.Y--;
    }
}
