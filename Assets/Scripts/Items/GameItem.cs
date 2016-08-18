using System;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Position))]
    public class GameItem : GameEntity
    {
        [NonSerialized] public Position pos;
        protected SpriteRenderer renderer;

        protected void Awake()
        {
            pos = GetComponent<Position>();
            renderer = GetComponent<SpriteRenderer>();
        }

        protected void Start()
        {
            GameStateManager.Instance.items.Add(this);
            GameStateManager.Instance.ItemTurns += OnTurn;
        }

        protected virtual void OnTurn()
        {
        }

        void OnDestroy()
        {
            if (GameStateManager.Instance != null)
                GameStateManager.Instance.ItemTurns -= OnTurn;
        }
    }
}