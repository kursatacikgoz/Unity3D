using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    GameControl gameControl;

    Rigidbody rb;

    void Start()
    {
        DOTween.Init();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Collectable")
        {
            gameControl.removeClonePlayer(other.gameObject);
            Destroy(other.gameObject);
        }

        if (gameControl.count == 1 && other.transform.tag == "Player")
        {
            Destroy(other.gameObject);
            gameControl.RestartGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            rb.AddForce(Vector3.up * 500);
        }
    }
}
