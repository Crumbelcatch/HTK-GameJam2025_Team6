using System.Linq;
using UnityEngine;

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
            return;
        }
        
        puzzleCount = 0;
    }
}
