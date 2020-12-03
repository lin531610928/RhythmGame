using System;
namespace AssemblyCSharp.Assets.MusicGame.Scripts.Game
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
    }
}
