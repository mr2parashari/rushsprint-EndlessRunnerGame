using UnityEngine;
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
}







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