using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject controlGame;
    private bool isDone;
    private float nextRoadsPosZ=50f;
    // Start is called before the first frame update
    void Start()
    {
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameContinue = controlGame.GetComponent<GameController>().isGameContinue;
        if (isGameContinue)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            transform.position = newPos;


            if (transform.position.z < -50f)
            {
                Destroy(gameObject);
            }

            if(transform.position.z<0f && !isDone)
            {
                controlGame.GetComponent<GameController>().spawnRandomRoad(nextRoadsPosZ);
                isDone = true;
            }
        }

            
    }
}
