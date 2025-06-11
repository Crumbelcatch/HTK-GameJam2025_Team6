using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemID;

    [HideInInspector] public Transform originalParent;

    private Vector3 startWorldPosition;
    private Vector2 startSizeDelta;
    private Vector3 startScale;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private ItemRowManager rowManager;
    public Transform dragLayer;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rowManager = FindObjectOfType<ItemRowManager>();

        if (dragLayer == null)
        {
            var dl = GameObject.Find("DragLayer");
            if (dl != null) dragLayer = dl.transform;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        if (dragLayer != null)
        {
            rectTransform.SetParent(dragLayer, worldPositionStays: false);
        }

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (rowManager != null)
        {
            rowManager.InsertItemAtPosition(this);
        }
        else
        {
            ReturnToOriginal();
        }
    }

    public void ReturnToOriginal()
    {
        rectTransform.SetParent(originalParent, worldPositionStays: true);
        rectTransform.position = startWorldPosition;
        rectTransform.sizeDelta = startSizeDelta;
        rectTransform.localScale = startScale;
    }
}