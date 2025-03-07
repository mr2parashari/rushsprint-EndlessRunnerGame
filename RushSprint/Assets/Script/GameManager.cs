using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int ringCount = 0;
    public int maxAttempts = 3;
    private int currentAttempts;

    public Text scoreText;
    public Text ringText;
    public Text attemptText;

    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public Button mainMenuButton;
    public Button retryButton;
    public Button playAgainButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button pauseMainMenuButton;

    private bool isPaused = false;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f; // Ensure game runs normally on scene load
        currentAttempts = maxAttempts;
        UpdateUI();
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);

        // Assign button functions
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        retryButton.onClick.AddListener(RestartGame);
        playAgainButton.onClick.AddListener(PlayAgain);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        pauseMainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void AddRings(int amount)
    {
        ringCount += amount;
        UpdateUI();
    }

    public void LoseRings()
    {
        ringCount = 0;
        UpdateUI();
    }

    public void PlayerHit()
    {
        currentAttempts--;

        if (currentAttempts <= 0)
        {
            GameOver();
        }
        else
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (attemptText != null) attemptText.text = "Attempts: " + currentAttempts;
        if (ringText != null) ringText.text = "Rings: " + ringCount;
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Ensure normal speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayAgain()
    {
        gameOverUI.SetActive(false);
        currentAttempts = maxAttempts;
        ringCount = 0;
        UpdateUI();
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f; // Fix: Reset time scale before switching scenes
        SceneManager.LoadScene("MainMenu"); // Change to your actual main menu scene
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause game
        pauseMenuUI.SetActive(true);
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume game
        pauseMenuUI.SetActive(false);
    }
}


/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int ringCount = 0;
    public int maxAttempts = 3;
    private int currentAttempts;

    public Text scoreText;
    public Text ringText;
    public Text attemptText;

    public GameObject gameOverUI;
    public Button mainMenuButton;
    public Button retryButton;
    public Button playAgainButton;

    void Awake()
    {
        instance = this;
        currentAttempts = maxAttempts;
        UpdateUI();
        gameOverUI.SetActive(false);

        // Assign button functions
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        retryButton.onClick.AddListener(RestartGame);
        playAgainButton.onClick.AddListener(PlayAgain);
    }

    public void AddRings(int amount)
    {
        ringCount += amount;
        UpdateUI();
    }

    public void LoseRings()
    {
        ringCount = 0;
        UpdateUI();
    }

    public void PlayerHit()
    {
        currentAttempts--;

        if (currentAttempts <= 0)
        {
            GameOver();
        }
        else
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (attemptText != null) attemptText.text = "Attempts: " + currentAttempts;
        if (ringText != null) ringText.text = "Rings: " + ringCount;
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayAgain()
    {
        gameOverUI.SetActive(false);
        currentAttempts = maxAttempts;
        ringCount = 0;
        UpdateUI();
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Change "MainMenu" to your actual scene name
    }
}*/






/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int ringCount = 0;
    public int maxAttempts = 3;  // Max retries before game over
    private int currentAttempts;

    public Text scoreText;
    public Text ringText;
    public Text attemptText;  // Assign in Inspector
    public GameObject retryUI;
    public GameObject gameOverUI;

    void Awake()
    {
        instance = this;
        currentAttempts = maxAttempts;
        UpdateAttemptsUI();
        retryUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void AddRings(int amount)
    {
        ringCount += amount;
        ringText.text = "Rings: " + ringCount;
    }

    public void LoseRings()
    {
        ringCount = 0;
        ringText.text = "Rings: " + ringCount;
    }

    public void PlayerHit()
    {
        currentAttempts--;

        if (currentAttempts > 0)
        {
            retryUI.SetActive(true);
            UpdateAttemptsUI();
            Invoke("HideRetryUI", 2f);  // Hide retry UI after 2 seconds
        }
        else
        {
            GameOver();
        }
    }

    void HideRetryUI()
    {
        retryUI.SetActive(false);
    }

    void UpdateAttemptsUI()
    {
        attemptText.text = "Attempts: " + currentAttempts;
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        Invoke("RestartGame", 3f);  // Restart after 3 seconds
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}*/







/*using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int ringCount = 0;
    public Text scoreText;
    public Text ringText; // Assign in Inspector

    void Awake()
    {
        instance = this;
    }

    public void AddRings(int amount)
    {
        ringCount += amount;
        ringText.text = "Rings: " + ringCount;
    }

    public void LoseRings()
    {
        ringCount = 0; // Sonic loses all rings on hit
        ringText.text = "Rings: " + ringCount;
    }
}*/