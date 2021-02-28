using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int countOfSphereNeed = 3;
    public int sizeOfGlass = 3;
    public GameObject[] Fields;
    public GameObject[] ColorPrefabs;
    public GameObject[] Glasses;

    // Start is called before the first frame update
    void Start()
    {
        initializeFieldsWithSphere();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initializeFieldsWithSphere()
    {
        int sphereCount = (Fields.Length - countOfSphereNeed) / countOfSphereNeed;
        int numOfColorPrefabs = ColorPrefabs.Length;

        GameObject[] colors = new GameObject[Fields.Length - countOfSphereNeed];

        for (int i = 0; i < colors.Length; i++)
        {
            int num = i % (ColorPrefabs.Length);
            colors[i] = ColorPrefabs[num];
        }

        reshuffle(colors);



        for (int i = 0; i < Fields.Length / countOfSphereNeed; i++)
        {
            for (int j = 0; j < countOfSphereNeed; j++)
            {
                int num = i * countOfSphereNeed + j;
                GameObject temp;
                if (num < colors.Length)
                {
                    temp = Instantiate(colors[num], Fields[num].transform.position, Quaternion.identity);
                    temp.GetComponent<Sphere>().gameControl = transform.gameObject;
                    temp.transform.parent = Fields[num].transform;
                }
                else
                {
                    temp = null;
                }
            }
        }


    }

    void reshuffle(GameObject[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            GameObject tmp = texts[t];
            int r = UnityEngine.Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    public void isWon()
    {
        bool isWon = true;
        foreach (GameObject glass in Glasses)
        {
            Transform glassTransform = glass.transform;
            
            string sphereColor = "";
            foreach (Transform field in glassTransform)
            {
                if (field.tag == "Field")
                {
                    String str = "Field";
                    string lastSphere = null;
                    int count = 0;
                    for (int i = 1; i < countOfSphereNeed + 1; i++)
                    {
                        Transform t = glass.transform.Find(str + i);
                        foreach (Transform child in t)
                        {
                            if (child.tag == "BlueSphere" ||
                               child.tag == "GreenSphere" ||
                               child.tag == "RedSphere")
                            {
                                if(lastSphere== null)
                                {
                                    lastSphere = child.tag;
                                    count++;
                                }
                                else if (lastSphere == child.tag)
                                {
                                    count++;
                                }
                            }
                        }
                    }

                    if (count != countOfSphereNeed && count!=0)
                    {
                        isWon = false;
                        return;
                    }
                }
            }
        }

        if (isWon)
        {
            Debug.Log("basarılı");
        }


    }
}
