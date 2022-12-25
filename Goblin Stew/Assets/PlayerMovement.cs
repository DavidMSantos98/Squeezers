using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movSpeed = new Vector2();
    float inputX, inputY;
    Vector2 movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        movement=new Vector3(movSpeed.x*inputX, movSpeed.y*inputY);

        movement*=Time.deltaTime;
        transform.Translate(movement);
    }
}
