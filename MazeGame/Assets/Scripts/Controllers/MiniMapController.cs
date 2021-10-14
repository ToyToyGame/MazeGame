using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    BoardController boardController;
    PlayerController player;
    MonsterController monster;
    float tileSize = 0.015f;
    MiniTile[,] _minitile; // 2차원 배열
    Vector3 miniMapPosition = new Vector3(0.0f,7.0f,0.0f);
    GameObject miniPlayer;
    GameObject miniMonster;
    void Start()
    {
        boardController = Managers.Game.GetBoard().GetComponent<BoardController>();
        player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        monster = Managers.Game.GetMonster().GetComponent<MonsterController>();
        _minitile = new MiniTile[boardController._boardSize.Height, boardController._boardSize.Width];

        gameObject.transform.parent = Camera.main.transform;
        DrawMap();
        DrawMover(Define.WorldObject.Player);
        DrawMover(Define.WorldObject.Monster);
        // 카메라 화면에서 좌측 하단부에 위치하도록, vector.z 값이 0이면 화면에 안나오니 주의
        gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0.3f,1.0f));//position = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Managers.Game.gameState != Define.GameState.Play)
            return;
        // 미니맵 내의 플레이어, 몬스터 위치 업데이트
        miniPlayer.transform.position = GetMoverPosition(player);
        miniMonster.transform.position = GetMoverPosition(monster);
        skimDarkness();

    }

    // 미니맵 그리기
    void DrawMap()
    {
        Vector3 position = new Vector3();
        for (int y = 0; y < boardController._boardSize.Height; y++)
        {
            for (int x = 0; x < boardController._boardSize.Width; x++)
            {
                position.x = tileSize * x;
                position.y = -1 * tileSize * y;
                _minitile[y, x] = new MiniTile();
                _minitile[y, x].tileType = boardController.getTile(y, x).tileType;

                if(y == 0 || x == 0 || y == boardController._boardSize.Height -1 || x == boardController._boardSize.Width -1)
                    SpawnTile(position, _minitile[y, x], false);
                else
                    SpawnTile(position, _minitile[y, x], true);
            }
        }
    }

    public void SpawnTile(Vector3 position, MiniTile miniTile, bool fillDarkness)
    {

        miniTile.tileObject = Managers.Resource.Instantiate($"MiniMap_Tiles/{miniTile.tileType.ToString()}", gameObject.transform);
        miniTile.tileObject.transform.position = position;
        miniTile.tileObject.transform.localScale = new Vector3(tileSize, tileSize);

        miniTile.darkness = Managers.Resource.Instantiate($"MiniMap_Tiles/Darkness", gameObject.transform);
        miniTile.darkness.transform.position = position;
        miniTile.darkness.transform.localScale = new Vector3(tileSize, tileSize);
        if(! fillDarkness)
        {
            miniTile.darkness.SetActive(false);
        }

    }
    // 미니맵 가리는 암흑 걷어내기
    void skimDarkness()
    {
        if(_minitile[player.PosY, player.PosX].darkness.activeSelf)
        {
            _minitile[player.PosY, player.PosX].darkness.SetActive(false);
        }
        int[] deltaY = new int[] { -1, 0, 1, 0 };
        int[] deltaX = new int[] { 0, -1, 0, 1 };
        for (int i = 0; i < 4; i++)
        {
            int nextY = player.PosY + deltaY[i];
            int nextX = player.PosX + deltaX[i];
            if (nextX < 0 || nextX >= boardController._boardSize.Width || nextY < 0 || nextY >= boardController._boardSize.Height)
                continue;
            if (_minitile[nextY, nextX].darkness.activeSelf)
            {
                _minitile[nextY, nextX].darkness.SetActive(false);
            }
        }
    }

    // MoveController 를 상속받는 객체면 사용 가능
    Vector3 GetMoverPosition(MoveController mover)
    {
        Vector3 position = _minitile[mover.PosY, mover.PosX].tileObject.transform.position;
        return position;
    }

    void DrawMover(Define.WorldObject moverType)
    {
        switch(moverType)
        {
            case Define.WorldObject.Player:
                {
                    SpawnMover(GetMoverPosition(player), Define.WorldObject.Player, out miniPlayer);
                    break;
                }

            case Define.WorldObject.Monster:
                {
                    SpawnMover(GetMoverPosition(monster), Define.WorldObject.Monster, out miniMonster);
                    break;
                }
        }
    }

    public void SpawnMover(Vector3 position, Define.WorldObject moverType, out GameObject mover)
    {
        mover = Managers.Resource.Instantiate($"MiniMap_Tiles/{moverType.ToString()}", gameObject.transform);
        mover.transform.position = position;
        mover.transform.localScale = new Vector3(tileSize, tileSize);
        //mover.transform.parent = gameObject.transform;
    }

}
