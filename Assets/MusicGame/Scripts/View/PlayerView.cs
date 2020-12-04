using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.Assets.MusicGame;
using UnityEngine;
using UniRx;

public class PlayerView : MonoBehaviour
{
    private CompositeDisposable disposables = new CompositeDisposable();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        disposables.Dispose();
    }

    /// <summary>
    /// 移动Player
    /// </summary>
    public void movePlayer(RhythmPointInfoModel currentPoint, RhythmPointInfoModel nextPoint) {
        disposables.Clear();
        transform.position = currentPoint.vector3;
        float currentSpeed = Vector3.Distance(currentPoint.vector3, nextPoint.vector3) / (nextPoint.time - currentPoint.time);
        Vector3 orientation = nextPoint.vector3 - transform.position;
        Observable
            .EveryUpdate()
            .Subscribe(_ =>
            {
                float currentDistance = Vector3.Distance(transform.position, nextPoint.vector3);
                float moveDistance = (currentSpeed * Time.deltaTime);
                transform.Translate(orientation.normalized * Mathf.Min(currentDistance, moveDistance), Space.World);
            })
            .AddTo(disposables);
    }
}
