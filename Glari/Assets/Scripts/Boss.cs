using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour {

    public bool StartBattle;
    public int BossHealth;
    private NavMeshAgent agent;
    public Transform point;
    public Player player;

    // Update is called once per frame
    void Update ()
    {
        CheckBattle();
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
        yield return new WaitForSeconds(10);
        StartWalking();
    }

    void StartWalking()
    {
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
}
