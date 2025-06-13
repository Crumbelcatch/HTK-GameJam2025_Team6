using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.IK;
using FMOD;
using JetBrains.Annotations;


public class S_DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    #region Variables
    private RectTransform _rTransform;
    private CanvasGroup _canvasGroup;
    private GameObject _lastDropContainer;
    public Vector3 _lastDropPosition;

    [Header("Refrences to Fill")]
    [SerializeField] private Canvas _canvasRef;
    [SerializeField] public String stackTag;

    [Header("Audio Settings")]
    [SerializeField] public FMODUnity.EventReference _pickup;
    [SerializeField] public FMODUnity.EventReference _drop;
    #endregion

    private void Awake()
    {
        _rTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _lastDropContainer = null;
        _lastDropPosition = _rTransform.anchoredPosition;

    }
    

    public void OnClick(PointerEventData eventData)
    {
        return;
    }

    // On Begin of Drag   
    public void OnBeginDrag(PointerEventData eventData) 
    {

        _lastDropPosition = _rTransform.anchoredPosition;
        FMODUnity.RuntimeManager.PlayOneShot(_pickup, _lastDropPosition);
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
        if (_lastDropContainer != null)
        {
            ClearDropContainer();
        }

    }

    // During Drag
    public void OnDrag(PointerEventData eventData) 
    { 
        _rTransform.anchoredPosition += eventData.delta / _canvasRef.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        FMODUnity.RuntimeManager.PlayOneShot(_drop, transform.position);
    }

    public void OnDrop(PointerEventData eventData) 
    {
        if (stackTag != string.Empty && stackTag == eventData.pointerDrag.GetComponent<S_DragDrop>().stackTag)
        {
            if (_lastDropContainer != null)
            {
                _lastDropContainer.GetComponent<S_DropContainer>().OnDrop(eventData);
                eventData.pointerDrag.GetComponent<S_DragDrop>().DropRefrence(_lastDropContainer);
            }
            return;
        }
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<S_DragDrop>()._lastDropPosition;
        return;
    }

    public void ClearDropContainer()
    {
        _lastDropContainer.GetComponent<S_DropContainer>().ThrowOutItem();
        _lastDropContainer = null;
    }

    public void DropRefrence(GameObject dropContainer) 
    {
        _lastDropContainer = dropContainer;
        _lastDropContainer.GetComponent<S_DropContainer>().slotFull = true;
    }


}