using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_DropContainer : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;
    private GameObject _this;
    public bool slotFull;
    public bool solved;

    [SerializeField] private GameObject _correctItem;

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
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<S_DragDrop>()._lastDropPosition;
            return;
        }


        if (eventData.pointerDrag != null) 
        {
            //Debug.Log(eventData.pointerDrag.name + " Dropped");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;

            eventData.pointerDrag.GetComponent<S_DragDrop>().DropRefrence(_this);

            if (eventData.pointerDrag == _correctItem)
            {
                solved = true;
                Solve();
            }
            else
            {
                solved = false;
                Solve();
            }
        }
    }

    public void Solve() 
    {
        GetComponentInParent<S_PuzzleManager>().SolvedCheck();
    }
}
