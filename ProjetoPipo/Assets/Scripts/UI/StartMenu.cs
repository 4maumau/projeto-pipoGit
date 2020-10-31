using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField]private GameData gameData;
    public TextMeshProUGUI coinsText;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        gameData = FindObjectOfType<GameData>();

        coinsText.SetText("{0:0000}" , gameData.overallCoins);
    }
    
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TouchToStart(); // tá assim por causa do touch
        }
        
    }

    public void TouchToStart()
    {
        _gameManager.StartGame();
    }

    
    
}

