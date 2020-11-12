using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StartMenu : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]private GameData gameData;
    public TextMeshProUGUI coinsText;

    public RectTransform shopButton;
    public RectTransform optionsButton;

    private float enterTime = 0.55f;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        gameData = FindObjectOfType<GameData>();
        
        

        UIStart();

        coinsText.SetText("{0:0000}" , gameData.overallCoins);
    }


    private void UIStart()
    {
        Sequence uiEntranceSequence = DOTween.Sequence();
        uiEntranceSequence.Append(shopButton.DOAnchorPosX(-85f, enterTime).SetEase(Ease.OutBack))
            .Join(optionsButton.DOAnchorPosX(123f, enterTime).SetEase(Ease.OutBack));

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TouchToStart(); // tá assim por causa do touch
        }

        if (Input.GetKeyDown(KeyCode.Insert)) PlayerPrefs.DeleteAll();
        
    }

    public void TouchToStart()
    {
        _gameManager.StartGame();
    }

    
    
}

