using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] bool updateMap;
    public int[,,] grid;
    [SerializeField] GameObject[] floors;
    void Start()
    {
        if (updateMap)
        {
            UpdateGrid(floors);
        }
    }

    private void UpdateGrid(GameObject[] levelFloors)
    {
        Vector2Int gridOriginPoint = new Vector2Int((int)levelFloors[0].transform.position.x, (int)levelFloors[0].transform.position.y);

        int xMin;
        int xMax;
        int zMin;
        int zMax;
        bool firstIte = true;

        foreach (GameObject floor in levelFloors)
        {
            if (firstIte)
            {
                xMin=
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
