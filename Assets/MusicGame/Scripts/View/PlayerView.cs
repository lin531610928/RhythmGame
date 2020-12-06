using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.Assets.MusicGame;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class PlayerView : MonoBehaviour
{
    private Sequence sequence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 移动Player
    /// </summary>
    public void movePlayer(MovePathInfoModel movePathInfo) {
        if (sequence != null && sequence.IsPlaying()) {
            sequence.Kill();
        }
        sequence = DOTween.Sequence();
        Vector3[] path = new Vector3[movePathInfo.lineRenderer.positionCount];
        movePathInfo.lineRenderer.GetPositions(path);
        sequence.Append(transform.DOPath(path, movePathInfo.duration).SetEase(Ease.Linear));
        sequence.Play();
    }
}
