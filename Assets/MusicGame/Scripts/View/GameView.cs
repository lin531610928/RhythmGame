using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp.Assets.MusicGame;
using UnityEngine;

public class GameView : MonoBehaviour
{
    public LineRenderer lineRenderer;
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
    public void CteatePath(List<RhythmPointInfoModel> infos)
    {
        Vector3[] vector3s = infos.Select(e => e.vector3).ToArray();
        lineRenderer.positionCount = vector3s.Count();
        lineRenderer.SetPositions(vector3s);
    }

    /// <summary>
    /// 设置基本节拍点
    /// </summary>
    /// <param name="infoModel">节拍点信息</param>
    public GameObject getBaseRhythmPoint(RhythmPointInfoModel infoModel)
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
}
