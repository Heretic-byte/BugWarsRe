using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPortraitManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _heroModels;
    private GameObject[] m_HeroModels { get => _heroModels; }

    GameObject m_ModelParent;

    Vector3 m_CreatePivot = new Vector3(100, 0, 0);

    [SerializeField]
    Transform _portraitCamtrans;
    Transform m_PortraitCamtrans { get => _portraitCamtrans; }

    Vector3 m_CamOffset = new Vector3(0, 1.5f, -10f);
    void Awake()
    {
        m_ModelParent = new GameObject("Model's P");
    }

    public void CreateHeroModel(int index)
    {
        Instantiate(m_HeroModels[index], m_CreatePivot, Quaternion.identity, m_ModelParent.transform);

        m_CreatePivot.x += 30f;
    }

    public void MoveCamera(int index)
    {
        Vector3 coord = m_CamOffset;
        coord.x = 100 + (index * 30);
        m_PortraitCamtrans.position = coord;
    }

    public void MoveCameraToZero()
    {
        m_PortraitCamtrans.position = new Vector3(-50, 0, 0);
    }
}
