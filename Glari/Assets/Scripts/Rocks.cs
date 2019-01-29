using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject Hole;
    public GameObject RampCollider;
    public GameObject PlayerObject;
    public Player player;

    void Start()
    {
        Physics.IgnoreCollision(Hole.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(RampCollider.GetComponent<Collider>(), GetComponent<Collider>());
        StartCoroutine(Respawn());
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);
        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(1);
        ChangePos();
        
    }

    void ChangePos()
    {
        transform.position = SpawnPoint.transform.position;
        GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(Respawn());
    }

    void OnCollisionEnter(Collision collision)
    {
     if(collision.gameObject == PlayerObject)
        {
            player.Health = 0;
        }
    }
}
