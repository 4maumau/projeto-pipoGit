using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomOutSpeed;
    private bool zoomActive = false;


    public GameObject hUI;        // ui game object
    [SerializeField] private UIupdater uiUpdater; // ui script
    
    public GameObject deathMenu;
    public GameObject startMenu;
    public GameObject shops;

    private PlayerCollision playerCollision;
    private PlayerController playerController;

    private void Start()
    {
        playerCollision = FindObjectOfType<PlayerCollision>();
        playerController = FindObjectOfType<PlayerController>();
      

        // subscribing EndGame() to player's death event
        playerCollision.OnPlayerDeath += EndGame;
    }

    
    public void StartGame()
    {
        startMenu.SetActive(false);
        hUI.SetActive(true);
        playerController.gameStarted = true;
    }

    public void EndGame()
    {
        Debug.Log("Game Over!");
        

        // camera stops following player and zooms out
        zoomActive = true; 
        vcam.Follow = null;
 
        
        uiUpdater.scoreIncreasing = false;

        Invoke("ActivateDeathMenu", 1f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Time.timeScale == 0f)  
        {
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    private void ActivateDeathMenu() // deactivate HUI and activates death menu
    {
        hUI.SetActive(false);
        deathMenu.SetActive(true);
    }

    private void LateUpdate() // function to zoom out smoothly
    {
        if (zoomActive)  vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 5f, zoomOutSpeed);
    }

    private void Shop()
    {
        if (startMenu.activeInHierarchy) startMenu.SetActive(false);
        else if (deathMenu.activeInHierarchy) deathMenu.SetActive(false);
        shops.SetActive(true);
    }

}
