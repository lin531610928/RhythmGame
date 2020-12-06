using System;
namespace AssemblyCSharp.Assets.MusicGame
{
    public enum NoteType
    {
        Base,
        LongTap
    }

    public enum PointType
    {
        Base,
        Start,
        End,
        BezierStart,
        BezierMiddle,
        BezierEnd
    }

    public enum PathType
    {
        Line,
        BezierCurve
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
        Perfect,
        Great,
        Good,
        Bad,
        Miss
    }
}
