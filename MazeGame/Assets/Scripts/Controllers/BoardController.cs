using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardSize
{
    public BoardSize(int h, int w) { Height = h; Width = w; }
    public int Width;
    public int Height;
}
public class BoardController : MonoBehaviour
{

    Tile[,] _tile; // 2차원 배열
    GameObject _coin;
    float coinSpeed = 2.0f;
    float coinRiseDist = 1.0f;
    [SerializeField]
    public BoardSize _boardSize;
    float tileSize = 0.4f;


    public Tile getTile(int posY , int posX)
    {
        return _tile[posY, posX];
    }

    // Start is called before the first frame update
    void Start()
    {
        // 사이즈 17짜리 랜덤 배열 생성
        Init(21,41);
        // 미로는 안움직이니까 한번만 그리자.
        Render();

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Init(int height, int width)
    {
        if (width % 2 == 0 || height % 2 == 0)
            return;
        _tile = new Tile[height, width];
        _boardSize = new BoardSize(height,width);


        GenerateBySideWinder();
    }

    void GenerateBySideWinder()
    {
        // 일단, 길을 다 막아버리는 작업
        
        for (int y = 0; y < _boardSize.Height; y++)
        {
            for (int x = 0; x < _boardSize.Width; x++)
            {
                Tile tile = new Tile();
                if (x % 2 == 0 || y % 2 == 0)
                    tile.tileType = Define.TileType.Wall;
                else
                    tile.tileType = Define.TileType.Empty;
                _tile[y, x] = tile;
            }
        }

        // 
        // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
        System.Random rand = new System.Random();
        for (int y = 0; y < _boardSize.Height; y++)
        {
            int count = 1;
            for (int x = 0; x < _boardSize.Width; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                    continue;
                if (y == _boardSize.Height - 2 && x == _boardSize.Width - 2)
                    continue;
                if (y == _boardSize.Height - 2)
                {
                    _tile[y, x + 1].tileType = Define.TileType.Empty;
                    continue;
                }
                if (x == _boardSize.Width - 2)
                {
                    _tile[y + 1, x].tileType = Define.TileType.Empty;
                    continue;
                }

                if (rand.Next(0, 2) == 0) // 0 혹은 1 두개중 하나가 나온다. 1/2 확률 생성
                {
                    _tile[y, x + 1].tileType = Define.TileType.Empty;
                    count++;
                }
                else
                {
                    int randomindex = rand.Next(0, count);
                    _tile[y + 1, x - randomindex * 2].tileType = Define.TileType.Empty;
                    count = 1;
                }
            }
        }
        _tile[0, 1].tileType = Define.TileType.Start;
        _tile[_boardSize.Height - 2, _boardSize.Width - 2].tileType = Define.TileType.End;
    }



    public void SpawnTile(Vector3 position, Tile tile)
    {
        tile.tileObject = Managers.Resource.Instantiate($"Tiles/{tile.tileType.ToString()}", gameObject.transform);
        tile.tileObject.transform.position = position;
        tile.tileObject.transform.localScale = new Vector3(tileSize, tileSize);
        if (tile.tileType == Define.TileType.End)
        {
            _coin = Managers.Resource.Instantiate($"Coin", gameObject.transform);
            _coin.transform.position = position;
        }
    }

    public void Render()
    {
        Vector3 position = new Vector3();
        float middlePosition = (_boardSize.Height + _boardSize.Width) * tileSize * 0.5f;
        for (int y = 0; y < _boardSize.Height; y++)
        {
            for (int x = 0; x < _boardSize.Width; x++)
            {
                position.x = -middlePosition + tileSize * x;
                position.y = middlePosition +- 1 * tileSize * y;
                SpawnTile(position, _tile[y, x]);
            }
        }
    }

    public void RiseCoin()
    {
        // 코인을 위로 이동
        Vector3 EndTilePos = _tile[_boardSize.Height - 1, _boardSize.Width - 1].tileObject.transform.position;
        Vector3 dir = _coin.transform.position - EndTilePos;
        if (dir.magnitude > coinRiseDist)
        { 
            Managers.Game.gameState = Define.GameState.Success;
            StartCoroutine(EndAfterSeconds(1.0f));
            return;
        }

        float moveDist = Mathf.Clamp(coinSpeed * Time.deltaTime, 0, coinRiseDist);
        _coin.transform.position += Vector3.up * moveDist;
    }
    IEnumerator EndAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("End Game! Go to NextLevel!");
    }
}
