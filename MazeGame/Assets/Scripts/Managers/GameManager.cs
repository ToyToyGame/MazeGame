using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    GameObject _player;
    GameObject _board;
    public GameObject GetPlayer() { return _player; }
    public GameObject GetBoard() { return _board; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Board:
                _board = go;
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }
}
