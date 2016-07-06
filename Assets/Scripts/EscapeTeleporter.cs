using UnityEngine;

[RequireComponent(typeof(Position))]
public class EscapeTeleporter : MonoBehaviour
{
    public Position pos;
    void Awake()
    {
        pos = GetComponent<Position>();
    }
}