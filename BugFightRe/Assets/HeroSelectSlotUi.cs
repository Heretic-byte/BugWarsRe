using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HeroSelectSlotUi : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    UnityEvent _onClick;
    public UnityEvent OnClick { get => _onClick; set => _onClick = value; }

    [SerializeField]
    Image _iconImage;
    public Image m_IconImage { get => _iconImage;}

    public void SetHeroData()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
    public void ShowSlot()
    {

    }
    public void HideSlot()
    {

    }
     void SetIconSprite(Sprite icon)
    {
        m_IconImage.color = Color.white;
        m_IconImage.sprite = icon;
    }
     void SetNullIcon()
    {
        m_IconImage.sprite = null;
        m_IconImage.color = new Color(0, 0, 0, 0);
    }

    //눌러서 장탈착

    //눌리면 눌린표시필요
}
