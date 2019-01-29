using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private void Start()
    {
        gameObject.name = "Fire";
    }

    public void DestroyFire()
    {
        StartCoroutine(RemoveTime());
    }

    IEnumerator RemoveTime()
    {
        yield return new WaitForSeconds(10);
        StartDestroying();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject)
        {
            Destroy(gameObject);
        }
    }

    public void StartDestroying()
    {
        Destroy(gameObject);
    }
}
