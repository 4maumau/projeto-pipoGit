using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public AudioManager audioManager;
    private bool loopMusic = true;
    
    private string currentMusic;
    private int currentMusicSpeed = 1;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currentMusic = "MusicSpeed1";

        StartCoroutine(MusicLoop());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StopCoroutine(MusicTransition());
            StartCoroutine(MusicTransition());
        }
    }

    IEnumerator MusicLoop()
    {
        Debug.Log("Playing " + currentMusic);
        while (loopMusic)
        {
            audioManager.PlaySound(currentMusic);
            
            yield return new WaitWhile(() => audioManager.GetSound(currentMusic).source.isPlaying);
            if (loopMusic) Debug.Log("we looping the music: " + currentMusic + "!!"); //checando se ainda vai loopar
        }
    }

    IEnumerator MusicTransition()
    {
        loopMusic = false;

        string musicTransition = "MusicTransition12";
        string nextMusic = "MusicSpeed2";

        switch (currentMusicSpeed)
        {
            case 1:
                currentMusic = "MusicSpeed1";
                musicTransition = "MusicTransition12";
                nextMusic = "MusicSpeed2";
                break;
            case 2:
                currentMusic = "MusicSpeed2";
                musicTransition = "MusicTransition23";
                nextMusic = "MusicSpeed3";
                break;
            case 3:
                currentMusic = "MusicSpeed3";
                musicTransition = "MusicTransition34";
                nextMusic = "MusicSpeed4";
                break;
            case 4:
                currentMusic = "MusicSpeed4";
                break;
            default:
                break;
        }

        Debug.Log("Wainting for current music to end. Current music: " + currentMusic);
        yield return new WaitWhile(() => audioManager.GetSound(currentMusic).source.isPlaying);
        

        audioManager.StopSound(currentMusic);
        Debug.Log("stopped current music: " +currentMusic);
        
        audioManager.PlaySound(musicTransition);
        Debug.Log("Now playing the transition music: " + musicTransition);

        yield return new WaitWhile(() => audioManager.GetSound(musicTransition).source.isPlaying);

        currentMusicSpeed++;
        currentMusic = nextMusic;
        loopMusic = true;
        Debug.Log("changing the current music and setting loop to true");
        StartCoroutine(MusicLoop());

        yield return null;
    }
}
