using System;
using UnityEngine;

namespace AssemblyCSharp.Assets.MusicGame
{
    public class RhythmPointInfoModel: ICloneable
    {
        public NoteType? noteType { get; private set; }
        public PointType pointType { get; private set; }
        public Vector3 vector3 { get; private set; }
        public float time { get; private set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void setValues(NoteType? noteType, PointType pointType, Vector3 vector3, float time)
        {
            this.noteType = noteType;
            this.pointType = pointType;
            this.vector3 = vector3;
            this.time = time;
        }
    }
}
