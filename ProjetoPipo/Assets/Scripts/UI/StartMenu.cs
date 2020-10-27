using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private GameManager _gameManager;


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TouchToStart();
        }
        
    }

    public void TouchToStart()
    {
        _gameManager.StartGame();
    }

    
    
}

