using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGameController : MonoBehaviour
{


    public float barPosition = 4.0f;
    GameObject GageBar;
    GameObject SafeBar;
    GameObject stopBar;

    public Define.EventGameStatus eventGameStatus = Define.EventGameStatus.None;
    PlayerController player;
    MonsterController monster;
    
    public void StartEventGame()
    {
        gameObject.SetActive(true);
        eventGameStatus = Define.EventGameStatus.Start;
        gameObject.transform.parent = Camera.main.transform;
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f)) + new Vector3(0, barPosition, 10.0f) ;

    }

    void Start()
    {
        GageBar = Managers.Resource.Instantiate($"EventGame/GageBar", gameObject.transform);
        SafeBar = Managers.Resource.Instantiate($"EventGame/SafeBar", gameObject.transform);
        stopBar = Managers.Resource.Instantiate($"EventGame/StopBar", gameObject.transform);
        gameObject.transform.parent = Camera.main.transform;
        player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        monster = Managers.Game.GetMonster().GetComponent<MonsterController>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (eventGameStatus == Define.EventGameStatus.None)
            return;
        if (eventGameStatus == Define.EventGameStatus.Start)
            return;
        if (eventGameStatus == Define.EventGameStatus.Fail)
        {
            player.KillPlayer();
        }
        if(eventGameStatus == Define.EventGameStatus.Success)
        {
            monster.monsterStatus = Define.MoverStatus.Rise;
        }

        gameObject.SetActive(false);
        eventGameStatus = Define.EventGameStatus.None;
    }



}
