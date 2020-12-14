using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    const string privateCode = "cIH06eZquUGMZw1VVhjEyQ34R6AASUCUSreugmyu5fgQ";
    const string publicCode = "5fd228eeeb36fe271405ddc7";
    const string webURL = "http://dreamlo.com/lb/";

    static Highscores instance;

    private Leaderboard leaderboard;

    public Highscore[] highscoresList;

    private void Awake()
    {
        leaderboard = GetComponent<Leaderboard>();
        instance = this;
    }

    public static void AddNewHighScore (string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }
    
    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Succesfull");
            DownloadHighscores();
        }
        else
        {
            print("Error uploading " + www.error);
        }
    }


    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoreFromDataBase");
    }
    IEnumerator DownloadHighscoreFromDataBase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            leaderboard.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error downloading " + www.error);
        }
    }

    private void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            //print(highscoresList[i].username +": " + highscoresList[i].score);
        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore (string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}
