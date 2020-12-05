using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class ToggleMusic : MonoBehaviour
{

    public AudioMixer audioMixer;

    private Toggle _musicToggle;
    private Toggle _audioToggle;

    
    private float volumeOn = 0f;
    private float volumeOff = -80f;

    private void Awake()
    {
        _musicToggle = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Toggle>();
        _audioToggle = this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<Toggle>();
    }
    void Start()
    {
        _musicToggle.onValueChanged.AddListener(delegate { ChangeMusicVolume(_musicToggle); }); // method now "listen" to any change in the music toggle

        _audioToggle.onValueChanged.AddListener(delegate { ChangeAudioVolume(_audioToggle); }); // same but for audio toggle



        //checking audio
        CheckAudioPref();

        //checking music
        CheckMusicPref();
    }

    void CheckAudioPref()
    {
        if (PlayerPrefs.HasKey("AudioOn"))
        {
            if (PlayerPrefs.GetInt("AudioOn") == 1) _audioToggle.isOn = true;

            else _audioToggle.isOn = false;
        }
        else
        {
            PlayerPrefs.SetInt("AudioOn", 1);
        }
    }

    void CheckMusicPref()
    {
        if (PlayerPrefs.HasKey("MusicOn")) // ve se tem player pref
        {
            if (PlayerPrefs.GetInt("MusicOn") == 1) _musicToggle.isOn = true; //se for igual a 1 = ON

            else _musicToggle.isOn = false;
        }
        else // se não tiver, cria um
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
    }
      
    public void ToggleListenerTest(Toggle tog)
    {
        Debug.Log("Im listening... Music is " + tog.isOn);
    }
         

    public void ChangeAudioVolume(Toggle toggle) //é chamado sempre q mudar algo no toggle
    {
        
        if (toggle.isOn)
        {
            audioMixer.SetFloat("volumeAudio", volumeOn);
            PlayerPrefs.SetInt("AudioOn", 1);

            Debug.Log("Audio on");
        }
        else
        {
            audioMixer.SetFloat("volumeAudio", volumeOff);
            PlayerPrefs.SetInt("AudioOn", 0);
            
            Debug.Log("Audio off");
        }
    }

    public void ChangeMusicVolume(Toggle toggle) //é chamado sempre q mudar algo no toggle de música
    {

        if (toggle.isOn)
        {
            audioMixer.SetFloat("volumeMusic", volumeOn -10f);
            PlayerPrefs.SetInt("MusicOn", 1);

            Debug.Log("Music on");
        }
        else
        {
            audioMixer.SetFloat("volumeMusic", volumeOff);
            PlayerPrefs.SetInt("MusicOn", 0);

            Debug.Log("Music off");
        }
    }
}
