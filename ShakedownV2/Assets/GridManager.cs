using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.ProBuilder;

public class GridManager : MonoBehaviour
{
    int numOfLayers;
    Hashtable levelMapping = new Hashtable();

    void Start()
    {
        CreateLevelMapping();
        PrintLevel();
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
            Transform levelObject = floorLayer.GetChild(i);
            
            //Get the objects position in the array by subtracting its position by the layers origin point
            Vector2Int objectPositionArray = new Vector2Int((int)levelObject.position.x - layerOriginPoint.x,
                                            (int)levelObject.position.z - layerOriginPoint.y);

            if (floorLayer.GetChild(i).tag == "Walkable")//Check for walkable status
            {
                if(floorLayer.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Floor"))//Check if its floor
                {
                    layerArray[objectPositionArray.x, objectPositionArray.y] = 1;
                }
                if(floorLayer.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Stair"))//Check if its stairs
                {
                    //Stairs are presented in the level array as 2 for the side with the initial steps and -2 for the rest.
                    
                    Transform stairTopRightCorner = levelObject.GetChild(1);
                    layerArray[objectPositionArray.x, objectPositionArray.y] = 2;

                    Vector2Int stairDimensions = new Vector2Int((int)levelObject.GetChild(0).GetComponent<Renderer>().bounds.size.x,
                        (int)levelObject.GetChild(0).GetComponent<Renderer>().bounds.size.z);

                    int stairDirection;
                    //0 Forward
                    //1 Right
                    //2 Left
                    //3 Backward

                    if (stairTopRightCorner.position.x> levelObject.position.x)
                    {
                        if (stairTopRightCorner.position.z > levelObject.position.z)// Stair is facing forward
                        {
                            stairDirection = 0;
                        }
                        else//Stair is facing right
                        {
                            stairDirection = 1;
                        }
                    }
                    else
                    {
                        if (stairTopRightCorner.position.z > levelObject.position.z)//Stair is facing left
                        {
                            stairDirection = 2;
                        }
                        else// Stair is facing backwards
                        {
                            stairDirection = 3;
                        }
                    }

                    for (int z = 0; z < stairDimensions.y; z++)
                    {
                        for (int x = 0; x < stairDimensions.x; x++)
                        {
                            if (stairDirection == 0)//Facing forward
                            {
                                if (z == 0)
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = 2;
                                }
                                else
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = -2;
                                }
                            }

                            if (stairDirection == 1)//Facing right
                            {
                                if (x == 0)
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = 2;
                                }
                                else
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = -2;
                                }
                            }

                            if (stairDirection == 2)//Facing left
                            {
                                if (x == stairDimensions.x-1)
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = 2;
                                }
                                else
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = -2;
                                }
                            }

                            if (stairDirection == 3)//Facing backward
                            {
                                if (z == stairDimensions.y-1)
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = 2;
                                }
                                else
                                {
                                    layerArray[objectPositionArray.x + x, objectPositionArray.y + z] = -2;
                                }
                            }

                        }
                    }

                }
                
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

    private void PrintLevel()
    {
        for (int i = 0; i < numOfLayers; i++)
        {
            ArrayList layerArrayList = (ArrayList)levelMapping[i];
            int[,] layerArray = layerArrayList.ToArray();
            string arrayRow = "";

            Debug.Log("Layer " + (i + 1));
            for (int z = 0; z < layerArray.GetLength(1); z++)
            {
                for (int x = 0; x < layerArray.GetLength(0); x++)
                {
                    arrayRow+=Convert.ToChar(layerArray[x,z]);
                }
                Debug.Log(arrayRow);
                arrayRow = "";
            }
        }
    }

    /// Code for creating level mapping
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Code that runs calculations on the previously generated level mapping

    // Update is called once per frame
    void Update()
    {
        
    }
}
