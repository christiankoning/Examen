using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject PlayPanel;
    public int Level;
    public Button Continue;

    void Start()
    {
        MainPanel.SetActive(true);
        PlayPanel.SetActive(false);

        Level = PlayerPrefs.GetInt("Level");

        if (Level == 1)
        {
            Continue.interactable = false;
        }
        else
        {
            Continue.interactable = true;
        }
    }

    public void StartGame()
    {
        PlayPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene("Level 1");
    }

    public void ContinueGame()
    {
        
        if(Level == 2)
        {
            SceneManager.LoadScene("Level 2");
        }

        if(Level == 3)
        {
            SceneManager.LoadScene("Level 3");
        }
    }

    public void BackToMenu()
    {
        MainPanel.SetActive(true);
        PlayPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
