using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreDeath;
    public TextMeshProUGUI scoreDeath;
    public TextMeshProUGUI coinsDeath;

    public GameData gameData;
    public UIupdater uiUpdater;

    private void OnEnable()
    {
        highScoreDeath.SetText("{}m", Mathf.Round(uiUpdater.highScoreCount));
        scoreDeath.SetText("{}m", Mathf.Round(uiUpdater.scoreCount));
        coinsDeath.SetText("{}", gameData.coinsCollected);
       
    }

}
