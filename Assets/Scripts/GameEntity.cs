using System;
using Items;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Position))]
public class GameEntity : MonoBehaviour
{
    public Position pos;

    protected void Awake()
    {
        pos = GetComponent<Position>();
    }

    public virtual void Setup(object[] args) { }

    protected Player Player { get { return GameStateManager.Instance.player; } }

    protected Vector2i PlayerPos()
    {
        return GameStateManager.Instance.player.pos.GetVector2i();
    }

    protected Vector2i DiffFromPlayer()
    {
        Vector2i playerPos = GameStateManager.Instance.player.pos.GetVector2i();
        return playerPos - pos.GetVector2i();
    }

    protected Direction DirectionFromPlayer()
    {
        return DirectionHelper.ToDirectionX(DiffFromPlayer());
    }

    protected bool CheckTile(int x, int y, Predicate<TileData> predicate)
    {
        return predicate(TileManager.Instance.GetTileData(x, y));
    }

    protected bool CheckTile(Vector2i pos, Predicate<TileData> predicate)
    {
        return predicate(TileManager.Instance.GetTileData(pos.x, pos.y));
    }

    protected bool CheckTileIsNormal(int x, int y)
    {
        return CheckTile(x, y, tile => tile.type == TileType.Normal);
    }

    protected bool CheckTileIsNormal(Vector2i vec)
    {
        return CheckTileIsNormal(vec.x, vec.y);
    }

    protected Monster SpawnMonster(Monster monster, int x, int y)
    {
        return GameStateManager.Instance.SpawnMonster(monster, x, y);
    }

    protected Monster SpawnMonster(Monster monster, Vector2i vec)
    {
        return GameStateManager.Instance.SpawnMonster(monster, vec.x, vec.y);
    }

    protected Monster SpawnMonster(string name, Vector2i vec, params object[] args)
    {
        Monster monster = SpawnMonster(name, vec);
        monster.Setup(args);
        return monster;
    }

    protected Monster SpawnMonster(string name, int x, int y, params object[] args)
    {
        Monster monster = SpawnMonster(name, x, y);
        monster.Setup(args);
        return monster;
    }

    protected Projectile SpawnProjectile(Projectile projectile, int x, int y, Direction dir, params object[] args)
    {
        Projectile proj = GameStateManager.Instance.SpawnProjectile(projectile, x, y, dir);
        proj.Setup(args);
        return proj;
    }

    // we don't have a projectile dictionary yet...
    /*
    protected void SpawnProjectile(string name, int x, int y, Direction dir)
    {
        SpawnProjectile(PrefabDictionary.Instance.projectilePrefabDictionary.Get(Projectile), x, y);
    }
    */

    protected Orb SpawnOrb(TileColor color, int x, int y)
    {
        return GameStateManager.Instance.SpawnOrb(PrefabDictionary.Instance.orbPrefab, color, x, y);
    }

    protected GameItem SpawnGameItem(GameItem gameItem, int x, int y, params object[] args)
    {
        GameItem item = GameStateManager.Instance.SpawnGameItem(gameItem, x, y);
        item.Setup(args);
        return item;
    }
    protected GameItem SpawnGameItem(string name, int x, int y, params object[] args)
    {
        GameItem item = GameStateManager.Instance.SpawnGameItem(PrefabDictionary.Instance.itemPrefabDict.GetItem(name), x, y);
        item.Setup(args);
        return item;
    }

    protected Monster CheckMonsterPosition(int x, int y)
    {
        return GameStateManager.Instance.CheckMonsterPosition(x, y);
    }
    protected Orb CheckOrbPosition(int x, int y)
    {
        return GameStateManager.Instance.CheckOrbPosition(x, y);
    }
    protected Projectile CheckProjectilePosition(int x, int y)
    {
        return GameStateManager.Instance.CheckProjectilePosition(x, y);
    }

    protected TileColor GetTileColor(int x, int y)
    {
        return TileManager.Instance.GetTileColor(x, y);
    }

    protected TileType GetTileType(int x, int y)
    {
        return TileManager.Instance.GetTileType(x, y);
    }

    protected TileData GetTileData(int x, int y)
    {
        return TileManager.Instance.GetTileData(x, y);
    }

    protected void SetTileColor(int x, int y, TileColor color)
    {
        TileManager.Instance.SetTileColor(x, y, color);
    }

    protected void SetTileType(int x, int y, TileType type)
    {
        TileManager.Instance.SetTileType(x, y, type);
    }

    protected void SetTileData(int x, int y, TileData data)
    {
        TileManager.Instance.SetTileData(x, y, data);
    }
}