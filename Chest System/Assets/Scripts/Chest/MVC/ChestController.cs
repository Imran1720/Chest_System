using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI.Slot;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private SlotData slotData;
        private ChestView chestView;
        private ChestModel chestModel;

        private ChestStateMachine chestStateMachine;

        private GameService gameService;
        private SlotService slotService;
        private EventService eventService;
        private PlayerService playerService;

        private const int secondsPerMinute = 60;
        private const int secondsPerHour = 3600;
        private const int costUpdateIntervalInMinutes = 10;
        private const int minimumGem = 1;

        public ChestController(ChestView chestView, ChestModel chestModel, SlotData slotData, GameService gameService)
        {
            this.chestView = chestView;
            this.chestModel = chestModel;
            this.slotData = slotData;
            this.gameService = gameService;

            InitializeServices();
            AddEventListeners();

            CreateChestStateMachine();
            CalculateReward();
            ResetToDefaultState();
        }

        private void AddEventListeners()
        {
            eventService.OnProcessingUndo.AddListener(OnUndo);
            eventService.OnChestBought.AddListener(OnChestBought);
            eventService.OnChestSelected.AddListener(OnChestSelected);
            eventService.OnRewardCollected.AddListener(OnRewardCollected);
        }

        public void RemoveEventListeners()
        {
            eventService.OnProcessingUndo.RemoveListener(OnUndo);
            eventService.OnChestBought.RemoveListener(OnChestBought);
            eventService.OnChestSelected.RemoveListener(OnChestSelected);
        }

        public void InitializeServices()
        {
            slotService = gameService.GetSlotService();
            eventService = gameService.GetEventService();
            playerService = gameService.GetPlayerService();
        }

        public void Update() => chestStateMachine.Update();

        public void UpdateChestUI(EChestState currentState)
        {
            ChestData chestData = chestModel.GetChestData();
            chestView.SetState(currentState);

            int timer = GetChestOpenDuration();
            chestView.SetTimer(CalculateHours(timer), CalculateMinutes(timer));

            Sprite icon = chestData.chestIcon;
            chestView.SetChestIcon(icon);
            chestView.SetOpeningCost(GetChestBuyingCostByTime(timer));
        }

        private void CalculateReward()
        {
            CalculateCoinsToBeRewarded();
            CalculateGemsToBeRewarded();
        }

        public int GetChestBuyingCostByTime(float timer)
        {
            int elapsedTime = (int)timer / secondsPerMinute;
            int gemsRequired = (int)(Mathf.Ceil((float)elapsedTime / costUpdateIntervalInMinutes));

            if (gemsRequired == 0)
            {
                gemsRequired = minimumGem;
            }
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

        public void UpdateCost(float timer)
        {
            int gemsRequired = GetChestBuyingCostByTime(timer);
            SetChestGemPrice(gemsRequired);
        }

        public void UpdateTime(float timer)
        {
            int hours = CalculateHours(timer);
            int minutes = CalculateMinutes(timer);

            SetChestTime(hours, minutes);
        }

        public void SwitchSlot(SlotData slotData)
        {
            this.slotData = slotData;
            chestView.transform.SetParent(null, false);
            chestView.transform.position = slotData.transform.position;
            chestView.transform.SetParent(slotData.transform, true);
        }

        public void ResetToDefaultState()
        {
            chestView.Reset();
            slotService.FillSlot(slotData);
            ChangeToLockedState();
        }

        //Event Listeners
        private void OnUndo(ChestController controller)
        {
            if (controller == this)
            {
                ResetToDefaultState();
                playerService.IncrementGemsBy(GetChestBuyingCost());
            }
        }

        private void OnChestBought(ChestController controller)
        {
            if (this == controller)
            {
                if (CanBuyChest())
                {
                    playerService.DecrementGemsBy(GetChestBuyingCost());
                    OpenWithGems();
                }
                else
                {
                    eventService.OnInsufficientFunds.InvokeEvent();
                }
            }
        }

        private void OnChestSelected(ChestView view)
        {
            if (view.Equals(chestView))
            {
                chestStateMachine.ProcessOnClick();
            }
        }

        private void OnRewardCollected(ChestController controller)
        {
            if (controller == this)
            {
                eventService.OnRewardProcessing.InvokeEvent();
                SetViewInactive();
            }
        }

        // Default State Setter
        public void ChangeToLockedState() => chestStateMachine.ChangeState(EChestState.LOCKED);

        // Buying Cost Getters
        public int GetChestBuyingCost() => chestStateMachine.GetChestBuyingCost();
        public int GetDefaultBuyingCost() => GetChestBuyingCostByTime(chestModel.GetOpenDuration() * secondsPerMinute);

        // Utilities
        public SlotData GetCurrentSlot() => slotData;

        public int GetSecondsPerMinute() => secondsPerMinute;
        public int GetUpdateInterval() => costUpdateIntervalInMinutes * secondsPerMinute;

        public bool CanUnlockChest() => gameService.CanUnlockChest();
        public bool CanBuyChest() => playerService.HasSufficientGems(GetChestBuyingCost());

        public EChestState GetCurrentChestState() => chestStateMachine.GetCurrentStateType();


        // Chest View Modifiers
        public void SetViewActive() => chestView.gameObject.SetActive(true);
        public void SetViewInactive() => chestView.gameObject.SetActive(false);

        public void SetOpenedChestBG() => chestView.SetOpenedChestBG();
        public void SetChestGemPrice(int price) => chestView.SetOpeningCost(price);
        public void SetChestTime(int hour, int minute) => chestView.SetTimer(hour, minute);
        public void SetChestIcon(Sprite chestSprite) => chestView.SetChestIcon(chestSprite);
        public void SetLockedUI(bool value) => chestView.SetLockedUI(value, GetCurrentChestState());


        // Chest Data
        public int GetGemsToBeRewarded() => chestModel.GetGemsToBeRewarded();
        public int GetCoinsToBeRewarded() => chestModel.GetCoinsToBeRewarded();
        public int GetChestOpenDuration() => chestModel.GetOpenDuration() * secondsPerMinute;

        public EChestType GetChestRarity() => chestModel.GetChestRarity();

        // Time Calculations
        public int CalculateHours(float timer) => (int)timer / secondsPerHour;
        public int CalculateMinutes(float timer) => (int)timer / secondsPerMinute;

        // Statemachine related Locgic
        public void StartTimer() => chestStateMachine.ChangeState(EChestState.UNLOCKING);
        private void OpenWithGems() => chestStateMachine.ChangeState(EChestState.UNLOCKED);
        private void CreateChestStateMachine() => chestStateMachine = new ChestStateMachine(this, eventService);
    }
}
