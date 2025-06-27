using ChestSystem.UI.Slot;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private ChestView chestView;
        private ChestModel chestModel;
        private SlotData slotData;

        public ChestController(ChestView chestView, ChestModel chestModel, SlotData slotData)
        {
            this.chestView = chestView;
            this.chestModel = chestModel;
            this.slotData = slotData;
            CalculateReward();
        }

        public void Update()
        {

        }

        public void RefreshChestUI() { }

        public void SetChestRarity() { }

        public void SetChestTime(int minute, int second) { }

        public void SetChestIcon() { }

        public void SetChestGemPrice(int price) { }

        private void CreateChestStateMachine()
        {
            //Create Chest statemachine
        }

        private void StartChestTimer()
        {
            //Change State to Unlocking
        }
        private void OpenWithGems()
        {
            //Change state to Unlocked and deduct gems from player
        }

        private void CalculateReward()
        {
            CalculateCoinsToBeRewarded();
            CalculateGemsToBeRewarded();
        }

        public int ClaculateChestBuyingCost(int timerValueInMinutes)
        {
            float time = (float)timerValueInMinutes / 10;
            int buyingCost = (int)Mathf.Ceil(time) * 10;

            return buyingCost;
        }

        private void CalculateCoinsToBeRewarded()
        {
            int minimumcoinLimit = chestModel.GetMinCoinsRewarded();
            int maximumcoinLimit = chestModel.GetMaxCoinsRewarded() + 1;

            int coinQuantity = Random.Range(minimumcoinLimit, maximumcoinLimit);
            chestModel.SetCoinsToBeRewarded(coinQuantity);
        }

        private void CalculateGemsToBeRewarded()
        {
            int minimumGemsLimit = chestModel.GetMinGemsRewarded();
            int maximumGemsLimit = chestModel.GetMaxGemsRewarded() + 1;

            int gemQuantity = Random.Range(minimumGemsLimit, maximumGemsLimit);
            chestModel.SetGemsToBeRewarded(gemQuantity);
        }

        public int GetCoinsToBeRewarded() => chestModel.GetCoinsToBeRewarded();
        public int GetGemsToBeRewarded() => chestModel.GetGemsToBeRewarded();

        public string GetChestRarityText()
        {
            switch (chestModel.GetChesRarity())
            {
                case EChestType.LEGENDARY:
                    return "LEGENDARY";
                case EChestType.EPIC:
                    return "EPIC";
                case EChestType.RARE:
                    return "RARE";
                default:
                    return "COMMON";

            }
        }
    }
}
