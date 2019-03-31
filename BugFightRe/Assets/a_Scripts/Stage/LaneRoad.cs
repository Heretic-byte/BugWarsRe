using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LaneRoad : MonoBehaviour
{
    Collider2D _coll2D;  
    [SerializeField]
    Nexus _leftNexus;
    [SerializeField]
    Nexus _rightNexus;
    [Header("DragFeedBackSRender")]
    [SerializeField]
    SpriteRenderer _sRenderer;

    Tweener _fadeTweener;

    public Collider2D  myColl2D { get => _coll2D; set => _coll2D = value; }
    public Nexus myLeftNexus { get => _leftNexus; set => _leftNexus = value; }
    public Nexus myRightNexus { get => _rightNexus; set => _rightNexus = value; }
    public SpriteRenderer mySRenderer { get => _sRenderer; set => _sRenderer = value; }

    private void Awake()
    {
        myColl2D = GetComponent<Collider2D>();
       
    }
    public void ShowFeedBackLaneRoad()
    {
        if(_fadeTweener!=null)
        {
            _fadeTweener.Complete();
        }
         _fadeTweener = mySRenderer.DOFade(1, 0.4f);
    }
    public void HideFeedBackLaneRoad()
    {
        if (_fadeTweener != null)
        {
            _fadeTweener.Complete();
        }
        _fadeTweener = mySRenderer.DOFade(0, 0.4f);
    }
}
