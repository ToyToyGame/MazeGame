using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum TileType
    {
        Start,
        Empty,
        Wall,
        End,
    }

    public TileType tileType;
    public GameObject tileObject;

}
