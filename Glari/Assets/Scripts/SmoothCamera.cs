using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    public GameObject Player;
    public bool StartBossBattle;
    public GameObject boss;

    private Vector3 CameraPos;
    public Vector3 BossCamPos;
    public bool CutScene;
    
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
        if(StartBossBattle == true && CutScene == false)
        {
            transform.position = Player.transform.position + BossCamPos;
            transform.rotation = Quaternion.Euler(30, 180, 0);
        }
        else if(StartBossBattle == false && CutScene == false)
        {
            transform.position = Player.transform.position + CameraPos;
            transform.rotation = Quaternion.Euler(22.423f, 0, 0);
        }
        else if(CutScene == true)
        {
            transform.LookAt(boss.transform);
            transform.position = Player.transform.position + BossCamPos;
        }
    }

    public void BossCutScene()
    {
        StartCoroutine(EndCutScene());
        Player.GetComponent<Player>().enabled = false;
    }

    IEnumerator EndCutScene()
    {
        yield return new WaitForSeconds(7);
        CutScene = false;
        Player.GetComponent<Player>().enabled = true;
    }
}
