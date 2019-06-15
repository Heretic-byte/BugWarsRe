using UnityEngine;
using UnityEngine.UI;

public class StarShow : MonoBehaviour
{
    [SerializeField]
    Sprite _starSprite;
    Sprite m_StarSprite { get => _starSprite; }
    [SerializeField]
    Sprite _emptyStarSprite;
    Sprite m_EmptyStarSprite { get => _emptyStarSprite; }

    Image[] m_EmptyStar { get; set; }

    public void OnAwake()
    {
        m_EmptyStar = GetComponentsInChildren<Image>();
    }

    public void SetStarSequence(int starCount)
    {
        Debug.Log("starCount:" + starCount);

       
        for (int i=0; i < starCount;i++)
        {
            SetStar(i+1);
        }
    }

    public void SetStar(int index)
    {
        Debug.Log("index" + index);
        if(index<1)
        {
            index = 1;
        }
        if (index > 5)
        {
            index = 5;
        }

        m_EmptyStar[index].sprite = m_StarSprite;
    }


    public void SetEmptyStarSequence(int starMaxCount)
    {
        for (int i = starMaxCount; i > 1; i--)
        {
            SetEmptyStar(i);
        }
    }

    public void SetEmptyStar(int index)
    {
        m_EmptyStar[index].sprite = m_EmptyStarSprite;
    }


}
