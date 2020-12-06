using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using AssemblyCSharp.Assets.MusicGame;

public class PlayerPresenter : MonoBehaviour
{
    public PlayerView playerView;
    public GameDataModel gameDataModel;

    void Awake()
    {
        gameDataModel.nextPathIndexRP
            .Where(_ => gameDataModel.gameStatus == GameStatus.Playing)
            .Where(e => e >= 0)
            .Subscribe(index =>
            {
                playerView.movePlayer(gameDataModel.movePathList[index]);
            });
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
