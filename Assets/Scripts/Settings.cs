using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TextMeshProUGUI _musicVolumeText, _sfxVolumeText, _masterVolumeText;
    public Slider _musicVolumeSlider, _sfxVolumeSlider, _masterVolumeSlider;
    public AudioMixer audioMixer;

    // Default values
    private readonly float defaultMusicVolume = 0f;
    private readonly float defaultSfxVolume = 0f;
    private readonly float defaultMasterVolume = 0f;

    private void Start()
    {
        LoadSettings();
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", musicVolume);
        float normalizedValue = Mathf.InverseLerp(-80f, 0f, musicVolume) * 100f;
        _musicVolumeText.text = Mathf.Round(normalizedValue).ToString();
        _musicVolumeSlider.value = musicVolume;
    }

    public void SetSfxVolume(float sfxVolume)
    {
        audioMixer.SetFloat("SfxVolume", sfxVolume);
        float normalizedValue = Mathf.InverseLerp(-80f, 0f, sfxVolume) * 100f;
        _sfxVolumeText.text = Mathf.Round(normalizedValue).ToString();
        _sfxVolumeSlider.value = sfxVolume;
    }

    public void SetMasterVolume(float masterVolume)
    {
        audioMixer.SetFloat("MasterVolume", masterVolume);
        float normalizedValue = Mathf.InverseLerp(-80f, 0f, masterVolume) * 100f;
        _masterVolumeText.text = Mathf.Round(normalizedValue).ToString();
        _masterVolumeSlider.value = masterVolume;
    }

    public void Back()
    {
        float musicVolume = _musicVolumeSlider.value;
        float sfxVolume = _sfxVolumeSlider.value;
        float masterVolume = _masterVolumeSlider.value;
        SaveSettings(musicVolume, sfxVolume, masterVolume);
        SceneManager.LoadScene(0);
    }

    public void ResetToDefaults()
    {
        SetMusicVolume(defaultMusicVolume);
        SetSfxVolume(defaultSfxVolume);
        SetMasterVolume(defaultMasterVolume);
    }

    public void SaveSettings(float musicVolume, float sfxVolume, float masterVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0f);
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);

        SetMusicVolume(musicVolume);
        SetSfxVolume(sfxVolume);
        SetMasterVolume(masterVolume);
    }
}
