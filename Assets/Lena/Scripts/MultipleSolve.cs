using UnityEngine;
using UnityEngine.SceneManagement;

public class MultipleSolve : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private MultipleDrop[] containers;
    FMOD.Studio.EventInstance _completedEvent;

    [Header("Audio Settings")]
    [SerializeField] public FMODUnity.EventReference _completedSound;

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
        foreach (var container in containers)
        {
            if (!container.solved)
            {
                Debug.Log("wrong.");
                return;
            }
        }

        Debug.Log("Solved!");

        _completedEvent = FMODUnity.RuntimeManager.CreateInstance(_completedSound);

        _completedEvent.start();

        UnlockLevel();

    }

    private void UnlockLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(_completedSound, transform.position);
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.GetInt("UnlockedIndex", PlayerPrefs.GetInt("UnlockedLevel") + 1);
            PlayerPrefs.Save();
        }
    }
}