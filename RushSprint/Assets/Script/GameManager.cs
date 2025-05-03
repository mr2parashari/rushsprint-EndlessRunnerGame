using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float score = 0;
    public int ringCount = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ringText;
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public Button mainMenuButton;
    public Button retryButton;
    public Button playAgainButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button pauseMainMenuButton;
    public bool showAd = false;
    private bool isPaused = false;


    #region Monobehaviour Methods

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


        // Load last saved score
        score = PlayerPrefs.GetFloat(Utils.SCORE, 0);
        Debug.Log("Score from PlayerPrefabs : " + score);
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

    private void OnDestroy()
    {
        // Remove Listeners
        mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        retryButton.onClick.RemoveListener(RestartGame);
        playAgainButton.onClick.RemoveListener(PlayAgain);
        pauseButton.onClick.RemoveListener(PauseGame);
        resumeButton.onClick.RemoveListener(ResumeGame);
        pauseMainMenuButton.onClick.RemoveListener(GoToMainMenu);
    }

    private void Update()
    {
        score += (10 * Time.deltaTime);
        AddScore();
    }

    #endregion


    #region Custom Methods

    public void AddScore()
    {
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

    [System.Obsolete]
    public void PlayerHit(GameObject obj)
    {
        Debug.Log($"Player hit detected , {obj.name} going to game over");
        GameOver();
    }

    [System.Obsolete]
    public void GameOver()
    {
        PlayerPrefs.SetFloat(Utils.SCORE, score);
        PlayerPrefs.Save();

        Debug.Log("Game Over function called");
        gameOverUI.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        Time.timeScale = 0f; // Pause the game

        // ADs
        if (showAd)
        {
            FindObjectOfType<AdMobManager>().ShowInterstitialAd();
            Debug.Log("Game Over! Interstitial Ad Played.");
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetFloat(Utils.SCORE, score); // Save before reload
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        PlayerPrefs.SetFloat(Utils.SCORE, score); // Save before menu
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Change this to your Main Menu scene name
    }

    public void PlayAgain()
    {
        PlayerPrefs.SetFloat(Utils.SCORE, score); // Save before reload
        PlayerPrefs.Save();

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
        scoreText.text = "Score: " + (int)score;
        ringText.text = "Rings: " + ringCount;
    }

    #endregion
}


/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int ringCount = 0;
    //public int maxAttempts = 3;
    private int currentAttempts;

    public Text scoreText;
    public Text ringText;
    //public Text attemptText;

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

        //currentAttempts = maxAttempts;
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
        //attemptText.text = "Attempts: " + currentAttempts;
    }
}*/