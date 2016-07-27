using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Utils;
using Random = System.Random;

public class CreateWallMonster : Monster
{
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
        Vector2i playerPos = Player.pos.GetVector2i();
        List<Vector2i> wallCreatePositions = new List<Vector2i>()
        {
                                  new Vector2i(0, 1),
            new Vector2i(-1, 0),                       new Vector2i(1, 0),
                                  new Vector2i(0, -1),
        };
        wallCreatePositions = wallCreatePositions
            .OrderBy(x => random.Next())
            .Take(3)
            .Select(v => playerPos + v)
            .ToList();

        foreach (var wallPos in wallCreatePositions)
        {
            if (GetTileType(wallPos.x, wallPos.y) != TileType.None)
            {
                SetTileType(wallPos.x, wallPos.y, TileType.Wall);
                SetTileColor(wallPos.x, wallPos.y, monstersColor);

                Monster monsterOnWall = CheckMonsterPosition(wallPos.x, wallPos.y);
                if (monsterOnWall != null)
                {
                    monsterOnWall.Health -= 1;
                    monsterOnWall.CheckHealth();
                }
            }
        }
    }

    protected override void WhenDestroyed()
    {
        GameStateManager.Instance.MonsterDied -= OnMonsterDied;
    }
}