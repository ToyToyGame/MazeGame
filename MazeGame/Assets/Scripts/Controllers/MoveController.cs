using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    protected BoardController boardController;

    // boardController �迭�� ��ġ,�ܺο����� ���� �ҷ��ü��� �ְ� ������ �Ұ���
    public int PosY { get; protected set; }
    public int PosX { get; protected set; }

    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Init()
    {
        boardController = Managers.Game.GetBoard().GetComponent<BoardController>();
        // Start Position
        PosY = 0;
        PosX = 1;
        // Set Player Object Position
        gameObject.transform.position = boardController.getTile(PosY, PosX).tileObject.transform.position;
    }
    protected abstract void Move();
}
