using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();

        GameObject board = Managers.Game.Spawn(Define.WorldObject.Board, "Board");
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);
        StartCoroutine(SpawnAfterSeconds(1.5f));
    }
    IEnumerator SpawnAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject monster = Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");

    }

    public override void Clear()
    {
    }
}
