using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using AssemblyCSharp.Assets.MusicGame;

public class GameDataModel : MonoBehaviour
{
    public IntReactiveProperty nextPointIndexRP = new IntReactiveProperty();
    public ReactiveProperty<GameStatus> gameStatusRP = new ReactiveProperty<GameStatus>();
    public ReactiveProperty<List<RhythmPointInfoModel>> rhythmPointListRP = new ReactiveProperty<List<RhythmPointInfoModel>>();

    public GameStatus gameStatus
    {
        get { return gameStatusRP.Value; }
        set { gameStatusRP.Value = value; }
    }
    public int nextPointIndex
    {
        get { return nextPointIndexRP.Value; }
        set { nextPointIndexRP.Value = value; }
    }
    public List<RhythmPointInfoModel> rhythmPointList
    {
        get { return rhythmPointListRP.Value; }
        set { rhythmPointListRP.Value = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 获取游戏数据
    /// </summary>
    public void getGameData()
    {
        rhythmPointList = (List<RhythmPointInfoModel>)GameDataManager.GetInstance().GetRhythmPointList().Clone();
    }
}
