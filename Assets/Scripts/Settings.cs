using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _musicVolumeText, _sfxVolumeText;
    public Slider _musicVolumeSlider, _sfxVolumeSlider;
    public AudioMixer audioMixer;

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
        SaveSettings(musicVolume, PlayerPrefs.GetFloat("SfxVolume", 0f));
    }

    public void SetSfxVolume(float sfxVolume)
    {
        audioMixer.SetFloat("SfxVolume", sfxVolume);
        float normalizedValue = Mathf.InverseLerp(-80f, 0f, sfxVolume) * 100f;
        _sfxVolumeText.text = Mathf.Round(normalizedValue).ToString();
        _sfxVolumeSlider.value = sfxVolume;
        SaveSettings(PlayerPrefs.GetFloat("MusicVolume", 0f), sfxVolume);
    }

    public void Back()
    {
        SaveSettings(PlayerPrefs.GetFloat("MusicVolume", 0f), PlayerPrefs.GetFloat("SfxVolume", 0f));
        SceneManager.LoadScene(0);
    }

    public void SaveSettings(float musicVolume, float sfxVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        float sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0f);

        SetMusicVolume(musicVolume);
        SetSfxVolume(sfxVolume);
    }
}
