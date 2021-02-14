using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SIDE { Left, Middle, Right }
public class Character : MonoBehaviour
{
    public GameObject controlGame;
    public Camera mainCamera;
    public ParticleSystem coinEffect;

    private Animator charAnimator;
    private Rigidbody rb;
    //private CharacterController charController;
    private float newPositionX = 0f;

    public SIDE playerSide = SIDE.Middle;

    public float pointX;
    private float x, y;
    private float speedDodge = 40f, jumpPower = 7f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown, inJump, inRoll;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        //charController = GetComponent<CharacterController>();
        charAnimator = GetComponent<Animator>();
        transform.position = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameContinue = controlGame.GetComponent<GameController>().isGameContinue;

        if (isGameContinue)
        {
            SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
            SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
            SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
            SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

            if (transform.position.y < 0f)
            {
                isGrounded = true;
            }

            if (SwipeLeft)
            {

                if (playerSide == SIDE.Right)
                {
                    playerSide = SIDE.Middle;
                    newPositionX = 0f;
                }
                else if (playerSide == SIDE.Middle)
                {
                    playerSide = SIDE.Left;
                    newPositionX = -pointX;
                }
                charAnimator.Play("dodgeLeft");
            }
            else if (SwipeRight)
            {

                if (playerSide == SIDE.Left)
                {
                    playerSide = SIDE.Middle;
                    newPositionX = 0f;
                }
                else if (playerSide == SIDE.Middle)
                {
                    playerSide = SIDE.Right;
                    newPositionX = pointX;
                }
                charAnimator.Play("dodgeRight");
            }

            //    if(SwipeRight || SwipeLeft)
            //        MoveToPosition(new Vector3(newPositionX, transform.position.y, transform.position.z));


            moveHorizontal();
            Jump();
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Road")
        {
            isGrounded = true;
        }
        if (collision.transform.tag == "Block")
        {
            float midX = transform.position.x + mainCamera.transform.position.x *10f;
            midX /= 11f;
            float midY = transform.position.y + mainCamera.transform.position.y*10f;
            midY /= 11f;
            float midZ = transform.position.z + mainCamera.transform.position.z*10f;
            midZ /= 11f;

            rb.position = new Vector3(midX, midY, midZ);

            charAnimator.CrossFadeInFixedTime("death_movingTrain", .1f);
            controlGame.GetComponent<GameController>().isGameContinue = false;

        }
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Gold")
        {
            Debug.Log(collision.transform.tag);
            ParticleSystem effect = Instantiate(coinEffect, collision.transform.position, collision.transform.rotation) as ParticleSystem;
            float delayTime = 1f;
            Destroy(collision.gameObject);
            Destroy(effect.gameObject, delayTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Road")
        {
            isGrounded = false;
        }
    }

    public void moveHorizontal()
    {
        if (SwipeRight || SwipeLeft)
        {
            //x = Mathf.Lerp(transform.position.x, newPositionX, Time.deltaTime * speedDodge);
            //Debug.Log(newPositionX);
            //rb.MovePosition(Vector3.Lerp(transform.position, new Vector3(newPositionX, transform.position.y, transform.position.z), speedDodge));
            rb.position = Vector3.MoveTowards(transform.position, new Vector3(newPositionX, transform.position.y, transform.position.z), speedDodge);
            //rb.MovePosition(Vector3.right * newPositionX);
        }
    }

    public void Jump()
    {
        //Debug.Log(isGrounded);
        if (isGrounded)
        {
            if (charAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                charAnimator.Play("Landing");
                inJump = false;
            }

            if (SwipeUp)
            {
                y = jumpPower;
                rb.AddForce(Vector3.up * y * speedDodge);
                charAnimator.CrossFadeInFixedTime("Jump", .1f);
                inJump = true;
            }
            else if (SwipeDown)
            {
                charAnimator.CrossFadeInFixedTime("Roll", .1f);
                inJump = false;
            }

        }
        else
        {
            if (SwipeDown)
            {
                if (transform.position.y > 0)
                {
                    y -= jumpPower * 2 * Time.deltaTime;
                    rb.AddForce(Vector3.down * y * speedDodge);
                }
            }

            charAnimator.Play("Fall");
        }

    }
}
