using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPause = false;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameWinMenu;
    public FloatSO Round;
    public FloatSO Score;

    private void Start()
    {
        gameOverMenu.SetActive(false);  
        gameWinMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(isPause) {
                Resume();
            }
            else { 
                Pause(); 
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void Restart()
    {
        Round.Value = 1;
        Score.Value = 0;
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void GameWin()
    {
        gameWinMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
