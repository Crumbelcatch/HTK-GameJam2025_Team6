using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleDrop : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;
    private GameObject _this;
    private GameObject _lastObj;
    public bool slotFull;
    public bool solved;
    public int itemCount;



    [Header("Required to solve")]
    [SerializeField] private GameObject _correctItem;
    [SerializeField] public int reqItemCount;
    public List<GameObject> correctItems = new List<GameObject>();
    private List<GameObject> currentItems = new List<GameObject>();

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _this = this.gameObject;

    }

    //public void
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;

        if (droppedItem == null)
        {
            Debug.LogWarning("Dropped item is null");
            return;
        }

        S_DragDrop dragComponent = droppedItem.GetComponent<S_DragDrop>();
        if (dragComponent == null)
        {
            Debug.LogWarning("Dropped item does not have S_DragDrop component");
            return;
        }

        droppedItem.transform.SetParent(transform);
        droppedItem.GetComponent<RectTransform>().anchoredPosition =
            transform.InverseTransformPoint(eventData.position);

        currentItems.Add(droppedItem);
        itemCount++;

        dragComponent.DropRefrence(gameObject);
        Solve();
    }

    public void Solve(GameObject item)
    {

        if (item == _correctItem && itemCount == reqItemCount)
        {
            solved = true;
            GetComponentInParent<S_PuzzleManager>().SolvedCheck();
        }
        else
        {
            solved = false;
            GetComponentInParent<S_PuzzleManager>().SolvedCheck();
        }
    }


    //Call this if Item is dragged off of this spot
    public void ThrowOutItem(GameObject item)
    {
        itemCount--;
        currentItems.Remove(item);

        if (itemCount <= 0)
        {
            slotFull = false;
        }

        Solve(); // пересчитать
    }

    public void Solve()
    {
        if (currentItems.Count != reqItemCount)
        {
            solved = false;
            GetComponentInParent<S_PuzzleManager>().SolvedCheck();
            return;
        }

        foreach (var item in currentItems)
        {
            if (!correctItems.Contains(item))
            {
                solved = false;
                GetComponentInParent<S_PuzzleManager>().SolvedCheck();
                return;
            }
        }

        solved = true;
        GetComponentInParent<S_PuzzleManager>().SolvedCheck();
    }
}
