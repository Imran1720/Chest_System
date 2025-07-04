namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestData data;

        private int coinsToBeRewarded;
        private int gemsToBeRewarded;

        public ChestModel(ChestData data)
        {
            this.data = data;
        }

        public int GetOpenDuration() => data.openDuration;

        public int GetGemsToBeRewarded() => gemsToBeRewarded;
        public int GetCoinsToBeRewarded() => coinsToBeRewarded;

        public int GetMinGemsRewarded() => data.minGemsRewarded;
        public int GetMaxGemsRewarded() => data.maxGemsRewarded;

        public int GetMinCoinsRewarded() => data.minCoinsRewarded;
        public int GetMaxCoinsRewarded() => data.maxCoinsRewarded;

        public void SetGemsToBeRewarded(int amount) => gemsToBeRewarded = amount;
        public void SetCoinsToBeRewarded(int amount) => coinsToBeRewarded = amount;

        public ChestData GetChestData() => data;

        public EChestType GetChestRarity() => data.chestRarity;
    }
}
