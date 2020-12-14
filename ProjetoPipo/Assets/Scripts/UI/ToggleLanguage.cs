using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLanguage : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;
    public bool pt;

    [SerializeField] private Toggle ptToggle;
    [SerializeField] private Toggle usToggle;


    private void Awake()
    {
        //if (!PlayerPrefs.HasKey("Language")) PlayerPrefs.SetInt("Language", 1);
    }
    void Start()
    {
        ptToggle.onValueChanged.AddListener(delegate { ChangeLanguage(); });
        if (PlayerPrefs.HasKey("Language"))
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                ptToggle.isOn = true;
            } 
            else
            {
                usToggle.isOn = true;
            }
        }
    }

    void Update()
    {
        
    }

    public void ChangeLanguage() //é chamado sempre q mudar algo no toggle
    {

        foreach (Toggle t in toggleGroup.ActiveToggles())
        {
            if (t.name == "US") pt = false;
            else pt = true;
        }

        if (pt)
        {
            PlayerPrefs.SetInt("Language", 1);
            ptToggle.isOn = true;
            Debug.Log("Language: PT");
        }
        else
        {
            PlayerPrefs.SetInt("Language", 0);
            usToggle.isOn = true;
            Debug.Log("Language US");
        }
    }
}
