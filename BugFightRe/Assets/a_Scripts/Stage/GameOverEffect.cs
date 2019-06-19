using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverEffect : MonoBehaviour
{
    //마지막으로 죽은 타워 위치로 카메라 이동
    //깃발 내려옴
    //플레이어 승리시 승리 표시

    public Text m_WinLose;
    public Transform m_Flag;
    public Transform m_TargetTrans;
    public float m_CamMoveTime = 0.4f;
    public float m_FlagDur=1f;
    public float m_TextFadeDur = 1f;
    [SerializeField]
    CameraMover m_CameraMover;

    Vector3 m_CamTarget;

    public void SetCamTarget(Vector3 vector3)
    {
        m_CamTarget = vector3;
        m_CameraMover.RemoveTickFromManager();
        m_CameraMover.transform.DOMoveX(m_CamTarget.x, m_CamMoveTime);
    }

    public void GameOver(string winLose)
    {
        m_WinLose.text = winLose+"\r\n"+"로비씬으로 돌아갑니다.";

        Sequence sequence = DOTween.Sequence();
        sequence.Append(m_Flag.DOMove(m_TargetTrans.position, m_FlagDur))
            .Append(m_WinLose.DOFade(1, m_TextFadeDur))
            .OnComplete(LoadScene);
    }

    void LoadScene()
    {
        mySceneManager.GetInstance.MoveScene(1);
    }

}
