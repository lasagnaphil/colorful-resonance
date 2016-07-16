using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Utils;
using Random = System.Random;

public class CreateWallMonster : Monster
{
    public TileColor wallColor;

    private Random random = new Random();

    protected override void Start()
    {
        base.Start();
        GameStateManager.Instance.MonsterDied += OnMonsterDied;
    }

    protected override void OnTurn(Sequence sequence)
    {
    }

    private void OnMonsterDied()
    {
        Vector2i playerPos = GameStateManager.Instance.player.pos.GetVector2i();
        List<Vector2i> wallCreatePositions = new List<Vector2i>()
        {
            new Vector2i(-1, 1), new Vector2i(0, 1), new Vector2i(1, 1),
            new Vector2i(-1, 0),                     new Vector2i(1, 0),
            new Vector2i(-1, -1), new Vector2i(0, -1), new Vector2i(1, -1)
        };
        wallCreatePositions = wallCreatePositions.OrderBy(x => random.Next()).Take(4).ToList();

        foreach (var wallPos in wallCreatePositions)
        {
            TileManager.Instance.SetTileType(wallPos.x, wallPos.y, TileType.Wall);
            TileManager.Instance.SetTileColor(wallPos.x, wallPos.y, wallColor);
        }
    }

    protected override void WhenDestroyed()
    {
    }
}