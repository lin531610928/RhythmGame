using System;
using UnityEngine;

namespace AssemblyCSharp.Assets.MusicGame
{
    public class RhythmPointInfoModel: ICloneable
    {
        public PointType type { get; private set; }
        public Vector3 vector3 { get; private set; }
        public float time { get; private set; }
        public GameObject gameObject { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void setValues(PointType type, Vector3 vector3, float time)
        {
            this.type = type;
            this.vector3 = vector3;
            this.time = time;
        }
    }
}
