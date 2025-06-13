using UnityEngine;

public class MultipleSolve : MonoBehaviour
{
    [SerializeField] private MultipleDrop container1;
    [SerializeField] private MultipleDrop container2;

    public void SolvedCheck()
    {
        if (container1 != null && container2 != null)
        {
            if (container1.solved && container2.solved)
            {
                Debug.Log("Solved");
            }
        }
    }
}