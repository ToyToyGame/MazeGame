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
        GameObject monster = Managers.Game.Spawn(Define.WorldObject.Monster, "Monster");
        monster.SetActive(false);
        GameObject miniMap = Managers.Game.Spawn(Define.WorldObject.MiniMap, "MiniMap");
        Camera.main.gameObject.GetComponent<CameraController>().SetPlayer(player);
        StartCoroutine(SpawnAfterSeconds(1.5f,monster));
    }
    IEnumerator SpawnAfterSeconds(float seconds, GameObject monster)
    {
        yield return new WaitForSeconds(seconds);
        monster.SetActive(true);

    }

    public override void Clear()
    {
    }
}
