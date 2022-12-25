using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    Vector3 cameraPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = cameraPosition;
    }
}
