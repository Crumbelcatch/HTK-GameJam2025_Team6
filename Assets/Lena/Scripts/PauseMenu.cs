using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public static bool Paused = false;

     void Start()
    {
        Time.timeScale = 1f;
        Paused = false;
        pauseMenu.SetActive(false);

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Lvl_MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Play();
            }

            else
            {
                Stop();
            }
        }
    }

    public void Stop()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void Play()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}


