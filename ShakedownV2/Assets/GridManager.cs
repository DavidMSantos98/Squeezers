using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    int numOfLayers;
    Hashtable levelMapping = new Hashtable();

    void Start()
    {
        CreateLevelMapping();
    }

    private void CreateLevelMapping()
    {
        numOfLayers = transform.childCount;
        //only count layers that have any objects in them
        for (int i = 0; i < numOfLayers; i++)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                numOfLayers -= 1;
            }
        }

        for (int i = 0; i < numOfLayers; i++)
        {
            ArrayList layerData = new ArrayList();
            (int[,] layerMap, Vector2Int layerOriginWorldPosition) = GetLayerMap(transform.GetChild(i));
            layerData.Add(layerMap);
            layerData.Add(layerOriginWorldPosition);
            levelMapping.Add(i, layerData);
        }
    }
    
    private (int[,],Vector2Int layerOrigin) GetLayerMap(Transform floorLayer)
    {
        (Vector2Int layerDimensions,Vector2Int layerOriginPoint) = GetLayerDimensions(floorLayer);
        int[,] layerArray = new int[layerDimensions.x, layerDimensions.y];

        //Fill Array with "empty" value
        for (int z = 0; z < layerDimensions.y; z++)
        {
            for (int x = 0; x < layerDimensions.x; x++)//Fill Row with empty values
            {
                layerArray[x,z] = 0;
            }
        }

        
        //Replace empty values with values respective to existing floor objects, where appropriate

        for (int i = 0; i < floorLayer.childCount; i++)
        {
            if (floorLayer.GetChild(i).tag == "Walkable")
            {
                //Get the objects position in the array by subtracting its position by the layers origin point
                layerArray[(int)floorLayer.GetChild(i).position.x - layerOriginPoint.x,
                    (int)floorLayer.GetChild(i).position.z - layerOriginPoint.y]
                    = 1;
            }
        }

        return (layerArray, layerOriginPoint);
    }

    private (Vector2Int, Vector2Int) GetLayerDimensions(Transform floorLayer)
    {

        int largestX=0;
        int largestZ=0;


        int smallestX = (int)floorLayer.GetChild(0).position.x;
        int smallestZ = (int)floorLayer.GetChild(0).position.z;

        for (int i = 0; i < floorLayer.childCount; i++)
        {
            if(floorLayer.GetChild(i).position.x > largestX)
            {
                largestX = (int)floorLayer.GetChild(i).position.x;
            }

            if (floorLayer.GetChild(i).position.z > largestZ)
            {
                largestZ = (int)floorLayer.GetChild(i).position.z;
            }

            if (floorLayer.GetChild(i).position.x < smallestX)
            {
                smallestX = (int)floorLayer.GetChild(i).position.x;
            }

            if(floorLayer.GetChild(i).position.z < smallestZ)
            {
                smallestZ = (int)floorLayer.GetChild(i).position.z;
            }
        }
        //Always add +1 to the dimensions to count the origin cell
        return (new Vector2Int(largestX-smallestX+1, largestZ-smallestZ+1),(new Vector2Int(smallestX, smallestZ)));
    }

    /// Code for creating level mapping
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Code that runs calculations on the previously generated level mapping

    // Update is called once per frame
    void Update()
    {
        
    }
}
