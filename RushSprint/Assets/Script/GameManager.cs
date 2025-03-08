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
        if (instance == null)
        {
            instance = this;
        }

        currentAttempts = maxAttempts;
        UpdateUI();
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);

        // Assign button listeners
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        retryButton.onClick.AddListener(RestartGame);
        playAgainButton.onClick.AddListener(PlayAgain);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        pauseMainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
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

        if (currentAttempts > 0)
        {
            UpdateUI();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Change this to your Main Menu scene name
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseMenuUI.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenuUI.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        ringText.text = "Rings: " + ringCount;
        attemptText.text = "Attempts: " + currentAttempts;
    }
}


























/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverUI;
    public GameObject pauseMenuUI;

    public Text bulletText;
    public Text ringText;
    public Text attemptText;

    public int maxAttempts = 3;
    private int currentAttempts;

    private int maxBullets = 10;
    private int currentBullets = 8;
    private int autoReloadThreshold = 2;

    public int ringCount = 0;

    private bool isGamePaused = false;

    void Awake()
    {
        instance = this;
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        currentAttempts = maxAttempts;
        UpdateUI();
    }

    void UpdateUI()
    {
        bulletText.text = currentBullets + "/" + maxBullets;
        ringText.text = "Rings: " + ringCount;
        attemptText.text = "Attempts: " + currentAttempts;
    }

    // 📌 Player gets hit, loses an attempt
    public void PlayerHit()
    {
        currentAttempts--;
        UpdateUI();

        if (currentAttempts <= 0)
        {
            GameOver();
        }
    }

    // 📌 Player collects rings
    public void AddRings(int amount)
    {
        ringCount += amount;
        UpdateUI();
    }

    // 📌 Player loses all rings
    public void LoseRings()
    {
        ringCount = 0;
        UpdateUI();
    }

    // 📌 Gun system: Fire bullets
    public void FireBullet()
    {
        if (currentBullets > 0)
        {
            currentBullets--;
            UpdateUI();

            if (currentBullets <= autoReloadThreshold)
            {
                Invoke("AutoReload", 1.5f);
            }
        }
    }

    // 📌 Auto-reload bullets
    void AutoReload()
    {
        currentBullets = maxBullets;
        UpdateUI();
    }

    // 📌 Player collects bullets
    public void CollectBullets(int amount)
    {
        currentBullets = Mathf.Min(currentBullets + amount, maxBullets);
        UpdateUI();
    }

    // 📌 Game Over System
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    // 📌 Pause Menu System
    public void PauseGame()
    {
        isGamePaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
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
}*/