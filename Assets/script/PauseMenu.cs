using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseMeunUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauseMeunUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPause = false;
    }
    public void Pause()
    {
        PauseMeunUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPause = true;
    }
    public void MainMenu()
    {
        GameIsPause = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
