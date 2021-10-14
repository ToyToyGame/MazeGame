using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Board,
        Monster,
        MiniMap,
    }
    public enum TileType
    {
        Start,
        Empty,
        Wall,
        End,
    }

    public enum GameState
    {
        Play,
        Fail,
        Success,
        End,
    }
    public enum Scene
    {
        Unknown,
        Lobby,
        Game,
    }
}
