using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isGameContinue;
    public GameObject[] roadPrefabs;
    public int score = 0;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        isGameContinue = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameContinue)
        {
            score++;
            text.text = "Score : " + score;
        }
        
    }

    public void spawnRandomRoad(float z)
    {
        int num = Random.Range(0, roadPrefabs.Length);
        Vector3 pos = new Vector3(0f, 0f, z);
        GameObject newObj = roadPrefabs[num];
        newObj.GetComponent<Movement>().controlGame = transform.gameObject;

        Instantiate(newObj, pos, Quaternion.identity);
    }
}
