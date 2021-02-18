using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Animator mainCameraAnimator;
    public GameObject player;
    public GameObject nextGate;
    public GameObject spawnPos;
    public GameObject[] gatePrefabs;
    public Text text;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        spawnGate();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score : " + score;

    }

    public void spawnGate()
    {
        int num = UnityEngine.Random.Range(0, gatePrefabs.Length);
        //Debug.Log(num);
        GameObject newGate = gatePrefabs[num];
        nextGate = newGate;
        nextGate.GetComponent<GateControl>().gameControl = transform.gameObject;
        player.GetComponent<PlayerController>().nextGate = newGate;
        Instantiate(nextGate, spawnPos.transform.position, Quaternion.identity);
    }

    public void EndGame()
    {
        // Show score and restart
        SceneManager.LoadScene("Main");
    }
}
