using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp.Assets.MusicGame;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public AudioSource musicControl;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 生成移动路径
    /// </summary>
    public List<MovePathInfoModel> GetMovePaths(List<RhythmPointInfoModel> infos)
    {
        List<MovePathInfoModel> movePathInfos = new List<MovePathInfoModel>();
        for (int i = 0; i < infos.Count(); i++)
        {
            switch (infos[i].pointType) {
                case PointType.BezierMiddle:
                case PointType.End:
                    break;
                case PointType.BezierStart:
                    LineRenderer bezierCurve = GetBezierCurve(infos[i].vector3, infos[i + 1].vector3, infos[i + 2].vector3);
                    movePathInfos.Add(new MovePathInfoModel(
                        PathType.Line,
                        bezierCurve,
                        infos[i + 2].time - infos[i].time,
                        infos[i + 1].vector3));
                    break;
                default:
                    LineRenderer line = GetLine(infos[i].vector3, infos[i + 1].vector3);
                    movePathInfos.Add(new MovePathInfoModel(
                        PathType.Line,
                        line,
                        infos[i + 1].time - infos[i].time));
                    break;
            }
        }
        return movePathInfos;
    }

    /// <summary>
    /// 生成直线
    /// </summary>
    /// <param name="point1">开始点</param>
    /// <param name="point2">结束点</param>
    public LineRenderer GetLine(Vector3 point1, Vector3 point2)
    {
        LineRenderer lineRenderer = ((GameObject)Instantiate(Resources.Load(Prefabs.LinePath))).GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        Vector3[] vectors = { point1, point2 };
        lineRenderer.SetPositions(vectors);
        return lineRenderer;
    }

    public LineRenderer GetBezierCurve(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        LineRenderer lineRenderer = ((GameObject)Instantiate(Resources.Load(Prefabs.LinePath))).GetComponent<LineRenderer>();
        int vertexCount = 30;
        List<Vector3> pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            Vector3 tangentLineVertex1 = Vector3.Lerp(point1, point2, ratio);
            Vector3 tangentLineVectex2 = Vector3.Lerp(point2, point3, ratio);
            Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
            pointList.Add(bezierPoint);
        }
        pointList.Add(point3);
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
        return lineRenderer;
    }

    /// <summary>
    /// 设置基本节拍点
    /// </summary>
    /// <param name="infoModel">节拍点信息</param>
    public GameObject GetBaseRhythmPoint(RhythmPointInfoModel infoModel)
    {
        GameObject gameObject = (GameObject)Instantiate(Resources.Load(Prefabs.BaseRhythmPoint));
        gameObject.transform.position = infoModel.vector3;
        return gameObject;
    }

    /// <summary>
    /// 设置Player坐标
    /// </summary>
    /// <param name="vector">坐标</param>
    public void SetPlayerPosition(Vector3 vector) {
        player.transform.position = vector;
    }

    public void SetMusic() {
        musicControl.clip = (AudioClip)Instantiate(Resources.Load(Music.Demo));
    }

    public void StartGame()
    {
        musicControl.Play();
    }

    public void SetAnimation(List<RhythmDecisionInfoModel> infos)
    {
        foreach (RhythmDecisionInfoModel info in infos)
        {
            if (info.type == NoteType.Base
                && musicControl.time + 1.0f >= info.time
                && info.gameObject != null
                && info.detectStatus == DecisionStatus.WaitToStart) {
                info.detectStatus = DecisionStatus.Animating;
                info.gameObject.GetComponentInChildren<Animation>().Play();
            }
        }
    }

    public float GetCurrentMusicTime()
    {
        return musicControl.time;
    }
}
