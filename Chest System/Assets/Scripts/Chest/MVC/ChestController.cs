using ChestSystem.UI;
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
            Reset();
            CalculateReward();
        }

        public void Reset()
        {
            chestView.Reset();
            ChangeToDefaultState();
        }

        public void ChangeToDefaultState()
        {
            chestStateMachine.ChangeState(EChestState.LOCKED);
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

        public void OpenWithGems() => chestStateMachine.ChangeState(EChestState.UNLOCKED);
        public void StartTimer() => chestStateMachine.ChangeState(EChestState.UNLOCKING);

        //public void SetChestUnlockingUI() => chestStateMachine.ChangeState(EChestState.UNLOCKING);

        private void CalculateReward()
        {
            CalculateCoinsToBeRewarded();
            CalculateGemsToBeRewarded();
        }

        public int CalculateChestBuyingCost(float timer)
        {
            int elapsedTime = (int)timer / 60;
            int gemsRequired = (int)(Mathf.Ceil((float)elapsedTime / 10));
            if (gemsRequired == 0)
                gemsRequired = 1;

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

        public void SetOpenedChestBG() => chestView.SetOpenedChestBG();

        public void SetLockedUI(bool value) => chestView.SetLockedUI(value);

        public void SetViewActive() => chestView.gameObject.SetActive(true);
        public void SetViewInactive() => chestView.gameObject.SetActive(false);

        public void EmptyCurrentSlot() => UIService.Instance.GetSlotService().EmptySlot(slotData);
        //public void FillCurrentSlot() => UIService.Instance.GetSlotService().FillSlot(slotData);
        public EChestType GetChestRarity() => chestModel.GetChesRarity();

        public void ResetParent(Transform parent)
        {
            chestView.transform.parent = null;
            chestView.transform.position = parent.position;
            chestView.transform.SetParent(parent, true);
        }

        public int GetChestBuyingCost() => chestStateMachine.GetChestBuyingCost();
        public int GetDefaultBuyingCost() => CalculateChestBuyingCost(chestModel.GetOpenDuration() * 60);
    }
}
