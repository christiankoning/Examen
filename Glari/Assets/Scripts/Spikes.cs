using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private float RandomStartTime;
    private float RandomEndTime;

    void Start()
    {
        RandomStartTime = Random.Range(2, 5);
        RandomEndTime = Random.Range(5, 9);
        StartAnimation();
    }

    void StartAnimation()
    {
        GetComponent<Animator>().SetBool("IsReady", true);
        StartCoroutine(SwitchAnim());
    }

    IEnumerator SwitchAnim()
    {
        yield return new WaitForSeconds(RandomStartTime);
        GetComponent<Animator>().SetBool("IsReady", false);
        yield return new WaitForSeconds(RandomEndTime);
        StartAnimation();
    }

}
