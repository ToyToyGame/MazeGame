using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MoveController
{

    bool _pressed = false;
    KeyCode _pressedKey = KeyCode.None;
    float playerRiseDist = 0.3f;
    float playerSpeed = 2.0f;
    public Define.MoverStatus playerStatus = Define.MoverStatus.Run;

    protected override void Init()
    {
        base.Init();
        Managers.Input.KeyAction -= CheckAndMove;
        Managers.Input.KeyAction += CheckAndMove;
    }
    void CheckAndMove()
    {
        if (boardController == null)
            return;
        // 게임 끝
        if (Managers.Game.gameState != Define.GameState.Play)
            return;
        if (playerStatus == Define.MoverStatus.Idle)
            return;
        if (playerStatus == Define.MoverStatus.Rise)
            RisePlayer();
        if(boardController.getTile(PosY, PosX).tileType == Define.TileType.End)
        {
            boardController.RiseCoin();
            return;
        }
        if (pressedKey() != KeyCode.None)
        {
            _pressedKey = pressedKey();
            _pressed = true;
        }
        else
        {
            if (_pressed)
            {
                Move();

            }
            _pressed = false;
            _pressedKey = KeyCode.None;
        }
    }

    protected override void Move()
    {
        switch (_pressedKey)
        {
            case KeyCode.W:
                {
                    // 위로 이동
                    bool canMove = checkCanMove(PosY - 1, PosX);
                    if (canMove)
                    {
                        transform.position = boardController.getTile(PosY - 1, PosX).tileObject.transform.position;
                        --PosY;
                    }
                }
                break;
            case KeyCode.A:
                {
                    // 왼쪽으로 이동
                    bool canMove = checkCanMove(PosY, PosX - 1);
                    if (canMove)
                    {
                        transform.position = boardController.getTile(PosY, PosX - 1).tileObject.transform.position;
                        --PosX;
                    }
                }
                break;
            case KeyCode.S:
                {
                    // 아래로 이동
                    bool canMove = checkCanMove(PosY + 1, PosX);
                    if (canMove)
                    {
                        transform.position = boardController.getTile(PosY + 1, PosX).tileObject.transform.position;
                        ++PosY;
                    }
                }
                break;
            case KeyCode.D:
                {
                    // 오른쪽으로 이동
                    bool canMove = checkCanMove(PosY, PosX + 1);
                    if (canMove)
                    {
                        transform.position = boardController.getTile(PosY, PosX + 1).tileObject.transform.position;
                        ++PosX;
                    }
                }
                break;
        }
    }

    bool checkCanMove(int posY, int posX)
    {
        return posY >= 0 && posX >= 0 && boardController.getTile(posY, posX).tileType != Define.TileType.Wall;
    }


    KeyCode pressedKey()
    {
        if (Input.GetKey(KeyCode.W))
            return KeyCode.W;
        if (Input.GetKey(KeyCode.A))
            return KeyCode.A;
        if (Input.GetKey(KeyCode.S))
            return KeyCode.S;
        if (Input.GetKey(KeyCode.D))
            return KeyCode.D;

        return KeyCode.None;
    }


    public void KillPlayer()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("Player_Jump");
        playerStatus = Define.MoverStatus.Rise;
    }


    public void RisePlayer()
    {
        // 몬스터에게 잡혔으니 플레이어를 위로 이동
        Vector3 tilePos = boardController.getTile(PosY, PosX).tileObject.transform.position;
        Vector3 dir = transform.position - tilePos;
        if (dir.magnitude > playerRiseDist)
        {
            // TODO
            // 게임 상태 바꾸는게 약간 중구난방인 느낌.. 
            Managers.Game.gameState = Define.GameState.Fail;
            return;
        }

        float moveDist = Mathf.Clamp(playerSpeed * Time.deltaTime, 0, playerRiseDist);
        gameObject.transform.position += Vector3.up * moveDist;
    }
}
