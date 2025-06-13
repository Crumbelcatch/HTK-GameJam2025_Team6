using FMODUnity;
using FMOD.Studio;

using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PuzzleManager : MonoBehaviour
{

    FMOD.Studio.EventInstance _completedEvent;

    public GameObject[] Puzzles;
    public int puzzleCount;

    [Header("Audio Settings")]
    [SerializeField] public FMODUnity.EventReference _completedSound;

    private void Awake()
    {
        puzzleCount = 0;
    }

    private void Update()
    {
        if (_completedEvent.isValid())
        {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            _completedEvent.getPlaybackState(out playbackState);
            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
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
            //Debug.Log("Puzzle solved. Now leave.");
            //Puzzle Solved! Go next goes here ->
            //Unlock next Level in Level Select
            UnlockLevel();


            _completedEvent = FMODUnity.RuntimeManager.CreateInstance(_completedSound);

            _completedEvent.start();

            //Load next Level
  
            return;
        }
        
        puzzleCount = 0;
    }

    private void UnlockLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            //FMODUnity.RuntimeManager.PlayOneShot(_completedSound, transform.position);
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.GetInt("UnlockedIndex", PlayerPrefs.GetInt("UnlockedLevel") + 1);
            PlayerPrefs.Save();
        }
    }
}
