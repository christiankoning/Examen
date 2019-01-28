using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float time = 180;
    public string Minutes;
    public string Seconds;

    void Update()
    {
        Minutes = Mathf.Floor(time / 60).ToString("00");
        Seconds = Mathf.RoundToInt(time % 60).ToString("00");

        time -= Time.deltaTime;
    }
}
