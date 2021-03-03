using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    int health;

    private float moveSpeed = 10f;

    [SerializeField]
    Material[] colors;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        GetComponent<MeshRenderer>().material = colors[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }


    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            GetComponent<MeshRenderer>().material = colors[1];
            Debug.Log("Sağlığı yenilemek için T ye basınız");
        }
        else
        {
            Debug.Log(health);
        }
    }

    public void makeHealthFull()
    {
        health = 100;
        GetComponent<MeshRenderer>().material = colors[0];
        Debug.Log("Sağlık yenilendi.");
    }
}
