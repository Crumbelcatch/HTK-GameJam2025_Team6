using UnityEngine;
using UnityEngine.SceneManagement;

public class MultipleSolve : MonoBehaviour
{
    [SerializeField] private MultipleDrop[] containers;

    public void SolvedCheck()
    {
        foreach (var container in containers)
        {
            if (!container.solved)
            {
                Debug.Log("wrong.");
                return;
            }
        }

        Debug.Log("Solved!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UnlockLevel();
    }

    private void UnlockLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.GetInt("UnlockedIndex", PlayerPrefs.GetInt("UnlockedLevel") + 1);
            PlayerPrefs.Save();
        }
    }
}