using UnityEngine;
using System.Collections;

public enum TileType
{
    None, Yellow, Blue, Green, Red, Purple, Black
}
public class Tile : MonoBehaviour
{
    public TileType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

	void Start ()
	{
	    transform.position = new Vector3(X, Y);
	}
}
