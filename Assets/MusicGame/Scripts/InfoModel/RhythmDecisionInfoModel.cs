using System;
using UnityEngine;

namespace AssemblyCSharp.Assets.MusicGame
{
    public class RhythmDecisionInfoModel
    {
        public PointType type { get; private set; }
        public GameObject gameObject { get; set; }
        public float time { get; private set; }
        public DecisionStatus detectStatus { get; set; }
        public DecisionResult decisionResult { get; set; }

        public RhythmDecisionInfoModel()
        {
            detectStatus = DecisionStatus.WaitToStart;
        }

        public void setValues(RhythmPointInfoModel info)
        {
            this.type = info.type;
            this.time = info.time;
        }
    }
}
