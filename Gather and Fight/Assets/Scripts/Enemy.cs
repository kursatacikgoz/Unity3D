using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject field;

    [SerializeField]
    GameControl gameControl;

    private SphereCollider fieldCollider;
    private bool canAttack = true;

    public List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        fieldCollider = field.GetComponent<SphereCollider>();
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
    }

    public void AttackPlayer()
    {
        if (targets.Count > 0)
        {
            if (!targets[0])
            {
                targets.RemoveAt(0);
            }
            else
            {
                transform.DOMove(targets[0].transform.position, 1f);
            }
        }

    }

    public void addTarget(GameObject t)
    {
        targets.Add(t);
    }

    public void removeTarget(GameObject t)
    {
        targets.Remove(t);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack)
        {
            if (other.transform.tag == "Collectable")
            {
                gameControl.removeClonePlayer(other.gameObject);
                Destroy(other.gameObject);
                Destroy(gameObject);
                canAttack = false;
            }
            else if (other.transform.tag == "Player" && other.GetComponent<Player>().count == 1)
            {
                gameControl.removeClonePlayer(other.gameObject);
                Destroy(other.gameObject);
                Destroy(gameObject);
                canAttack = false;
                gameControl.RestartGame();
            }
        }

    }
}
