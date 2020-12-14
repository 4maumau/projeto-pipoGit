using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]private GameData gameData;
    public TextMeshProUGUI coinsText;

    public RectTransform shopButton;
    public RectTransform optionsButton;
    public RectTransform languageButton;
    public RectTransform leaderboardButton;

    private float enterTime = 0.55f;


    [SerializeField] private Image titleImage;

    public Sprite titleUS;
    public Sprite titlePT;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        gameData = FindObjectOfType<GameData>();

        UIStart();

        coinsText.SetText("{0:0000}", gameData.overallCoins);

        Translate();    
    }


    private void UIStart()
    {
        Sequence uiEntranceSequence = DOTween.Sequence();
        uiEntranceSequence.Append(shopButton.DOAnchorPosX(-65f, enterTime).SetEase(Ease.OutBack))
            .Join(optionsButton.DOAnchorPosX(85f, enterTime).SetEase(Ease.OutBack))
            .Join(languageButton.DOAnchorPosX(85f, enterTime).SetEase(Ease.OutBack))
            .Join(leaderboardButton.DOAnchorPosX(-49f, enterTime).SetEase(Ease.OutBack));

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TouchToStart(); // tá assim por causa do touch
        }

        if (Input.GetKeyDown(KeyCode.Insert)) PlayerPrefs.DeleteAll();
        Translate();

    }

    public void TouchToStart()
    {
        _gameManager.StartGame();
    }

    public void FeedBack()
    {
        Application.OpenURL("https://forms.gle/pG8AmT4ZKe6mtfnP7");
    }

    private void Translate()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            if (PlayerPrefs.GetInt("Language") == 1) titleImage.sprite = titlePT;
            else titleImage.sprite = titleUS;
        }
    }
    
}

