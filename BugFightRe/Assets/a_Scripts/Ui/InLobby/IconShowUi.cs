using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class IconShowUi : MonoBehaviour
{
    [SerializeField]
    Image _IconSlot;
    public Image m_IconSlot { get => _IconSlot; }

 
    public void ShowIcon(Sprite icon)
    {
        m_IconSlot.sprite = icon;
        m_IconSlot.DOFade(1, 0);
    }
    public void HideIcon()
    {
        m_IconSlot.DOFade(0, 0);
    }


}
