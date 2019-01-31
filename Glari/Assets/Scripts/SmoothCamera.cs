using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public GameObject Player;
    public bool StartBossBattle;

    private Vector3 CameraPos;
    public Vector3 BossCamPos;
    
    void Start()
    {
        CameraPos = transform.position - Player.transform.position;
        BossCamPos = BossCamPos - Player.transform.position;
    }

    void Update()
    {
        BossFight();
    }

    public void BossFight()
    {
        if(StartBossBattle == true)
        {
            transform.position = Player.transform.position + BossCamPos;
            transform.rotation = Quaternion.Euler(40, 180, 0);
        }
        else
        {
            transform.position = Player.transform.position + CameraPos;
        }
    }
}
