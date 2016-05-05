using UnityEngine;

public class Orb : MonoBehaviour
{
    public TileColor color;

    public Position pos;

    protected void Awake()
    {
        pos = GetComponent<Position>();
    }

    protected void Start()
    {
        GameStateManager.Instance.AddOrb(this);
    }

    protected void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.RemoveOrb(this);
    }
}