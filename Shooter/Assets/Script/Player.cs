using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject[] guns;
    [SerializeField]
    GameObject activeGun;
    [SerializeField]
    GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (i == 0) guns[i].SetActive(true);
            else guns[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextGun();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            activeGun.GetComponent<GunControl>().reload();
        }

        

        if (Input.GetKeyDown(KeyCode.T))
        {
            target.GetComponent<Target>().makeHealthFull();
        }
    }

    void nextGun()
    {
        if (guns.Length > 1)
        {
            int activeGunNum = 0;
            for (int i = 0; i < guns.Length; i++)
            {
                if (guns[i].activeSelf) activeGunNum = i;

                guns[i].SetActive(false);
            }

            if (activeGunNum + 1 == guns.Length)
            {
                guns[0].SetActive(true);
                activeGunNum = 0;
            }
            else
            {
                activeGunNum++;
                guns[activeGunNum].SetActive(true);
            }

            activeGun = guns[activeGunNum];
        }
    }
}
