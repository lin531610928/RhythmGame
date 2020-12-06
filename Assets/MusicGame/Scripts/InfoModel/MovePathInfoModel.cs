using System;
using UnityEngine;

namespace AssemblyCSharp.Assets.MusicGame
{
    public class MovePathInfoModel
    {
        public PathType pathType { get; private set; }
        public LineRenderer lineRenderer { get; private set; }
        public float duration { get; private set; }
        public Vector3? bezierMiddlePoint { get; private set; }

        public MovePathInfoModel(PathType pathType, LineRenderer lineRenderer, float duration)
        {
            this.pathType = pathType;
            this.lineRenderer = lineRenderer;
            this.duration = duration;
        }

        public MovePathInfoModel(PathType pathType, LineRenderer lineRenderer, float duration, Vector3 bezierMiddlePoint)
            : this(pathType, lineRenderer, duration)
        {
            this.bezierMiddlePoint = bezierMiddlePoint;
        }
    }
}
