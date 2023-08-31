using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Player
{
    public AI(Vector2 position, Vector2 direction) : base(position, direction)
    {

    }

    public void SetDirection(ushort[,] grid)
    {
        if (CheckIfWall(grid, 3, m_Direction))
        {
            //var rand = Random.Range(0f,0.99f);
            //if ()
            //if (rand <0.5f)
            //{
            //    CheckIfWall(grid, 3);
            //}
            //else
            //{

            //}
        }
    }

    bool CheckIfWall(ushort[,] grid, int errorMargin, Vector2 direction)
    {
        int cellX = (int)(position.x + 20f);
        int cellY = (int)(position.y + 20f);

        int directionX = (int)(direction.x);
        int directionY = (int)(direction.y);

        for (int i = 0; i < errorMargin; ++i)
        {
            if (cellX + directionX < 0 || cellX + directionX > 40 ||
                cellY + directionY < 0 || cellY + directionY > 40)
            {
                return true;
            }

            if (grid[cellX+directionX, cellY+directionY] != 0)
            {
                return true;
            }
        }
        return false;
    }
}
