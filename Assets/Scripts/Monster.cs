using System;
using FullInspector;
using UnityEngine;

// Base monster class. Inherit from this when creating new monsters.

[RequireComponent(typeof(Position))]
public class Monster : BaseBehavior
{
    public int MaxHealth { get; set; }
    public int Damage { get; set; }

    [ShowInInspector]
    private int Health { get; set; }

    protected Position pos;

    protected void Start()
    {
        pos = GetComponent<Position>();
        GameStateManager.Instance.AddMonster(this);
        GameStateManager.Instance.MonsterTurns += OnTurn;
        GameStateManager.Instance.MonsterResets += OnReset;
    }

    // Override this!!!
    protected virtual void OnTurn()
    {

    }

    protected virtual void OnReset()
    {
        Health = MaxHealth;
    }

    protected void OnDestroy()
    {
        GameStateManager.Instance.RemoveMonster(this);
        GameStateManager.Instance.MonsterTurns -= OnTurn;
        GameStateManager.Instance.MonsterTurns -= OnReset;
    }
}