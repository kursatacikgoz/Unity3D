using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    public GameObject bullet;
    public Camera fpsCam;
    public Text text;

    int ShootPerMinute;
    bool isAuto;
    int Capacity;
    float Accuracy;
    int reloadTime;
    int damage;

    int bulletLeft;

    float range = 1000f;
    bool isReady;

    // Start is called before the first frame update
    void Start()
    {
        initializeVariables();
        reload();
        isReady = true;
        InvokeRepeating("makeReady", 0, 60.0f / ShootPerMinute);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            if (Input.GetKey(KeyCode.S))
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Fire();
            }
        }

        text.text = bulletLeft + "/" + Capacity;
    }

    void initializeVariables()
    {
        if (transform.name == "Desert Eagle")
        {
            ShootPerMinute = 60;
            isAuto = false;
            Capacity = 10;
            Accuracy = .9f;
            reloadTime = 4;
            damage = 40;

        }
        else if (transform.name == "Ak-47")
        {
            ShootPerMinute = 400;
            isAuto = true;
            Capacity = 30;
            Accuracy = .6f;
            reloadTime = 5;
            damage = 5;
        }
    }

    public void reload()
    {
        bulletLeft = Capacity;
    }

    public void Fire()
    {
        if (bulletLeft <= 0)
        {
            Debug.Log("Reload için R ye basın");
        }
        else if (isReady)
        {
            Debug.Log("Ateş edildi");
            isReady = false;
            bulletLeft--;

            RaycastHit hit;

            Vector3 dir = fpsCam.transform.forward;
            float spread = 1 - Accuracy;
            dir.x += Random.Range(-spread, spread);
            dir.y += Random.Range(-spread, spread);
            dir.z += Random.Range(-spread, spread);

            if (Physics.Raycast(fpsCam.transform.position, dir, out hit, range))
            {
                if (hit.transform.tag == "Target")
                {
                    Debug.Log("Hedef vuruldu");
                    // Debug.Log(damage);
                    hit.transform.GetComponent<Target>().takeDamage(damage);
                }
                else
                {
                    Debug.Log("Hedef vurulamadı");
                }
                // Debug.Log(hit.transform.name);
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green, 2, false);
            }
        }

    }

    void makeReady()
    {
        isReady = true;
    }
}
