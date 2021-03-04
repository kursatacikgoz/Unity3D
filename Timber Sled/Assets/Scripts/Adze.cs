using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adze : MonoBehaviour
{
    bool isEnter = false;

    [SerializeField]
    GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            player.GetComponent<Player>().shrink();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().moveSpeed -= 2;
            isEnter = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().moveSpeed += 2;
            isEnter = false;
        }
    }
}
