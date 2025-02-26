using UnityEngine;
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
}