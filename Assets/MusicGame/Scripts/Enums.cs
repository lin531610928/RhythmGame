using System;
namespace AssemblyCSharp.Assets.MusicGame
{
    public enum PointType
    {
        Base,
        Start,
        End,
        BaseRhythm
    }

    public enum GameStatus
    {
        Init,
        WaitToStart,
        Playing,
        End
    }

    public enum DecisionStatus
    {
        WaitToStart,
        Animating,
        Detecting,
        End
    }

    public enum DecisionResult
    {
        Prefre,
        Great,
        Good,
        Bad,
        Miss
    }
}
