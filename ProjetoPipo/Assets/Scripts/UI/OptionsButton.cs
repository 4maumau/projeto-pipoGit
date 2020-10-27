using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionsButton : MonoBehaviour
{

    private float openedHeight = 270f;
    [SerializeField] private float overkill = 10f;
    [SerializeField] [Range(0f, 1f)] private float openingTime = .5f;

    public bool optionsButtonOpen = false;
    private Button button;

    private RectTransform optionsSliced;
    private Sequence optionsOpenSequence;

    private Sequence optionsCloseSequence;

    void Awake()
    {
        optionsSliced = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        button = this.gameObject.GetComponent <Button>();
        button.onClick.AddListener(OptionsClick);

        optionsOpenSequence = DOTween.Sequence();
        optionsCloseSequence = DOTween.Sequence();
    }

    
    public void OptionsClick()
    {
        if (!optionsButtonOpen) OpenOptions();
        else CloseOptions();
    }

    public void OpenOptions()
    {
        //optionsOpenSequence = DOTween.Sequence();
            //open
        optionsOpenSequence.Append(optionsSliced.DOSizeDelta(new Vector2(optionsSliced.rect.width, openedHeight + overkill), openingTime)); //aumenta o rect
        optionsOpenSequence.Join(optionsSliced.DOScaleX(1.1f, openingTime)); //scale
        // overkill
        optionsOpenSequence.Append(optionsSliced.DOSizeDelta(new Vector2(optionsSliced.rect.width, openedHeight), .03f)); 
        optionsOpenSequence.Join(optionsSliced.DOScaleX(1f, .03f));
        
        optionsButtonOpen = true;
        
    }

    public void CloseOptions()
    {
        //optionsCloseSequence = DOTween.Sequence();
            //overkill
        optionsCloseSequence.Append(optionsSliced.DOSizeDelta(new Vector2(optionsSliced.rect.width, openedHeight + overkill), .03f));
        optionsCloseSequence.Join(optionsSliced.DOScaleX(1.1f, 0.03f));
        //close
        optionsCloseSequence.Append(optionsSliced.DOSizeDelta(new Vector2(100, 100), .03f));
        optionsCloseSequence.Join(optionsSliced.DOScaleX(1f, .03f));

        optionsButtonOpen = false;
    }

    
}
