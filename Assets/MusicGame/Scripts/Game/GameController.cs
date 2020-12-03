using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using AssemblyCSharp.Assets.MusicGame;
using UniRx.Triggers;

public class GameController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject player;
    private List<RhythmPointInfoModel> rhythmPointInfoModels;
    private GameStatus gameStatus = new GameStatus();
    private int nextPointIndex = 0;
    private float currentPlayerSpeed = 0;
    // Start is called before the first frame update

    void Start()
    {
        gameStatus = GameStatus.Init;
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
                if (gameStatus == GameStatus.Init)
                {
                    nextPointIndex = 1;
                    gameStatus = GameStatus.WaitToStart;
                }
            });

        player.UpdateAsObservable()
            .Where(_ => gameStatus == GameStatus.Playing)
            .Where(_ => nextPointIndex > 0)
            .Subscribe(obj =>
            {
                movePlayer();
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
        float distance = Vector3.Distance(rhythmPointInfoModels[0].vector3, rhythmPointInfoModels[1].vector3);
        currentPlayerSpeed = distance / rhythmPointInfoModels[1].time;
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
    /// 移动Player
    /// </summary>
    private void movePlayer()
    {
        RhythmPointInfoModel nextPoint = rhythmPointInfoModels[nextPointIndex];
        float currentDistance = Vector3.Distance(player.transform.position, nextPoint.vector3);
        float moveDistance = currentPlayerSpeed * Time.deltaTime;
        float beyondDistance = moveDistance - currentDistance;
        if (beyondDistance >= 0)
        {
            player.transform.Translate(Vector3.forward * currentDistance);
            if(nextPointIndex + 1 > rhythmPointInfoModels.Count() - 1)
            {
                gameStatus = GameStatus.End;
                return;
            }
            nextPointIndex++;
            float beyondTime = beyondDistance / currentPlayerSpeed;
            nextPoint = rhythmPointInfoModels[nextPointIndex];
            float distance = Vector3.Distance(rhythmPointInfoModels[nextPointIndex - 1].vector3,
                nextPoint.vector3);
            currentPlayerSpeed = distance / (nextPoint.time - rhythmPointInfoModels[nextPointIndex - 1].time);
            Vector3 orientation = nextPoint.vector3 - player.transform.position;
            player.transform.Translate(orientation.normalized * currentPlayerSpeed * beyondTime);
        }
        else
        {
            Vector3 orientation = nextPoint.vector3 - player.transform.position;
            player.transform.Translate(orientation.normalized * moveDistance);
        }
    }

    /// <summary>
    /// 生成游戏路径
    /// </summary>
    private void PathCreator()
    {
    }
}
