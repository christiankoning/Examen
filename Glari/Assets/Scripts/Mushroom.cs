using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mushroom : MonoBehaviour {

    public int MHealth;
    public int MDamage;
    public GameObject Player;
    public Player player;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        MHealthManager();
    }

    void MHealthManager()
    {
        if(MHealth <= 0)
        {
            GetComponent<Animator>().SetBool("IsDead", true);
            gameObject.SetActive(false);
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Fire")
        {
            MHealth--;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            player.force = 500;
            player.Jump();
            MHealth--;
            player.force = 250;
        }
    }
}
