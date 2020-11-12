using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameEvents;
using TMPro;

public class UIupdater : MonoBehaviour
{
    PlayerController playerScript; 
    public Transform player;
    
    public TextMeshProUGUI coinCountText = null;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highScoreText;

    
    public float scoreCount;
    public float highScoreCount;
    
    public bool scoreIncreasing;

    private void Start() 
    {
        playerScript = FindObjectOfType<PlayerController>();
        OnCoinUpdate += UpdateCoinText;
        scoreIncreasing = true;

        coinCountText.SetText("0000");

        
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }

    }

    private void Update()
    {
        if (playerScript.gameStarted && scoreIncreasing) // score Increasing stops when player dies
        {
            scoreCount = player.position.x;
        }
        

        scoreText.SetText("{0:0000}m", Mathf.Round(scoreCount));
        

        if (scoreCount > highScoreCount) // updating high score
        {
            highScoreCount = scoreCount;
            //PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

    }

    void UpdateCoinText(int coinsCollected)
    {
        coinCountText.SetText("{0:0000}", coinsCollected);
        
    }

    private void OnDestroy()
    {
        OnCoinUpdate -= UpdateCoinText;
    }
}
