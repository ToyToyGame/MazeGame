using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    GameObject _player;
    GameObject _board;
    GameObject _monster;
    GameObject _miniMap;
    GameObject _monsterAvoidEventGame;
    GameObject _bubble;
    public GameObject GetPlayer() { return _player; }
    public GameObject GetBoard() { return _board; }
    public GameObject GetMonster() { return _monster; }
    public GameObject GetMiniMap() { return _miniMap; }
    public GameObject GetBubble() { return _bubble; }

    public GameObject GetMonsterEventGame() { return _monsterAvoidEventGame; }
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
            case Define.WorldObject.MiniMap:
                _miniMap = go;
                break;

            case Define.WorldObject.MonsterAvoidEventGame:
                _monsterAvoidEventGame = go;
                break;

            case Define.WorldObject.Bubble:
                _bubble = go;
                break;
        }

        return go;
    }
}
