using UnityEngine;
using UnityEngine.UI;
public class QuestStarStageShow : MonoBehaviour
{
    //StageButton ref

    [SerializeField]
    GameObject[] _starObjs;
    public GameObject[] m_StarObjs { get => _starObjs;}


    private void Awake()
    {
       foreach(var AA in m_StarObjs)
        {
            AA.SetActive(false);
        }
    }
   
    public void ShowStar(int count)
    {
        for(int i=0; i< count;i++)
        {
            m_StarObjs[i].SetActive(true);
        }
    }


}
