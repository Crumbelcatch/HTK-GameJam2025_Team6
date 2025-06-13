using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleDrop : MonoBehaviour, IDropHandler
{
    [Header("Settings")]
    public List<GameObject> correctItems = new List<GameObject>();
    public int reqItemCount = 1;

    private List<GameObject> currentItems = new List<GameObject>();
    public bool solved = false;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem == null) return;

        MultipleDrag dragComponent = droppedItem.GetComponent<MultipleDrag>();
        if (dragComponent == null) return;

        // �������� ������ ����������
        droppedItem.transform.SetParent(transform);
        droppedItem.GetComponent<RectTransform>().anchoredPosition =
            transform.InverseTransformPoint(eventData.position);

        currentItems.Add(droppedItem);
        dragComponent.SetDropReference(gameObject);

        Solve();
    }

    public void ThrowOutItem(GameObject item)
    {
        if (currentItems.Contains(item))
        {
            currentItems.Remove(item);
            Solve();
        }
    }

    public void Solve()
    {
        // ���������� ������ ����� ���������
        if (currentItems.Count != reqItemCount)
        {
            solved = false;
            GetComponentInParent<MultipleSolve>().SolvedCheck();
            return;
        }

        // ��� ������� �������� ������ ���� � ������ ����������
        foreach (var item in currentItems)
        {
            if (!correctItems.Contains(item))
            {
                solved = false;
                GetComponentInParent<MultipleSolve>().SolvedCheck();
                return;
            }
        }

        // �� ���������
        solved = true;
        GetComponentInParent<MultipleSolve>().SolvedCheck();
    }
}
