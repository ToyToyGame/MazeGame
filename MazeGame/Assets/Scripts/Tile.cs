using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{


    public Define.TileType tileType;
    public GameObject tileObject;

}


public class MiniTile : Tile
{
    public GameObject darkness;
}