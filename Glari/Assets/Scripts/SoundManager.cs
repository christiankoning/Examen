using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public AudioClip Forest;
    public AudioClip PowerUp;
    public AudioClip Collectible;
    public AudioClip FireBallHit;
    public AudioClip BossBattle;
    public AudioClip Jumping;

    private AudioSource AudioAmb;
    public AudioSource AudioCollecting;
    public AudioSource AudioShooting;
    public AudioSource AudioPlayerMovement;

    void Start()
    {
        CheckScene();
    }

    void Update()
    {

    }

    public AudioSource AddAudio(AudioClip clip, bool Loop, float Volume)
    {
        AudioSource NewAudio = gameObject.AddComponent<AudioSource>();
        NewAudio.clip = clip;
        NewAudio.loop = Loop;
        NewAudio.volume = Volume;
        return NewAudio;
    }

    void CheckScene()
    {
        if(SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            AudioAmb = AddAudio(Forest, true, 0.1f);
            AudioAmb.Play();
        }
        if(SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            // Add Cave ambient sound if founded
        }
        if(SceneManager.GetSceneByBuildIndex(3).isLoaded)
        {
            AudioAmb = AddAudio(BossBattle, true, 0.1f);
            AudioAmb.Play();
        }
    }
}
