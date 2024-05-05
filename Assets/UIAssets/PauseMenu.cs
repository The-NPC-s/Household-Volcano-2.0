using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (gameOverUI.activeSelf == false))
        {
            if (GameIsPaused)
            {
                Resume();
            } else{
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("StartMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
