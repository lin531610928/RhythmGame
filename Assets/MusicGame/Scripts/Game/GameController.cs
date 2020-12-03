using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using AssemblyCSharp.Assets.MusicGame;

public class GameController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject player;
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<RhythmPointInfoModel> rhythmPointInfoModels;
    private GameStatus gameStatus = new GameStatus();
    private int nextPointIndex = 0;
    // Start is called before the first frame update

    void Start()
    {
        gameStatus = GameStatus.WaitToStart;
        rhythmPointInfoModels = (List<RhythmPointInfoModel>)GameDataManager.GetInstance().GetRhythmPointList().Clone();

        rhythmPointInfoModels
            .ObserveEveryValueChanged(e => e)
            .Where(e => e != null)
            .Subscribe(infos =>
            {
                Vector3[] vector3s = infos.Select(e => e.vector3).ToArray();
                lineRenderer.positionCount = vector3s.Count();
                lineRenderer.SetPositions(vector3s);
                foreach (RhythmPointInfoModel info in infos)
                {
                    switch (info.type)
                    {
                        case PointType.Start:
                            player.transform.position = info.vector3;
                            break;
                        case PointType.BaseRhythm:
                            setBaseRhythmPoint(info);
                            break;
                        default:
                            break;
                    }
                }
                if(gameStatus == GameStatus.Init)
                {
                    gameStatus = GameStatus.WaitToStart;
                }
            });

        player.ObserveEveryValueChanged(e => e)
            .Where(_ => gameStatus == GameStatus.Playing)
            .Where(_ => nextPointIndex > 0)
            .Subscribe(obj =>
            {
                
            })
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => gameStatus == GameStatus.WaitToStart)
            .First(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                StartGame();
            })
            .AddTo(this);

        //gameObjects
        //    .ObserveEveryValueChanged(e => e.Select(x => x.transform.localPosition).ToArray())
        //    .Subscribe(vectors =>
        //    {
        //        lineRenderer.positionCount = vectors.Count();
        //        lineRenderer.SetPositions(vectors);
        //    });
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    private void StartGame()
    {
        GameObject startTips = GameObject.FindGameObjectWithTag("StartTips");
        startTips.SetActive(false);
        Destroy(startTips);
        gameStatus = GameStatus.Playing;
    }

    /// <summary>
    /// 设置基本节拍点
    /// </summary>
    /// <param name="infoModel">节拍点信息</param>
    private void setBaseRhythmPoint(RhythmPointInfoModel infoModel)
    {
        GameObject gameObject = (GameObject)Instantiate(Resources.Load(Prefabs.BaseRhythmPoint));
        gameObject.transform.position = infoModel.vector3;
    }

    /// <summary>
    /// 生成游戏路径
    /// </summary>
    private void PathCreator()
    {
    }
}
