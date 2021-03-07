using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public LayerMask hitLayers;
    public int count;
    public GameControl gameControl;

    void Start()
    {
        DOTween.Init();
    }

    void Update()
    {
        Move();
        count = gameControl.count;
    }

    //Allows the player to move according to the mouse position.
    void Move()
    {
        float time = 2f;
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))
        {
            Vector3 point = hit.point;
            point.y = 1f;

            if (Vector3.Distance(point, transform.position) > 10) time = 5f;

            transform.DOMove(point, time);
        }
    }
}
