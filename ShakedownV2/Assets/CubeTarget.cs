using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTarget : MonoBehaviour
{
    private Renderer renderer;
    private bool mouseIsOnTarget=false;
    private bool changeHappened = false;
    // Start is called before the first frame update
    void Start()
    {
        renderer= GetComponent<Renderer>();
    }

    private void OnMouseEnter()
    {
        mouseIsOnTarget=true;
        changeHappened = true;
    }

    private void OnMouseExit()
    {
        mouseIsOnTarget = false;
        changeHappened = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeHappened)
        {
            if (mouseIsOnTarget)
            {
                renderer.material.color = Color.red;
            }
            else
            {
                renderer.material.color = Color.white;
            }
            changeHappened = false;
        }
        
    }
}
