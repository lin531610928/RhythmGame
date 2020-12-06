using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp.Assets.MusicGame;
using DG.Tweening;
using UnityEngine;
using UniRx;
using System;

public class Test : MonoBehaviour
{
    public GameObject player;
    public GameObject empty;
    private LineRenderer line;
    public LineRenderer line2;
    // Start is called before the first frame update
    List<Vector3> list = new List<Vector3>();
    float speed = 0;
    void Start()
    {
        Vector3 point1 = new Vector3(0, -5, 15);
        Vector3 point2 = new Vector3(10, 0, 10);
        Vector3 point3 = new Vector3(0, 5, 0);
        list.Add(point1);
        list.Add(point2);
        list.Add(point3);
        CreateCurve(point1, point2, point3);
        player.transform.position = point1;
        empty.transform.position = point1;
        Vector3[] path = new Vector3[line.positionCount];
        line.GetPositions(path);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(empty.transform.DOMove(point2, 3.0f).SetEase(Ease.Linear));
        sequence.Join(player.transform.DOPath(path, 3.0f).SetEase(Ease.Linear));
        sequence.Play();

        speed = Vector3.Distance(point1, point2) / 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float d1 = Vector3.Distance(list[0], empty.transform.position);
        if (empty.transform.position != list[1])
        {
            float d2 = Vector3.Distance(list[0], list[1]);
            float progress = d1 / d2;
            CreateCurve2(list[0], list[1], list[2], progress);
        }
    }

    public void CreateCurve2(Vector3 point1, Vector3 point2, Vector3 point3, float progress)
    {
        int vertexCount = (int)Mathf.Floor(30 * progress);
        List<Vector3> pointList = new List<Vector3>();

        for (float ratio = progress; ratio <= 1; ratio += (1.0f - progress) / vertexCount)
        {
            Vector3 tangentLineVertex1 = Vector3.Lerp(point1, point2, ratio);
            Vector3 tangentLineVectex2 = Vector3.Lerp(point2, point3, ratio);
            Vector3 bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVectex2, ratio);
            pointList.Add(bezierPoint);
        }
        pointList.Add(point3);
        line2.positionCount = pointList.Count;
        line2.SetPositions(pointList.ToArray());
    }

    public void CreateCurve(Vector3 point1, Vector3 point2, Vector3 point3)
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
        line= lineRenderer;
    }
}
