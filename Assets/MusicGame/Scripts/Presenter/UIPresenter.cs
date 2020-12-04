using UnityEngine;
using UniRx;
using AssemblyCSharp.Assets.MusicGame;

public class UIPresenter : MonoBehaviour
{
    public UIView uiView;
    public GameDataModel gameDataModel;

    void Awake()
    {
        Observable.EveryUpdate()
            .Where(_ => gameDataModel.gameStatus == GameStatus.WaitToStart)
            .First(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ =>
            {
                gameDataModel.gameStatus = GameStatus.Playing;
            });

        gameDataModel.gameStatusRP.Subscribe(status =>
        {
            switch (status)
            {
                case GameStatus.Playing:
                    uiView.StartGame();
                    break;
                default:
                    break;
            }
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
