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
            CreateChestStateMachine();
            chestStateMachine.ChangeState(EChestState.LOCKED);
            CalculateReward();
        }

        public void Update()
        {
            chestStateMachine.Update();
        }

        public void UpdateChestUI(EChestState currentState)
        {
            ChestData chestData = chestModel.GetChestData();
            chestView.SetState(currentState);

            int timer = chestData.openDurationInMinutes * 60;
            chestView.SetTimer(CalculateHours(timer), CalculateMinutes(timer));

            chestView.SetChestIcon(chestData.chestIcon);
            chestView.SetOpeningCost(CalculateChestBuyingCost(timer));
        }

        public void SetChestTime(int minute, int second) => chestView.SetTimer(minute, second);

        public void SetChestIcon(Sprite chestSprite) => chestView.SetChestIcon(chestSprite);

        public void SetChestGemPrice(int price) => chestView.SetOpeningCost(price);

        private void CreateChestStateMachine() => chestStateMachine = new ChestStateMachine(this);

        public void OnSelectingChest() => chestStateMachine.ProcessOnClick();

        private void OpenWithGems() => chestStateMachine.ChangeState(EChestState.UNLOCKED);

        public void SetChestUnlockingUI() => chestStateMachine.ChangeState(EChestState.UNLOCKING);

        private void CalculateReward()
        {
            CalculateCoinsToBeRewarded();
            CalculateGemsToBeRewarded();
        }

        public int CalculateChestBuyingCost(float timer)
        {
            int elapsedTime = (int)timer / 60;
            int gemsRequired = (int)(Mathf.Ceil(elapsedTime / 10));

            return gemsRequired;
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

        public int CalculateMinutes(float timer) => (int)timer / 60;
        public int CalculateHours(float timer) => (int)timer / (60 * 60);

        public void UpdateCost(float timer)
        {
            int gemsRequired = CalculateChestBuyingCost(timer);
            SetChestGemPrice(gemsRequired);
        }

        public void UpdateTime(float timer)
        {
            int hours = CalculateHours(timer);
            int minutes = CalculateMinutes(timer);

            SetChestTime(hours, minutes);
        }

        public void SetLockedUI(bool value) => chestView.SetLockedUI(value);
    }
}
