using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour {

    public SmoothCamera sCamera;
    public Player player;

    public bool isFadingOut;
    public Texture2D fadeImage;
    public float fadeSpeed = 0.2f;
    public int drawDepth = 1000;
    private float alpha = -1.0f;
    private int fadeDir = 1;

    public void Start()
    {
        isFadingOut = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            sCamera.StartBossBattle = false;
            player.StartBattle = false;
            isFadingOut = true;
            StartCoroutine(finalcountdown());
        }
    }

    void OnGUI()
    {
        if (isFadingOut == true)
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            Color thisAlpha = GUI.color;
            thisAlpha.a = alpha;
            GUI.color = thisAlpha;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeImage);
        }
    }

    IEnumerator finalcountdown()
    {
        yield return new WaitForSeconds(3);
        GoToMenu();
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
