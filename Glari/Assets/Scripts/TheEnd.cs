using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheEnd : MonoBehaviour {

    public SmoothCamera sCamera;
    public Player player;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            sCamera.StartBossBattle = false;
            player.StartBattle = false;
        }
    }
}
