using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _musicVolumeText, _sfxVolumeText, _masterVolumeText;
    [SerializeField]
    private Slider _musicVolumeSlider, _sfxVolumeSlider, _masterVolumeSlider;
    [SerializeField]
    private Toggle _fullScreenToggle;

    public AudioMixer audioMixer;

    // Default values
    private readonly bool defaultFullScreen = true;
    private readonly float minVolume = -80f;
    private readonly float maxVolume = 0f;
    private readonly float defaultMusicVolume = 0f;
    private readonly float defaultSfxVolume = 0f;
    private readonly float defaultMasterVolume = 0f;

    void Start()
    {
        LoadSettings();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        _fullScreenToggle.isOn = isFullScreen == true;
        _fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
        UpdateUIElements(musicVolume, _musicVolumeText, _musicVolumeSlider);
    }

    public void SetSfxVolume(float sfxVolume)
    {
        audioMixer.SetFloat("SfxVolume", sfxVolume);
        UpdateUIElements(sfxVolume, _sfxVolumeText, _sfxVolumeSlider);
    }

    public void SetMasterVolume(float masterVolume)
    {
        audioMixer.SetFloat("MasterVolume", masterVolume);
        UpdateUIElements(masterVolume, _masterVolumeText, _masterVolumeSlider);
    }

    public void Back()
    {
        bool isFullScreen = Screen.fullScreen;
        float musicVolume = _musicVolumeSlider.value;
        float sfxVolume = _sfxVolumeSlider.value;
        float masterVolume = _masterVolumeSlider.value;
        SaveSettings(isFullScreen, musicVolume, sfxVolume, masterVolume);
        SceneManager.LoadScene(0);
    }

    public void ResetToDefaults()
    {
        SetFullScreen(defaultFullScreen);
        SetMusicVolume(defaultMusicVolume);
        SetSfxVolume(defaultSfxVolume);
        SetMasterVolume(defaultMasterVolume);
    }

    public void SaveSettings(bool isFullScreen, float musicVolume, float sfxVolume, float masterVolume)
    {
        PlayerPrefs.SetInt("FullScreen", (isFullScreen ? 1 : 0));
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {

        int fullScreen = PlayerPrefs.GetInt("FullScreen", 1);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0f);
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        
        _fullScreenToggle.isOn = fullScreen == 1;

        SetFullScreen(fullScreen == 1);
        SetMusicVolume(musicVolume);
        SetSfxVolume(sfxVolume);
        SetMasterVolume(masterVolume);
    }

    private void UpdateUIElements(float volume, TextMeshProUGUI text, Slider slider)
    {
        float normalizedValue = Mathf.InverseLerp(minVolume, maxVolume, volume) * 100f;
        text.text = Mathf.Round(normalizedValue).ToString();
        slider.value = volume;
    }
}
