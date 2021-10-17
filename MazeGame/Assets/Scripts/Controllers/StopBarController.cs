using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopBarController : MonoBehaviour
{
    enum StopBarStatus
    {
        Move = 0,
        Stop = 1,
    }

    enum StopResult
    {
        None,
        Success,
        Fail,
    }

    StopBarStatus stopBarStatus = StopBarStatus.Move;

    float minGagePosX = -7.0f;
    float maxGagePosX = 7.0f;
    Vector3 moveSign = Vector3.right;
    float moveSpeed = 12.0f;
    StopResult avoidMonsterResult = StopResult.None;
    EventGameController eventGame;
    void Start()
    {
        eventGame = gameObject.transform.parent.GetComponent<EventGameController>();
        Managers.Input.KeyAction -= CheckAndStop;
        Managers.Input.KeyAction += CheckAndStop;
    }

    // Update is called once per frame
    void Update()
    {
        if (eventGame.eventGameStatus == Define.EventGameStatus.None)
            return;
        if (stopBarStatus == StopBarStatus.Move)
        {
            if(gameObject.transform.localPosition.x < minGagePosX)
            {
                moveSign = Vector3.right;
            }
            if (gameObject.transform.localPosition.x > maxGagePosX)
            {
                moveSign = Vector3.left;
            }

            gameObject.transform.position += moveSign * moveSpeed * Time.deltaTime;

        }
        else
        {
            if(IsSuccessMonsterAvoid())
            {
                Debug.Log("성공!");
                eventGame.eventGameStatus = Define.EventGameStatus.Success;

            }
            else
            {
                Debug.Log("실패!");

                eventGame.eventGameStatus = Define.EventGameStatus.Fail;
            }
            Deactivate();
        }
        
    }

    public void Deactivate()
    {
        stopBarStatus = StopBarStatus.Move;
        moveSign = Vector3.right;
        avoidMonsterResult = StopResult.None;
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f)) + new Vector3(0, eventGame.barPosition, 10.0f) + Vector3.left * maxGagePosX;
        //gameObject.SetActive(false);
    }

    bool IsSuccessMonsterAvoid()
    {
        return avoidMonsterResult == StopResult.Success;
    }

    void CheckAndStop()
    {
        if (Managers.Game.gameState != Define.GameState.Play)
            return;
        if (eventGame.eventGameStatus != Define.EventGameStatus.Start)
            return;
        if (PushSpaceBar())
        {
            stopBarStatus = StopBarStatus.Stop;
        }
    }


    bool PushSpaceBar()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (eventGame.eventGameStatus != Define.EventGameStatus.Start)
            return;
        if (collision.tag.Equals("SafeBar"))
        {
            avoidMonsterResult = StopResult.Success;
        }
    }

}
