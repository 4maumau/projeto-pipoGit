using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI[] highscoresText;
    Highscores highscoreManager;

    void Start()
    {
        for (int i = 0; i < highscoresText.Length; i++)
        {
            highscoresText[i].SetText(i + 1 + ". Fetching...");
        }

        highscoreManager = GetComponent<Highscores>();
        StartCoroutine("RefreshHighscores");
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoresText.Length; i++)
        {
            highscoresText[i].SetText(i + 1 + ". ");
            if (highscoreList.Length > i)
            {
                highscoresText[i].text += highscoreList[i].username + " - " + highscoreList[i].score + "m";
            }
        }
    }

    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            int score = Random.Range(0, 2000);
            string username = "";
            string alphabet = "abcdefghijklmnopqrstuv";

            for (int i = 0; i < Random.Range(5,10); i++)
            {
                username += alphabet[Random.Range(0, alphabet.Length)];
            }

            Highscores.AddNewHighScore(username, score);
        }
    }
}
