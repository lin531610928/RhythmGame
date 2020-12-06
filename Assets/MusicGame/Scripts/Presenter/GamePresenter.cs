using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using AssemblyCSharp.Assets.MusicGame;
using System;

public class GamePresenter : MonoBehaviour
{
    public GameView gameView;
    public GameDataModel gameDataModel;

    void Awake()
    {
        gameDataModel.rhythmPointListRP
            .First(e => e != null)
            .Subscribe(list =>
            {
                gameDataModel.movePathList = gameView.GetMovePaths(list);
                List<RhythmDecisionInfoModel> decisionInfoModels = new List<RhythmDecisionInfoModel>();
                for (int i = 0; i < list.Count(); i++)
                {
                    switch (list[i].noteType)
                    {
                        case NoteType.Base:
                            RhythmDecisionInfoModel decisionInfoModel = new RhythmDecisionInfoModel();
                            decisionInfoModel.setValues(NoteType.Base, list[i].time);
                            decisionInfoModel.gameObject = gameView.GetBaseRhythmPoint(list[i]);
                            decisionInfoModels.Add(decisionInfoModel);
                            break;
                        default:
                            break;
                    }
                    if (list[i].pointType == PointType.Start) {
                        gameView.SetPlayerPosition(list[i].vector3);
                    }
                }
                gameDataModel.rhythmDecisionList = decisionInfoModels;
                gameDataModel.gameStatus = GameStatus.WaitToStart;
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(0.1f))
            .RepeatUntilDisable(this)
            .Where(_ => gameDataModel.gameStatus == GameStatus.Playing)
            .Where(_ => gameView.musicControl.isPlaying)
            .Subscribe(_ => {
                if (gameDataModel.nextPathIndex < 0) {
                    gameDataModel.nextPathIndex = 0;
                }
                if (gameView.musicControl.time > gameDataModel.rhythmPointList[gameDataModel.nextPointIndex].time
                && gameDataModel.nextPointIndex + 1 < gameDataModel.rhythmPointList.Count)
                {
                    if (gameDataModel.rhythmPointList[gameDataModel.nextPointIndex].pointType != PointType.BezierMiddle)
                    {
                        gameView.SetPlayerPosition(gameDataModel.rhythmPointList[gameDataModel.nextPointIndex].vector3);
                        gameDataModel.nextPathIndex++;
                    }
                    gameDataModel.nextPointIndex++;
                }
                gameView.SetAnimation(gameDataModel.rhythmDecisionList);
                foreach (RhythmDecisionInfoModel infoModel in gameDataModel.rhythmDecisionList)
                {
                    if (gameView.GetCurrentMusicTime() >= (infoModel.time - DecisionRange.Miss)
                    && (infoModel.detectStatus == DecisionStatus.WaitToStart
                    || infoModel.detectStatus == DecisionStatus.Animating))
                    {
                        infoModel.detectStatus = DecisionStatus.Detecting;
                    }
                    if (infoModel.detectStatus == DecisionStatus.Detecting
                    && gameView.GetCurrentMusicTime() > (infoModel.time + DecisionRange.Miss))
                    {
                        infoModel.detectStatus = DecisionStatus.End;
                        infoModel.decisionResult = DecisionResult.Miss;
                        gameDataModel.currentDecisionResult = infoModel.decisionResult;
                    }
                    if (infoModel.detectStatus == DecisionStatus.End
                    && infoModel.gameObject != null)
                    {
                        infoModel.gameObject.SetActive(false);
                        Destroy(infoModel.gameObject);
                    }
                }
            });

        Observable.Timer(TimeSpan.FromMilliseconds(0.1f))
            .RepeatUntilDisable(this)
            .Where(_ => gameDataModel.gameStatus == GameStatus.Playing)
            .Where(_ => gameView.musicControl.isPlaying)
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => {
                float tapTime = gameView.GetCurrentMusicTime();
                RhythmDecisionInfoModel decisionInfoModel = gameDataModel.rhythmDecisionList.Find(e => e.detectStatus == DecisionStatus.Detecting);
                if (decisionInfoModel != null) {
                    decisionInfoModel.decisionResult = GetDecisionResult(decisionInfoModel.time - tapTime);
                    decisionInfoModel.detectStatus = DecisionStatus.End;
                    gameDataModel.currentDecisionResult = decisionInfoModel.decisionResult;
                }
            });

        gameDataModel.gameStatusRP.Subscribe(status =>
        {
            switch (status)
            {
                case GameStatus.Playing:
                    gameView.StartGame();
                    gameDataModel.nextPointIndex = 1;
                    gameDataModel.nextPathIndex = -1;
                    break;
                default:
                    break;
            }
        });

        gameView.SetMusic();
        gameDataModel.GetGameData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private DecisionResult GetDecisionResult(float time)
    {
        float timeAds = Math.Abs(time);
        if (timeAds <= DecisionRange.Perfect)
        {
            return DecisionResult.Perfect;
        }
        else if (timeAds <= DecisionRange.Great)
        {
            return DecisionResult.Great;
        }
        else if (timeAds <= DecisionRange.Good)
        {
            return DecisionResult.Good;
        }
        else if (timeAds <= DecisionRange.Bad)
        {
            return DecisionResult.Bad;
        }
        else
        {
            return DecisionResult.Miss;
        }
    }
}
