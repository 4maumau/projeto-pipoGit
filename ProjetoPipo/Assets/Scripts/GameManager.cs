using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomOutSpeed;
    private bool zoomActive = false;

    public GameObject hUI;        
    [SerializeField] private UIupdater uiUpdater; 
    private AudioManager audioManager; 
    
    public GameObject deathMenu;
    public GameObject startMenu;
    public GameObject shops;
    [SerializeField]private RectTransform shopPanel = null;
    private bool shopPanelOut = true;

    private PlayerCollision playerCollision;
    private PlayerController playerController;

    private void Start()
    {
        playerCollision = FindObjectOfType<PlayerCollision>();
        playerController = FindObjectOfType<PlayerController>();
        audioManager = FindObjectOfType<AudioManager>();
                
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

    public void ActivateShop()
    {
        if (shopPanelOut)
        {
            shopPanel.DOAnchorPosX(0, 0.6f).SetEase(Ease.OutQuint);
            shopPanelOut = false;
        }
        else
        {
            shopPanel.DOAnchorPosX(-1649, 0.6f).SetEase(Ease.OutQuint);
            shopPanelOut = true;
        }
    }



    public void ButtonSound()
    {
        audioManager.PlaySound("ButtonClick");
    }

}
