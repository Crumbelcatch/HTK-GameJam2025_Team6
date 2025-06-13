using UnityEngine;

public class MultipleSolve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private MultipleDrop container1;
    [SerializeField] private MultipleDrop container2;

    public void SolvedCheck()
    {
        if (container1.solved && container2.solved)
        {
            Debug.Log("Пазл решён! Уровень пройден.");
        }
    }
}
