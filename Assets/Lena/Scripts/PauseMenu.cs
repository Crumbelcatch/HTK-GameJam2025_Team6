using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public Button[] buttons;

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

    private void Awake()
    {
        //int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        //for (int i = 0; i < unlockedLevel; i++)
        //{
        //    buttons[i].interactable = true;
        //}
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Lvl_Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
    
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel") + 1);
            PlayerPrefs.Save();
        }
    }
}
