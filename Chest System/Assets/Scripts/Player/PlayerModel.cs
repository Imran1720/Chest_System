namespace ChestSystem.Player
{
    public class PlayerModel
    {
        private int coinCount;
        private int gemCount;

        public PlayerModel(int initialCoinCount, int initialGemCount)
        {
            coinCount = initialCoinCount;
            gemCount = initialGemCount;
        }

        public void SetCoinCount(int count) => coinCount = count;
        public void SetGemCount(int count) => gemCount = count;

        public int GetCoinCount() => coinCount;
        public int GetGemCount() => gemCount;

        public int AddCoins(int amount) => coinCount += amount;
        public int AddGems(int amount) => gemCount += amount;
    }
}