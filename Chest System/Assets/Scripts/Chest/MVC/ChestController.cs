using ChestSystem.UI.Slot;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private ChestView chestView;
        private ChestModel chestModel;
        private SlotData slotData;

        private ChestStateMachine chestStateMachine;

        public ChestController(ChestView chestView, ChestModel chestModel, SlotData slotData)
        {
            this.chestView = chestView;
            this.chestModel = chestModel;
            this.slotData = slotData;

            this.chestView.SetChestController(this);
            chestStateMachine = new ChestStateMachine(this);
            chestStateMachine.ChangeState(EChestState.LOCKED);
            CalculateReward();
        }

        public void Update()
        {
            chestStateMachine.Update();
        }

        public void RefreshChestUI(EChestState currentState, int timeleft)
        {
            ChestData chestData = chestModel.GetChestData();
            chestView.SetState(currentState);
            chestView.SetTimer(chestData.openDurationInMinutes);
            chestView.SetChestIcon(chestData.chestIcon);
            chestView.SetOpeningCost(CalculateChestBuyingCost(timeleft));
        }

        public void SetChestRarity() { }

        public void SetChestTime(int minute, int second) { }

        public void SetChestIcon() { }

        public void SetChestGemPrice(int price)
        {
            chestView.SetOpeningCost(price);
        }

        private void CreateChestStateMachine()
        {
            //Create Chest statemachine
        }

        public void StartChestTimer()
        {
            chestStateMachine.ChangeState(EChestState.UNLOCKING);
        }
        private void OpenWithGems()
        {
            //Change state to Unlocked and deduct gems from player
        }

        public void SetChestUnlockingUI()
        {
            //call funtion in view to hide un necessary UI and show related UI
        }

        private void CalculateReward()
        {
            CalculateCoinsToBeRewarded();
            CalculateGemsToBeRewarded();
        }

        public int CalculateChestBuyingCost(int timerValueInMinutes)
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

        public int GetChestOpenDuration() => chestModel.GetOpenDuration();
    }
}
