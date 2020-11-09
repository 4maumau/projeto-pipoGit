using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeathMenu : MonoBehaviour
{
    public GameData gameData;
    public UIupdater uiUpdater;


    [Header ("Texts")]
    public TextMeshProUGUI highScoreDeath;
    public TextMeshProUGUI scoreDeath;
    public TextMeshProUGUI coinsDeath;
    public TextMeshProUGUI coinsOverall;


    [Header ("Rects for Animation")]
    public RectTransform coinsOverallRect;
    public RectTransform shopButton;
    public RectTransform optionsButton;
    public RectTransform replayButton;

    private float enterTime = 0.55f;


    private void OnEnable()
    {
        highScoreDeath.SetText("{}m", Mathf.Round(uiUpdater.highScoreCount));
        scoreDeath.SetText("{}m", Mathf.Round(uiUpdater.scoreCount));
        coinsDeath.SetText("{}", gameData.coinsCollected);
        coinsOverall.SetText("{0:0000}", gameData.overallCoins);


        UIStart();
    }

    private void UIStart()
    {
        Sequence uiEntranceSequence = DOTween.Sequence();
        uiEntranceSequence.Append(shopButton.DOAnchorPosX(-85f, enterTime).SetEase(Ease.OutBack))
            .Join(optionsButton.DOAnchorPosX(123f, enterTime).SetEase(Ease.OutBack))
            .Append(coinsOverallRect.DOAnchorPosX(-83, enterTime + 0.2f).SetEase(Ease.OutBack))
            .Join(replayButton.DOAnchorPosY(36, enterTime + .4f).SetEase(Ease.OutBack));

    }


}
