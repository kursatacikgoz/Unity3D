using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().growing();
            Destroy(transform.gameObject);
        }
    }
}
