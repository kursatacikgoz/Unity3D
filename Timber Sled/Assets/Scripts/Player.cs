using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    ParticleSystem growParticle;

    [SerializeField]
    GameObject timber;

    [SerializeField]
    GameControl gameControl;

    GameObject LastPlane;

    public float moveSpeed = 5f;

    void Start()
    {
        DOTween.Init();
    }

    void Update()
    {
        move();

        if (timber.GetComponent<Transform>().localScale.y < .05f)
        {
            gameControl.endGame();
        }

        if (LastPlane) isFall();

    }

    void move()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x <= 1.9f)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x >= -1.9f)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void growing()
    {
        growParticle.Play();
        float scaleY = timber.GetComponent<Transform>().localScale.y;
        scaleY += .2f;

        timber.transform.DOScaleY(scaleY, .2f);
    }

    public void shrink()
    {
        growParticle.Play();
        float scaleY = timber.GetComponent<Transform>().localScale.y;
        scaleY -= .1f;

        timber.transform.DOScaleY(scaleY, .2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            LastPlane = collision.gameObject;
        }
    }

    void isFall()
    {
        if(transform.position.y < LastPlane.transform.position.y - 10f)
        {
            gameControl.endGame();
        }
    }
}
