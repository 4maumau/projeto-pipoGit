﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class DeathMenu : MonoBehaviour
{
    public GameData gameData;
    public UIupdater uiUpdater;


    [Header ("Texts")]
    public TextMeshProUGUI highScoreDeath;
    public TextMeshProUGUI scoreDeath;
    public TextMeshProUGUI coinsDeath;
    public TextMeshProUGUI coinsOverall;

    private float highScore = 0;
    private int scoreRising = 0;
    [SerializeField] [Range(0f, 1f)]private float waitTime = 0.01f;
    private int coinsRising = 0;


    [Header ("Rects for Animation")]
    public RectTransform coinsOverallRect;
    public RectTransform shopButton;
    public RectTransform optionsButton;
    public RectTransform replayButton;

    [Header ("HighSCoreAnimation")]
    public Image highScoreImage;
    [SerializeField] [Range(0f, .6f)] private float alphaTime = .1f;
    [SerializeField] [Range(0f, .6f)] private float scaleTime = .1f;
    private bool hsBeaten = false;

    private float enterTime = 0.55f;


    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetFloat("HighScore");
            
        } else
        {
            PlayerPrefs.SetFloat("HighScore", highScore);
            
        }

        coinsRising = gameData.overallCoins;

        highScoreDeath.SetText("{}m", Mathf.Round(highScore));
        scoreDeath.SetText("{}m", scoreRising);
        coinsDeath.SetText("{}", gameData.coinsCollected);
        coinsOverall.SetText("{0:0000}", gameData.overallCoins);


        UIStart();
        StartCoroutine(UpdateScore());
        
    }

    private void UIStart()
    {
        Sequence uiEntranceSequence = DOTween.Sequence();
        uiEntranceSequence.Append(shopButton.DOAnchorPosX(-85f, enterTime).SetEase(Ease.OutBack))
            .Join(optionsButton.DOAnchorPosX(123f, enterTime).SetEase(Ease.OutBack))
            .Append(coinsOverallRect.DOAnchorPosX(-83, enterTime + 0.1f).SetEase(Ease.OutBack))
            .Join(replayButton.DOAnchorPosY(36, enterTime + .4f).SetEase(Ease.OutBack))
            .OnComplete(Test);
    }

    IEnumerator UpdateScore()
    {
        while (scoreRising < Mathf.Round(uiUpdater.scoreCount))
        {
            if (uiUpdater.scoreCount > 1000f) scoreRising += 30;
            if (uiUpdater.scoreCount > 400f) scoreRising += 25;
            if (uiUpdater.scoreCount > 80f) scoreRising += 10;
            else scoreRising += 5;

            scoreDeath.SetText("{}m", scoreRising);
            if (scoreRising > highScore)
            {
                hsBeaten = true;
                highScore = scoreRising;
                highScoreDeath.SetText("{}m", scoreRising);
            }
            yield return new WaitForSeconds(waitTime);
        }
        if (hsBeaten) HighScoreAnimation();

        PlayerPrefs.SetFloat("HighScore", highScore);
        
        yield return null;
    }

    IEnumerator UpdateCoins()
    {
        while (gameData.overallCoins + gameData.coinsCollected > coinsRising)
        {
            coinsRising++;
            coinsOverall.SetText("{0:0000}", coinsRising);
            yield return new WaitForSeconds(waitTime);
        }

        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert)) HighScoreAnimation();
    }

    private void HighScoreAnimation()
    {
        
        Sequence hsAnimation = DOTween.Sequence();
        hsAnimation.AppendInterval(.3f)
            .Append(highScoreImage.DOFade(1f, alphaTime))
            .Join(highScoreImage.transform.DOScale(new Vector2(1f, 1f), scaleTime).SetEase(Ease.InCubic))
            .OnComplete(CameraShake);
    }

    private void Test()
    {
        StartCoroutine(UpdateCoins());
    }
    
    private void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
    }
}
