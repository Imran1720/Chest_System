using UnityEngine;

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

        public int GetMinCoinsRewarded() => data.minCoinsRewarded;
        public int GetMaxCoinsRewarded() => data.maxCoinsRewarded;
        public int GetMinGemsRewarded() => data.minGemsRewarded;
        public int GetMaxGemsRewarded() => data.maxGemsRewarded;
        public int GetOpenDuration() => data.openDurationInMinutes;
        public int GetCoinsToBeRewarded() => coinsToBeRewarded;
        public int GetGemsToBeRewarded() => gemsToBeRewarded;


        public void SetCoinsToBeRewarded(int amount) => coinsToBeRewarded = amount;
        public void SetGemsToBeRewarded(int amount) => gemsToBeRewarded = amount;
        public EChestType GetChesRarity() => data.chestRarity;
        public Sprite GetChestIcon() => data.chestIcon;
    }
}
