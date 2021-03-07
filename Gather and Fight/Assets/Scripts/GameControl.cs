using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int count = 1;
    public GameObject player;
    public GameObject playerField;

    CapsuleCollider playerFieldCollider;

    private List<GameObject> ClonePlayers = new List<GameObject>();

    void Start()
    {
        ClonePlayers.Add(player);
        DOTween.Init();
        playerFieldCollider = playerField.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        FollowingPlayer();
    }

    //New object is added to the list 
    public void addClonePlayer(GameObject cp)
    {
        if (!ClonePlayers.Contains(cp))
        {
            ClonePlayers.Add(cp);
            count++;
        }
    }

    //The object is removed from the list
    public void removeClonePlayer(GameObject cp)
    {
        ClonePlayers.Remove(cp);
        count--;
    }

    // This function allows collectible clones to follow the player. 
    void FollowingPlayer()
    {
        for (int i = 1; i < ClonePlayers.Count; i++)
        {
            if (ClonePlayers[i])
            {
                if (player && Vector3.Distance(player.transform.position, ClonePlayers[i].transform.position) > 1)
                {
                    Vector3 pos = RandomPointInBounds(playerFieldCollider.bounds);
                    ClonePlayers[i].transform.DOMove(pos, 2f);
                }
            }
        }
    }

    //Returns a random point in the collider.
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            1f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    //Restarts the Scene.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
