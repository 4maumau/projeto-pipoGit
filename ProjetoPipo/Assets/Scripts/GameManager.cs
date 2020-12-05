using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomOutSpeed;
    private bool zoomActive = false;

    public GameObject hUI;        
    [SerializeField] private UIupdater uiUpdater; 
    [SerializeField]private AudioManager audioManager; 
    
    public GameObject deathMenu;
    public GameObject startMenu;
    public GameObject shops;
    [SerializeField]private RectTransform shopPanel = null;
    private bool shopPanelOut = true;

    private PlayerCollision playerCollision;
    private PlayerController playerController;


    private bool transitioning = false;
    private string currentMusic;
    private string musicTransition;
    private int currentMusicSpeed = 1;
    private bool loopMusic;

    private void Start()
    {
        playerCollision = FindObjectOfType<PlayerCollision>();
        playerController = FindObjectOfType<PlayerController>();
        audioManager = FindObjectOfType<AudioManager>();
                
        // subscribing EndGame() to player's death event
        playerCollision.OnPlayerDeath += EndGame;
        playerController.OnSpeedUp += ChangeMusic;
        currentMusicSpeed = 1;

        if (!audioManager.GetSound("MusicMenu").source.isPlaying) audioManager.PlaySound("MusicMenu");
        currentMusic = "MusicSpeed1";

    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        hUI.SetActive(true);
        playerController.gameStarted = true;
        playerController.gameStarted = true;
        playerController.rb.velocity = Vector2.up * playerController.jumpForce;

        audioManager.StopSound("MusicMenu");
        loopMusic = true;
        StartCoroutine(MusicLoop());
    }
  
    // handle all the menus
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
        loopMusic = false;
        StopCoroutine(MusicTransition());
        audioManager.StopSound(currentMusic);

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
        
        loopMusic = false;
        StopAllCoroutines();
        audioManager.StopSound(currentMusic);
        audioManager.StopSound(musicTransition);
        audioManager.PlaySound("MusicMenu");
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

    private void LateUpdate() // function to zoom out smoothly
    {
        if (zoomActive)  vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 5f, zoomOutSpeed);
    }


    private void ChangeMusic()
    {
        if (!transitioning)
        {
            StopCoroutine(MusicTransition());
            StartCoroutine(MusicTransition());
        } else
        {
            Debug.Log("Error, we are already transitioning musics");
        }
    }

    IEnumerator MusicLoop()
    {
        Debug.Log("Playing " + currentMusic);
        while (loopMusic)
        {
            audioManager.PlaySound(currentMusic);

            yield return new WaitWhile(() => audioManager.GetSound(currentMusic).source.isPlaying);
            if (loopMusic) Debug.Log("we looping the music: " + currentMusic + "!!"); //checando se ainda vai loopar
        }
    }

    IEnumerator MusicTransition()
    {
        loopMusic = false;
        transitioning = true;

        musicTransition = "MusicTransition12";
        string nextMusic = "MusicSpeed2";

        switch (currentMusicSpeed)
        {
            case 1:
                currentMusic = "MusicSpeed1";
                musicTransition = "MusicTransition12";
                nextMusic = "MusicSpeed2";
                break;
            case 2:
                currentMusic = "MusicSpeed2";
                musicTransition = "MusicTransition23";
                nextMusic = "MusicSpeed3";
                break;
            case 3:
                currentMusic = "MusicSpeed3";
                musicTransition = "MusicTransition34";
                nextMusic = "MusicSpeed4";
                break;
            default:
                currentMusic = "MusicSpeed4";
                musicTransition = null;
                nextMusic = "MusicSpeed4";
                break;
        }

        Debug.Log("Waiting for current music to end. Current music: " + currentMusic);
        yield return new WaitWhile(() => audioManager.GetSound(currentMusic).source.isPlaying);

        
        audioManager.StopSound(currentMusic);
        Debug.Log("stopped current music: " + currentMusic);

        if (musicTransition != null)
        {
            audioManager.PlaySound(musicTransition);
            Debug.Log("Now playing the transition music: " + musicTransition);
            yield return new WaitWhile(() => audioManager.GetSound(musicTransition).source.isPlaying);
        }
        else
        {
            audioManager.PlaySound(currentMusic);
            yield return new WaitWhile(() => audioManager.GetSound(currentMusic).source.isPlaying);
        }

        currentMusicSpeed++;
        currentMusic = nextMusic;
        loopMusic = true;
        Debug.Log("changing the current music and setting loop to true");
        StartCoroutine(MusicLoop());

        transitioning = false;
        yield return null;
    }
}
