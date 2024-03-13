using System;

namespace Assets.FlappyTerminator.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State PlayerState;
        public int Score;
        public int CurrentScore;

        public PlayerProgress(string initialLevel, int score)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new State();
            Score = score;
            CurrentScore = 0;
        }
    }
}