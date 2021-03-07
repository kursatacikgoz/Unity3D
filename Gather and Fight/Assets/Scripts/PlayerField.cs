using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerField : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    GameControl gameControl;

    // 
    private void OnTriggerEnter(Collider other)
    {
        //Allows a player to be added to the followers list when they approach a collectable clone.
        if (other.transform.tag == "Collectable")
        {
            gameControl.addClonePlayer(other.gameObject);
            player = gameControl.player;
        }
    }
}
