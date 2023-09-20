using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _timeText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Slider _thrusterSlider;
    private float _time = 0.0f;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);

        if (!GameObject.FindWithTag("GameManager").TryGetComponent(out _gameManager))
        {
            Debug.LogError("The Game Manager is NULL.");
        }
    }

    void Update()
    {
        CalculateTime();
    }

    public void CalculateTime()
    {
        if (_gameManager._isGameOver == false)
        {
            _time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_time / 60F);
            int seconds = Mathf.FloorToInt(_time - minutes * 60);

            _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLive)
    {
        _livesImage.sprite = _liveSprites[currentLive];
        if (currentLive == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.text = "WASTED";
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        _gameManager.ResumePlay();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateSprintBar(float currentThrusterTime)
    {
        _thrusterSlider.value = currentThrusterTime;
    }
}
