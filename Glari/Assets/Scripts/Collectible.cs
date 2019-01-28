using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public GameObject Player;
    public Player player;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            player.Collected++;
            Destroy(this.gameObject);
        }
    }
}
