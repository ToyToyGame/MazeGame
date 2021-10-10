using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    Tile[,] _tile; // 2차원 배열
    [SerializeField]
    public int _boardSize;
    [SerializeField]
    public float tileSize = 0.25f;

    public Tile getTile(int posY , int posX)
    {
        return _tile[posY, posX];
    }

    // Start is called before the first frame update
    void Start()
    {
        Init(17);
        Render();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Init(int size)
    {
        if (size % 2 == 0)
            return;
        _tile = new Tile[size, size];
        _boardSize = size;


        GenerateBySideWinder();
    }

    void GenerateBySideWinder()
    {
        // 일단, 길을 다 막아버리는 작업
        
        for (int y = 0; y < _boardSize; y++)
        {
            for (int x = 0; x < _boardSize; x++)
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
        for (int y = 0; y < _boardSize; y++)
        {
            int count = 1;
            for (int x = 0; x < _boardSize; x++)
            {
                if (x % 2 == 0 || y % 2 == 0)
                    continue;
                if (y == _boardSize - 2 && x == _boardSize - 2)
                    continue;
                if (y == _boardSize - 2)
                {
                    _tile[y, x + 1].tileType = Define.TileType.Empty;
                    continue;
                }
                if (x == _boardSize - 2)
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
        _tile[_boardSize - 2, _boardSize - 2].tileType = Define.TileType.End;
    }


    public void SpawnTile(Vector3 position, Tile tile)
    {
        tile.tileObject = Managers.Resource.Instantiate($"Tiles/{tile.tileType.ToString()}", gameObject.transform);
        tile.tileObject.transform.position = position;
    }

    public void Render()
    {
        Vector3 position = new Vector3();
        float middlePosition = _boardSize * tileSize * 0.5f;
        for (int y = 0; y < _boardSize; y++)
        {
            for (int x = 0; x < _boardSize; x++)
            {
                position.x = -middlePosition + tileSize * x;
                position.y = middlePosition +- 1 * tileSize * y;
                SpawnTile(position, _tile[y, x]);
            }
        }
    }

}
