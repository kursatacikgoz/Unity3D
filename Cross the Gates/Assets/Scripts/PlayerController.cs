using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject nextGate;
    public GameObject gameControl;
    [HideInInspector]
    public GateControl gateControl;
    private Direction nextDirection; // gelecek kapının üzerindeki yön tutuluyor
    private enum Direction { Right, Left, Up, Down, Mid }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gateControl = nextGate.GetComponent<GateControl>();
        defineDirection();

        

        /*
        if(gateControl.moveTo != (GateControl.position)nextDirection)
        {
            //hata animasyonu
        }*/

        if (gateControl.moveTo == (GateControl.position)nextDirection)
        {
            //score++
        }

        
    }

    private void defineDirection()
    {
        if (nextGate.tag == "Right")
        {
            nextDirection = Direction.Right;
        }
        else if (nextGate.tag == "Left")
        {
            nextDirection = Direction.Left;
        }
        else if (nextGate.tag == "Up")
        {
            nextDirection = Direction.Up;
        }
        else if (nextGate.tag == "Down")
        {
            nextDirection = Direction.Down;
        }
        else
        {
            // hata
            Debug.LogError("Invalid Tag");
        }
    }
}
