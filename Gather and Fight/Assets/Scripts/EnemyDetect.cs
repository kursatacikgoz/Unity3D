using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (enemy)
            if (other.transform.tag == "Collectable")
            {
                enemy.GetComponent<Enemy>().addTarget(other.gameObject);
            }
            else if (other.transform.tag == "Player" && other.GetComponent<Player>().count == 1)
            {
                enemy.GetComponent<Enemy>().addTarget(other.gameObject);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemy)
            if (other.transform.tag == "Collectable" || other.transform.tag == "Player")
            {
                enemy.GetComponent<Enemy>().removeTarget(other.gameObject);
            }
    }

}
