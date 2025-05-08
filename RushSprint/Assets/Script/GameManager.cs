using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float score = 0;
    public int coin = 0;
    public int gem = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
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
        coin = PlayerPrefs.GetInt(Utils.COIN, 0);
        gem = PlayerPrefs.GetInt(Utils.GEM, 0);
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

    public void AddCoins(int amount)
    {
        coin += amount;
        UpdateUI();
    }

    public void AddGems(int amount)
    {
        gem += amount;
        UpdateUI();
    }

    public void LoseCoins()
    {
        coin = 0;
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
        SaveData();

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
        SaveData();

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SaveData();

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Change this to your Main Menu scene name
    }

    public void PlayAgain()
    {
        SaveData();

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
        coinText.text = "Coin: " + coin;
        gemText.text = "Gem: " + gem;
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(Utils.SCORE, score); // Save before reload
        PlayerPrefs.SetInt(Utils.COIN, coin); // Save before reload
        PlayerPrefs.SetInt(Utils.GEM, gem); // Save before reload
        PlayerPrefs.Save();
    }

    #endregion
}