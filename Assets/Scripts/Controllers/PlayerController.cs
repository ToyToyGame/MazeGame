using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool _pressed = false;
    KeyCode _pressedKey = KeyCode.None;
    BoardController boardController;

    void Start()
    {
        boardController = Managers.Game.GetBoard().GetComponent<BoardController>();

        Vector3 position = new Vector3(-2.5f, 2.5f, 0.6f);
        gameObject.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void Move()
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
                switch (_pressedKey)
                {
                    case KeyCode.W:
                        transform.position += Vector3.up * boardController.tileSize;
                        break;
                    case KeyCode.A:
                        transform.position += Vector3.left * boardController.tileSize;
                        break;
                    case KeyCode.S:
                        transform.position += Vector3.down * boardController.tileSize;
                        break;
                    case KeyCode.D:
                        transform.position += Vector3.right * boardController.tileSize;
                        break;
                }
            }
            _pressed = false;
            _pressedKey = KeyCode.None;
        }
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
