using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    private int totalCoins;
    private int totalGems;

    private void OnEnable()
    {
        totalCoins = PlayerPrefs.GetInt(Utils.COIN, 0);
        totalGems = PlayerPrefs.GetInt(Utils.GEM, 0);

        UpdateUI();
    }

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        settingButton.onClick.AddListener(Setting);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(Play);
        settingButton.onClick.RemoveListener(Setting);
    }

    private void Play()
    {
        SceneController.LoadScene(new SceneProperties{ sceneName = Scenes.MainLevel, isAsync = true });
    }

    private void Setting()
    {

    }

    private void UpdateUI()
    {
        coinText.text = totalCoins.ToString();
        gemText.text = totalGems.ToString();
    }
}
