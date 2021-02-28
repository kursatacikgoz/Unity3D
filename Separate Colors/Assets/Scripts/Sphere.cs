using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Sphere : MonoBehaviour
{
    public Vector3 screenPoint;
    public Vector3 offset;
    private bool isLookingFor = false;
    public GameObject gameControl;
    private GameObject goTo; // field objesi var
    private bool isHold = false;
    Sequence seq;

    private void Start()
    {
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseUp()
    {
        if (isHold)
        {
            Vector3 curPosition = transform.position;
            curPosition.z += .1f;
            transform.position = curPosition;
            Vector3 target;
            if (!isLookingFor)
            {
                seq = DOTween.Sequence();
                target = goTo.transform.position;
                target.y = 2.5f;
                seq.Append(transform.DOMove(target, .8f));
                seq.Append(transform.DOMove(DeepestEmptyField(goTo).transform.position, 1f));
                transform.parent = DeepestEmptyField(goTo).transform;

                gameControl.GetComponent<GameController>().isWon();
            }
            else
            {
                target = transform.parent.gameObject.transform.position;
                seq.Append(transform.DOMove(target, 0.8f));
            }
        }


    }

    private GameObject DeepestEmptyField(GameObject field)
    {
        GameObject glass = field.transform.parent.gameObject;
        Transform f1 = glass.transform.Find("Field1");
        if (isFilled(f1))
        {
            Transform f2 = glass.gameObject.transform.Find("Field2");
            if (isFilled(f2))
            {
                Transform f3 = glass.gameObject.transform.Find("Field3");
                if (isFilled(f3))
                {
                    return null;
                }
                else
                {
                    return f3.gameObject;
                }
            }
            else
            {
                return f2.gameObject;
            }
        }
        else
        {
            return f1.gameObject;
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.z -= .1f;
        //curPosition.y -= -.5f;

        if (findTheFieldAbove() == null)
        {
            transform.position = curPosition;
            isHold = true;
        }
    }

    private GameObject findTheFieldAbove()
    {
        GameObject res = null;

        GameObject glass = transform.parent.gameObject.transform.parent.gameObject;
        int field = transform.parent.name[transform.parent.name.Length - 1] - '0';
        field++;
        string nextField = "Field" + field;
//        Debug.Log(nextField);
        Transform f = glass.transform.Find(nextField);

        if (f != null)
        {
            if (isFilled(f))
            {
                foreach (Transform child in f)
                {

                    res = child.gameObject;
                    break;
                }
            }
        }
        return res;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Field")
        {
            if (isMovable(other))
            {
                isLookingFor = false;
                goTo = other.gameObject;
            }

        }
        else if (other.transform.tag == "BlueSphere" ||
              other.transform.tag == "GreenSphere" ||
              other.transform.tag == "RedSphere")
        {
            goTo = other.transform.parent.gameObject;
        }
    }

    private bool isMovable(Collider other)
    {
        GameObject glass = other.transform.parent.gameObject;
        Transform f1 = glass.transform.Find("Field1");

        if (isFilled(f1))
        {
            Transform f2 = glass.gameObject.transform.Find("Field2");
            if (isFilled(f2))
            {
                Transform f3 = glass.gameObject.transform.Find("Field3");
                if (isFilled(f3))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    private bool isFilled(Transform t)
    {


        foreach (Transform child in t)
        {
            if (child.tag == "BlueSphere" ||
               child.tag == "GreenSphere" ||
               child.tag == "RedSphere")
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Field")
        {
            isLookingFor = true;
        }
    }

}
