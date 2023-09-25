using System.Collections.Generic;
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
    [SerializeField]
    private GameObject _settingsPanel, _confirmationPanel, _backgroundPanel;
    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;

    public AudioMixer audioMixer;

    Resolution[] resolutions;

    // Default values
    private readonly bool defaultFullScreen = true;
    private readonly float minVolume = -80f;
    private readonly float maxVolume = 0f;
    private readonly float defaultMusicVolume = 0f;
    private readonly float defaultSfxVolume = 0f;
    private readonly float defaultMasterVolume = 0f;

    void Start()
    {
        GetResolutions();
        LoadSettings();
    }

    public void SetDropdownMenu(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        SetResolution(resolution.width, resolution.height);
        _resolutionDropdown.value = resolutionIndex;
    }

    private void SetResolution(int resolutionWidth, int resolutionHeight)
    {
        Screen.SetResolution(resolutionWidth, resolutionHeight, Screen.fullScreen);
    }

    private void GetResolutions()
    {
        _resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
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
        int resolutionWidth = resolutions[_resolutionDropdown.value].width;
        int resolutionHeight = resolutions[_resolutionDropdown.value].height;
        SaveSettings(isFullScreen, musicVolume, sfxVolume, masterVolume, resolutionWidth, resolutionHeight);
        SceneManager.LoadScene(0);
    }

    public void OpenConfirmationPanel()
    {
        _settingsPanel.SetActive(false);
        _backgroundPanel.SetActive(true);
        _confirmationPanel.SetActive(true);
    }

    public void CloseConfirmationPanel()
    {
        _settingsPanel.SetActive(true);
        _backgroundPanel.SetActive(false);
        _confirmationPanel.SetActive(false);
    }

    public void ResetToDefaults()
    {
        int maxResolutionIndex = resolutions.Length - 1;
        SetDropdownMenu(maxResolutionIndex);

        SetFullScreen(defaultFullScreen);
        SetMusicVolume(defaultMusicVolume);
        SetSfxVolume(defaultSfxVolume);
        SetMasterVolume(defaultMasterVolume);
        CloseConfirmationPanel();
    }

    public void SaveSettings(bool isFullScreen, float musicVolume, float sfxVolume, float masterVolume, int resolutionWidth, int resolutionHeight)
    {
        PlayerPrefs.SetInt("FullScreen", (isFullScreen ? 1 : 0));
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetInt("ScreenWidth", resolutionWidth);
        PlayerPrefs.SetInt("ScreenHeight", resolutionHeight);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        int fullScreen = PlayerPrefs.GetInt("FullScreen", 1);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0f);
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        int resolutionWidth = PlayerPrefs.GetInt("ScreenWidth", Screen.currentResolution.width);
        int resolutionHeight = PlayerPrefs.GetInt("ScreenHeight", Screen.currentResolution.height);

        _fullScreenToggle.isOn = fullScreen == 1;

        SetResolution(resolutionWidth, resolutionHeight);
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
