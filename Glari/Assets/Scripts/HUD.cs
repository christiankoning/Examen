using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite FullHeart;
    public Sprite EmptyHeart;
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    public Text Collecting;

    public Player player;

    void Update()
    {
        PlayerHealthManager();
        CollectibleManager();
    }

    void PlayerHealthManager()
    {
        if (player.Health <= 0)
        {
            Heart1.sprite = EmptyHeart;
        }
        else if (player.Health == 1)
        {
            Heart2.sprite = EmptyHeart;
        }

        else if (player.Health == 2)
        {
            Heart3.sprite = EmptyHeart;
        }
        else
        {
            Heart1.sprite = FullHeart;
            Heart2.sprite = FullHeart;
            Heart3.sprite = FullHeart;
        }
    }

    void CollectibleManager()
    {
        Collecting.text = player.Collected + "/3";
    }
}
