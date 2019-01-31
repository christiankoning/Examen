using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public bool StartBattle;
    public int BossHealth;
    public Transform point;
    public Player player;
    private NavMeshAgent agent;
    public GameObject BossHealthBar;
    public Slider HealthBarSlider;

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
            BossHealthBar.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            BossHealthBar.SetActive(false);
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

        HealthBarSlider.value = BossHealth;

        if(BossHealth <= 0)
        {
            // Boss Dies
            agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsDead", true);
        }
    }
}
