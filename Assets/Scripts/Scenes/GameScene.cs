using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    void Start()
    {
        GameObject board = Managers.Game.Spawn(Define.WorldObject.Board, "Board");
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
