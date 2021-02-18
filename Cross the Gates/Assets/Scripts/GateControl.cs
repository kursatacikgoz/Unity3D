using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{
    private float speedZ = -10f;
    private float moveSpeed = 5f;
    private float moveX = .4f;
    private float moveY = 1.5f;
    private float borderZ = 13f;
    private Rigidbody rb;

    private Vector3 startPos;

    public position moveTo;
    public position currentPosition;

    public GameObject gameControl;

    private Vector3 target;

    public enum position { Right, Left, Up, Down, Mid, Fail }


    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        currentPosition = position.Mid;
        moveTo = position.Mid;
        target = transform.position;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + speedZ * Time.deltaTime);
        rb.transform.position = newPos;

        if (moveTo == position.Mid)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveTo = position.Up;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveTo = position.Left;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveTo = position.Down;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveTo = position.Right;
            }
        }


        if (moveTo != currentPosition)
        {
            if ((moveTo == position.Left && transform.tag == "Left") ||
                (moveTo == position.Right && transform.tag == "Right") ||
                (moveTo == position.Up && transform.tag == "Up") ||
                (moveTo == position.Down && transform.tag == "Down"))
            {
                updatePosition();
            }
            else
            {
                gameControl.GetComponent<GameControl>().EndGame();
            }

        }

        if (transform.position.z < -borderZ)
        {
            gameControl.GetComponent<GameControl>().spawnGate();
            gameControl.GetComponent<GameControl>().score++;
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        String objTag = other.transform.tag;
        Debug.Log(objTag);
        if (objTag == "Player")
        {
            gameControl.GetComponent<GameControl>().EndGame();
        }
    }
    private void updatePosition()
    {
        if (moveTo == position.Left)
        {
            target = new Vector3(-moveX, transform.position.y, transform.position.z);
        }
        else if (moveTo == position.Right)
        {
            target = new Vector3(moveX, transform.position.y, transform.position.z);
        }
        else if (moveTo == position.Up)
        {
            target = new Vector3(transform.position.x, moveY, transform.position.z);
        }
        else if (moveTo == position.Down)
        {
            target = new Vector3(transform.position.x, -moveY, transform.position.z);
        }

        if (moveTo != position.Fail)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }


    }
}
