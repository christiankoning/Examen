using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour {

    public GameObject BossObject;
    public Boss boss;
    public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == BossObject)
        {
            boss.BossHealth = boss.BossHealth - damage;
            Destroy(gameObject);
        }
    }
}
