using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    BoardController boardController;
    bool _pressed = false;
    KeyCode _pressedKey = KeyCode.None;
    // boardController 배열상 위치,외부에서는 값을 불러올수만 있고 변경은 불가능
    public int PosY { get; private set; } 
    public int PosX { get; private set; }

    void Start()
    {
        boardController = Managers.Game.GetBoard().GetComponent<BoardController>();
        // Start Position
        PosY = 0;
        PosX = 1;
        // Set Player Object Position
        gameObject.transform.position = boardController.getTile(PosY, PosX).tileObject.transform.position;
        
        Managers.Input.KeyAction -= CheckAndMove;
        Managers.Input.KeyAction += CheckAndMove;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CheckAndMove()
    {
        if (boardController == null)
            return;
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
        if(boardController.getTile(PosY, PosX).tileType == Define.TileType.End)
        {

            Debug.Log("도착!");
        }
    }

    void Move()
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
}
