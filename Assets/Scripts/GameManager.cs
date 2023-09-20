using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public bool _isGameOver;
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private Button _resumeButton;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (_isGameOver == true)
            {
                _resumeButton.interactable = false;
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ResumePlay() 
    {   
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
