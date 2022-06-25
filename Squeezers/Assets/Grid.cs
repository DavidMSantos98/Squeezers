using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    int gridX;
    int gridY;

    bool[,] grid;

    private void InitGrid(int gridX, int gridY)
    {
        grid = new bool[gridX, gridY];
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
