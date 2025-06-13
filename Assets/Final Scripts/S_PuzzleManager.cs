using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PuzzleManager : MonoBehaviour
{
    public GameObject[] Puzzles;
    public int puzzleCount;

    private void Awake()
    {
        puzzleCount = 0;
    }

    public void SolvedCheck()
    {
        foreach (GameObject puzzle in Puzzles)
        {
            if (puzzle.GetComponent<S_DropContainer>().solved)
            {
                puzzleCount++;
            }
        }
        int arrayLength = Puzzles.Count();
        if (puzzleCount == arrayLength) 
        {
            Debug.Log("Puzzle solved. Now leave.");
            //Puzzle Solved! Go next goes here ->
            //Unlock next Level in Level Select
            UnlockLevel();

            //Load next Level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        
        puzzleCount = 0;
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
