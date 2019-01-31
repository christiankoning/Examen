using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour {

    public bool StartBattle;
    public int BossHealth;
    public Transform point;
    public Player player;
    private NavMeshAgent agent;

    // Update is called once per frame
    void Update ()
    {
        CheckBattle();
        CheckHealth();
	}

    void CheckBattle()
    {
        if(StartBattle == true)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(Countdown());
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(7);
        StartWalking();
    }

    void StartWalking()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.destination = point.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.Health = 0;
        }
    }

    void CheckHealth()
    {
        if(BossHealth <= 0)
        {
            // Boss Dies
            agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsDead", true);
        }
    }
}
