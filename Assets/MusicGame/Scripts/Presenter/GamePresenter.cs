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
                gameView.CteatePath(list);
                for (int i = 0; i < list.Count(); i++)
                {
                    switch (list[i].type)
                    {
                        case PointType.Start:
                            gameView.SetPlayerPosition(list[i].vector3);
                            break;
                        case PointType.BaseRhythm:
                            list[i].gameObject = gameView.getBaseRhythmPoint(list[i]);
                            break;
                        default:
                            break;
                    }
                }
                gameDataModel.rhythmPointList = list;
                gameDataModel.gameStatus = GameStatus.WaitToStart;
            })
            .AddTo(this);

        gameDataModel.nextPointIndexRP.Subscribe(index =>
        {
            //playerView.movePlayer
        });

        Observable.Timer(TimeSpan.FromMilliseconds(0.1f))
            .RepeatUntilDisable(this)
            .Where(_ => gameDataModel.gameStatus == GameStatus.Playing)
            .Where(_ => gameView.musicControl.isPlaying)
            .Subscribe(_ => {
                if (gameView.musicControl.time > gameDataModel.rhythmPointList[gameDataModel.nextPointIndex].time
                && gameDataModel.nextPointIndex + 1 < gameDataModel.rhythmPointList.Count)
                {
                    gameView.SetPlayerPosition(gameDataModel.rhythmPointList[gameDataModel.nextPointIndex].vector3);
                    gameDataModel.nextPointIndex++;
                }
            });

        gameDataModel.gameStatusRP.Subscribe(status =>
        {
            switch (status)
            {
                case GameStatus.Playing:
                    gameView.StartGame();
                    gameDataModel.nextPointIndex = 1;
                    break;
                default:
                    break;
            }
        });

        gameView.SetMusic();
        gameDataModel.getGameData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
