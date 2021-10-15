using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class Pos
{
    public Pos(int y, int x) { Y = y; X = x; }
    public int Y;
    public int X;
}
public class MonsterController : MoveController
{
    List<Pos> path = new List<Pos>();
    PlayerController player;
    float monsterSleepTime = 0.5f;
    float _sleepTime = 0.0f;
    int playerPosY;
    int playerPosX;
    int currentPathIdx = 0;
    protected override void Init()
    {
        base.Init();
        player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        BFS(player.PosY, player.PosX);
        playerPosX = player.PosX;
        playerPosY = player.PosY;

    }
    protected override void Move()
    {
        if (Managers.Game.gameState != Define.GameState.Play)
            return;
        if(PosX == player.PosX && PosY == player.PosY)
        {
            player.KillPlayer();
            return;
        }
        if (monsterSleepTime > _sleepTime)
        {
            _sleepTime += Time.deltaTime;
            return;
        }
        if (playerPosX != player.PosX || playerPosY != player.PosY)
        {
            playerPosY = player.PosY;
            playerPosX = player.PosX;
            path.Clear();
            BFS(playerPosY, playerPosX);
            currentPathIdx = 0;
        }
        if(path.Count > 1)
        {
            currentPathIdx = Mathf.Clamp(currentPathIdx + 1, 0, path.Count -1);
            Pos pos = path[currentPathIdx];
            PosX = pos.X;
            PosY = pos.Y;
            transform.position = boardController.getTile(PosY , PosX).tileObject.transform.position;
            _sleepTime = 0.0f;
        }

    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    void BFS(int destY , int destX)
    {
        int[] deltaY = new int[] { -1, 0, 1, 0 };
        int[] deltaX = new int[] { 0, -1, 0, 1 };
        bool[,] found = new bool[boardController._boardSize.Height, boardController._boardSize.Width];
        Pos[,] parent = new Pos[boardController._boardSize.Height, boardController._boardSize.Width];

        Queue<Pos> q = new Queue<Pos>();
        q.Enqueue(new Pos(PosY, PosX));
        found[PosY, PosX] = true;
        parent[PosY, PosX] = new Pos(PosY, PosX);

        while (q.Count > 0)
        {
            Pos pos = q.Dequeue();
            int nowY = pos.Y;
            int nowX = pos.X;

            for (int i = 0; i < 4; i++)
            {
                int nextY = nowY + deltaY[i];
                int nextX = nowX + deltaX[i];

                if (nextX < 0 || nextX >= boardController._boardSize.Width || nextY < 0 || nextY >= boardController._boardSize.Height)
                    continue;
                if (boardController.getTile(nextY, nextX).tileType == Define.TileType.Wall)
                    continue;
                if (found[nextY, nextX])
                    continue;
                q.Enqueue(new Pos(nextY, nextX));
                found[nextY, nextX] = true;
                parent[nextY, nextX] = new Pos(nowY, nowX);
            }

        }

        CalcPathFromParent(parent, destY, destX);

    }

    void CalcPathFromParent(Pos[,] parent, int destY, int destX)
    {
        int y = destY;
        int x = destX;
        // root 노드의 부모 노드는 자기 자신
        while (parent[y, x].Y != y || parent[y, x].X != x)
        {

            path.Add(new Pos(y, x));
            Pos pos = parent[y, x];
            y = pos.Y;
            x = pos.X;
        }

        path.Add(new Pos(y, x));
        path.Reverse();
    }
}
