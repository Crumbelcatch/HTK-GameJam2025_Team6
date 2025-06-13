using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform _rTransform;
    private CanvasGroup _canvasGroup;
    private GameObject _lastDropContainer;
    public Vector3 _lastDropPosition;

    [Header("References")]
    [SerializeField] private Canvas _canvasRef;
    [SerializeField] public string stackTag;

    private void Awake()
    {
        _rTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _lastDropPosition = _rTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastDropPosition = _rTransform.anchoredPosition;
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;

        // Убираем из прошлого контейнера
        if (_lastDropContainer != null)
        {
            var drop = _lastDropContainer.GetComponent<MultipleDrop>();
            if (drop != null)
            {
                drop.ThrowOutItem(gameObject);
            }
            _lastDropContainer = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rTransform.anchoredPosition += eventData.delta / _canvasRef.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        // Если не попал в контейнер — возвращаем
        if (_lastDropContainer == null)
        {
            _rTransform.anchoredPosition = _lastDropPosition;
        }
    }

    public void SetDropReference(GameObject dropContainer)
    {
        _lastDropContainer = dropContainer;
    }
}
