using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public GameObject Player;
    public Player player;
    public SoundManager SManager;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            player.Collected++;
            SManager.AudioPlayerMovement = SManager.AddAudio(SManager.Collectible, false, 0.2f);
            SManager.AudioPlayerMovement.Play();
            Destroy(this.gameObject);
        }
    }
}
