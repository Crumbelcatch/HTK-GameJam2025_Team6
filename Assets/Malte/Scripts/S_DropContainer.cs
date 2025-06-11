using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_DropContainer : MonoBehaviour, IDropHandler
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


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _this = this.gameObject;

    }

    //public void
    public void OnDrop(PointerEventData eventData)
    {
        if (slotFull) 
        {
            if (_lastObj.GetComponent<S_DragDrop>().stackTag == eventData.pointerDrag.GetComponent<S_DragDrop>().stackTag)
            {
                //Slot in the box
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;
                itemCount++;
                return;
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<S_DragDrop>()._lastDropPosition;
            return;
        }


        if (eventData.pointerDrag != null) 
        {
            //Debug.Log(eventData.pointerDrag.name + " Dropped");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;

            _lastObj = eventData.pointerDrag;

            eventData.pointerDrag.GetComponent<S_DragDrop>().DropRefrence(_this);

            Solve(_lastObj);
        }
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
    public void ThrowOutItem() 
    {
        // Set Item count -1, check if container is solved, if itemcount is 0 set slotFull to FALSE
    }
}
