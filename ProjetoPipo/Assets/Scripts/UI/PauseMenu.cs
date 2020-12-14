using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    [SerializeField]private TextMeshProUGUI pauseText;
    private string ptPause = "Jogo Pausado";
    private string usPause = "Game Paused";

    public bool pt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (pt) pauseText.SetText(ptPause);
        else pauseText.SetText(usPause);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("lOad");
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
