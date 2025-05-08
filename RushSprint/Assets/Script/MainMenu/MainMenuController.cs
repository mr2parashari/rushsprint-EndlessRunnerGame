using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle musicToggle;
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

        soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        musicToggle.onValueChanged.AddListener(OnSoundToggleChanged);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(Play);
        settingButton.onClick.RemoveListener(Setting);

        soundToggle.onValueChanged.RemoveListener(OnSoundToggleChanged);
        musicToggle.onValueChanged.RemoveListener(OnMusicToggleChnaged);
    }

    private void Play()
    {
        SceneController.LoadScene(new SceneProperties{ sceneName = Scenes.MainLevel, isAsync = true });
    }

    private void Setting()
    {

    }

    private void OnMusicToggleChnaged(bool isOn)
    {
    }

    private void OnSoundToggleChanged(bool isOn)
    {
    }

    private void UpdateUI()
    {
        coinText.text = totalCoins.ToString();
        gemText.text = totalGems.ToString();
    }
}
