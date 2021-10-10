using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    void Start()
    {
        GameObject board = Managers.Game.Spawn(Define.WorldObject.Board, "Board");
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        StartCoroutine(SpawnAfterSeconds(1.5f));
    }

    IEnumerator SpawnAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject monster = Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");

    }
    void Update()
    {
        
    }
}
