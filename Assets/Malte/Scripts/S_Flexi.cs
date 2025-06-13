using UnityEngine;
using UnityEngine.UI;

public class S_Flexi : MonoBehaviour
{
    private RectTransform _this;
    private Image _child;

    private void Awake()
    {
        _this = GetComponent<RectTransform>();

        ResizeContainer();
    }
    
    public void ResizeContainer()
    {
        _child = _this.GetComponentInChildren<Image>();
        _this.sizeDelta = new Vector2(_child.rectTransform.rect.width, _child.rectTransform.rect.height);
    }
}
