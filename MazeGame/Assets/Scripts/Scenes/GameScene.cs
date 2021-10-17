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
        GameObject bubble = Managers.Game.Spawn(Define.WorldObject.Bubble, "Bubble");
        bubble.SetActive(false);
        GameObject miniMap = Managers.Game.Spawn(Define.WorldObject.MiniMap, "MiniMap");
        GameObject darkness = Managers.Game.Spawn(Define.WorldObject.Darkness, "Darkness");
        GameObject MonsterAvoidingEventGame = Managers.Game.Spawn(Define.WorldObject.MonsterAvoidEventGame, "EventGame/EventGame");
        MonsterAvoidingEventGame.SetActive(false);
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
