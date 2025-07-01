using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI;
using ChestSystem.UI.PopUp;
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

        private UIService uiService;
        private SlotService slotService;
        private PopUpService popUpService;
        private EventService eventService;

        public ChestController(ChestView chestView, ChestModel chestModel, SlotData slotData)
        {
            this.chestView = chestView;
            this.chestModel = chestModel;
            this.slotData = slotData;
            InitializeServices();
            CreateChestStateMachine();
            Reset();
            CalculateReward();
            AddListeners();
        }

        private void AddListeners()
        {
            eventService.OnChestSelected.AddListener(OnChestSelected);
        }

        ~ChestController()
        {
            eventService.OnChestSelected.RemoveListener(OnChestSelected);
        }

        public void InitializeServices()
        {
            uiService = GameService.Instance.GetUIService();
            slotService = uiService.GetSlotService();
            popUpService = uiService.GetPopUpService();
            eventService = GameService.Instance.GetEventService();
        }

        public void Reset()
        {
            chestView.Reset();
            slotService.FillSlot(slotData);
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

        public void OnChestSelected(ChestView view)
        {
            if (view.Equals(chestView))
            {
                chestStateMachine.ProcessOnClick();
            }
        }

        public void OpenWithGems() => chestStateMachine.ChangeState(EChestState.UNLOCKED);
        public void StartTimer() => chestStateMachine.ChangeState(EChestState.UNLOCKING);

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

        public void SetLockedUI(bool value) => chestView.SetLockedUI(value, GetCurrentChestState());

        public void SetViewActive() => chestView.gameObject.SetActive(true);
        public void SetViewInactive() => chestView.gameObject.SetActive(false);

        public void EmptyCurrentSlot() => slotService.EmptySlot(slotData);
        public EChestType GetChestRarity() => chestModel.GetChesRarity();

        public void ResetParent(Transform parent)
        {
            chestView.transform.parent = null;
            chestView.transform.position = parent.position;
            chestView.transform.SetParent(parent, true);
        }

        public void ClearCommandHistory() => GameService.Instance.ClearCommandHistory();

        public int GetChestBuyingCost() => chestStateMachine.GetChestBuyingCost();
        public int GetDefaultBuyingCost() => CalculateChestBuyingCost(chestModel.GetOpenDuration() * 60);

        public bool CanUnlockChest() => GameService.Instance.GetChestService().CanUnlockChest();

        public void ShowUnlockPopUP() => popUpService.ShowUnlockPopUP(this);
        public void ShowChestOpeningPopUP() => popUpService.ShowChestOpeningPopUP();
        public void ShowBuyPopUP() => popUpService.ShowBuyPopUP(this);
        public void ReturnChestToPool() => GameService.Instance.GetChestService().ReturnChestToPool(this);
        public EChestState GetCurrentChestState() => chestStateMachine.GetCurrentStateType();
    }
}
