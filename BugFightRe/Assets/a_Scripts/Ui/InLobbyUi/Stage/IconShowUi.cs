using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class IconShowUi : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    Image _IconSlot;
    public Image m_IconSlot { get => _IconSlot; }

    [SerializeField]
    UnityEvent _onClick;
    public UnityEvent m_OnClick { get => _onClick; }

    public void ShowIcon(Sprite icon)
    {
        m_IconSlot.sprite = icon;
        m_IconSlot.color = Color.white;
    }

    public void HideIcon()
    {
        m_IconSlot.sprite = null;
        m_IconSlot.color = new Color(0, 0, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClick?.Invoke();
    }

    public void AddAction(UnityAction unityAction)
    {
        m_OnClick.AddListener(unityAction);
    }

    public void RemoveAction(UnityAction unityAction)
    {
        m_OnClick.RemoveListener(unityAction);
    }

    public void RemoveAllAction()
    {
        m_OnClick.RemoveAllListeners();
    }


}
