using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    GameObject _player;
    GameObject _board;
    GameObject _monster;
    public GameObject GetPlayer() { return _player; }
    public GameObject GetBoard() { return _board; }
    public GameObject GetMonster() { return _monster; }
    public Define.GameState gameState { get; set; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        gameState = Define.GameState.Play;

        switch (type)
        {
            case Define.WorldObject.Board:
                _board = go;
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
            case Define.WorldObject.Monster:
                _monster = go;
                break;
        }

        return go;
    }
}
