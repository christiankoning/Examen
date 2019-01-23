using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public GameObject Player;

    private Vector3 CameraPos;

    
    void Start()
    {     
        CameraPos = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {  
        transform.position = Player.transform.position + CameraPos;
    }
}
