using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = true;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject GameOverUI;
    public static PauseMenu Instance;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (IsGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
