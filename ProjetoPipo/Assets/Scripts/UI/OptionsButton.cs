using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionsButton : MonoBehaviour
{

    private float openedHeight = 270f;
    private float closedHeight = 100f;
    

    public bool optionsButtonOpen = false;
    private Button button;

    public float enterTime = .5f;

    private RectTransform optionsSliced;
       
    void Awake()
    {
        optionsSliced = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OptionsClick);        
    }



    public void OptionsClick()
    {
        if (!optionsButtonOpen)
        {
            optionsSliced.DOSizeDelta(new Vector2(optionsSliced.rect.width, openedHeight), 0.5f).SetEase(Ease.OutBack);
            optionsButtonOpen = true;
        }
        else
        {
            optionsSliced.DOSizeDelta(new Vector2(optionsSliced.rect.width, closedHeight), 0.5f).SetEase(Ease.InBack);
            optionsButtonOpen = false;
        }

    }
            
}
