using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap floor0;

    private Grid grid0;
    ArrayList tilePosArray;
    void Start()
    {

        floor0.CompressBounds();
        BoundsInt tileBounds = floor0.cellBounds;
        Debug.Log(tileBounds.size);

        

    }

    void Update()
    {
        
    }

}
