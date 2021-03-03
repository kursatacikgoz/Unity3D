using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPos;
    private Vector3 mousePos;


    public void Start()
    {
        DOTween.Init();
    }

    public void Update()
    {
    }
    /*
    private void OnMouseDrag()
    {
        mousePos = Input.mousePosition;
        Ray mouseCast = Camera.main.ScreenPointToRay(mousePos);
        // Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        RaycastHit hit;
        // float rayLength;
        if (Physics.Raycast(mouseCast, out hit, 100))
        {
            Debug.DrawLine(mousePos, targetPos, Color.blue);
            targetPos = new Vector3(hit.point.x, 0f, hit.point.z);

            // We need some distance margin with our movement
            // or else the character could twitch back and forth with slight movement
            if (Vector3.Distance(targetPos, transform.position) >= 0.5f)
            {
                transform.LookAt(targetPos);
                //transform.DOMove(targetPos,2f);
                // transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }
    }
    */
}
