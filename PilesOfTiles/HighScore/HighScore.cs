namespace PilesOfTiles.HighScore
{
    public class HighScore
    {
        public string PlayerName { get; set; }
        public float Score { get; set; }
        public int DifficultyLevel { get; set; }

        public override string ToString()
        {
            return string.Format("{0};{1};{2}", PlayerName, Score, DifficultyLevel);
        }
    }
}