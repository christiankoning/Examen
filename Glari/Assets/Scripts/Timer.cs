using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public float time = 180;
    public string Minutes;
    public string Seconds;

    public Player player;

    void Update()
    {
        Minutes = Mathf.Floor(time / 60).ToString("00");
        Seconds = Mathf.Floor(time % 60).ToString("00");

        time -= Time.deltaTime;

        if(time <= 0)
        {
            player.Health = 0;   
        }
    }
}
