using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private int[,,] grid;
    private int x;
    private int z;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public PathNode( int[,,] grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }
}
