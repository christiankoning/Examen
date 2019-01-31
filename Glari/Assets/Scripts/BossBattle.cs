using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour {

    public GameObject PlayerModel;
    public Player player;
    public Boss boss;
    public SmoothCamera sCamera;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerModel)
        {
            player.StartBattle = true;
            sCamera.StartBossBattle = true;
            boss.StartBattle = true;
            player.BossBattleManager();
            sCamera.CutScene = true;
            sCamera.BossCutScene();
        }
    }
}
