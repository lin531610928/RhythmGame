using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.MusicGame
{
    public class GameDataManager
    {
        private static GameDataManager uniqueInstance;

        private static readonly object locker = new object();

        private GameDataManager()
        {
        }

        public static GameDataManager GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if(uniqueInstance == null)
                    {
                        uniqueInstance = new GameDataManager();
                    }
                }
            }
            return uniqueInstance;
        }

        public List<RhythmPointInfoModel> GetRhythmPointList() {
            List<RhythmPointInfoModel> rhythmPointInfoModels = new List<RhythmPointInfoModel>();
            RhythmPointInfoModel rhythmPointInfoModel1 = new RhythmPointInfoModel();
            RhythmPointInfoModel rhythmPointInfoModel2 = new RhythmPointInfoModel();
            RhythmPointInfoModel rhythmPointInfoModel3 = new RhythmPointInfoModel();
            RhythmPointInfoModel rhythmPointInfoModel4 = new RhythmPointInfoModel();
            RhythmPointInfoModel rhythmPointInfoModel5 = new RhythmPointInfoModel();
            rhythmPointInfoModel1.setValues(PointType.Start, new Vector3(0, -10, 0), 0.0f);
            rhythmPointInfoModel2.setValues(PointType.BaseRhythm, new Vector3(-5, -10, 0), 2.0f);
            rhythmPointInfoModel3.setValues(PointType.Base, new Vector3(0, 0, 0), 4.0f);
            rhythmPointInfoModel4.setValues(PointType.BaseRhythm, new Vector3(-5, 0, 0), 6.0f);
            rhythmPointInfoModel5.setValues(PointType.End, new Vector3(-5, 5, 0), 8.0f);
            rhythmPointInfoModels.Add(rhythmPointInfoModel1);
            rhythmPointInfoModels.Add(rhythmPointInfoModel2);
            rhythmPointInfoModels.Add(rhythmPointInfoModel3);
            rhythmPointInfoModels.Add(rhythmPointInfoModel4);
            rhythmPointInfoModels.Add(rhythmPointInfoModel5);
            return rhythmPointInfoModels;
        }
    }
}
