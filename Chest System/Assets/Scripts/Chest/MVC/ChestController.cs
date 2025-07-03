using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI;
using ChestSystem.UI.PopUp;
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

        public ChestController(ChestView chestView, ChestModel chestModel, SlotData slotData, GameService gameService)
        {
            this.chestView = chestView;
            this.chestModel = chestModel;
            this.slotData = slotData;
            this.gameService = gameService;

            InitializeServices();
            AddListeners();

            CreateChestStateMachine();
            CalculateReward();
            ResetChest();
        }

        private void AddListeners()
        {
            eventService.OnChestSelected.AddListener(OnChestSelected);
            eventService.OnChestBought.AddListener(OnChestBought);
            eventService.OnProcessingUndo.AddListener(OnUndo);
            eventService.OnRewardCollected.AddListener(OnRewardCollected);
        }

        ~ChestController()
        {
            eventService.OnChestSelected.RemoveListener(OnChestSelected);
            eventService.OnChestBought.RemoveListener(OnChestBought);
            eventService.OnProcessingUndo.RemoveListener(OnUndo);
        }

        public void InitializeServices()
        {
            slotService = gameService.GetSlotService();
            eventService = gameService.GetEventService();
            playerService = gameService.GetPlayerService();
        }

        public void ResetChest()
        {
            chestView.Reset();
            slotService.FillSlot(slotData);
            ChangeToDefaultState();
        }

        public void ChangeToDefaultState() => chestStateMachine.ChangeState(EChestState.LOCKED);

        public void Update() => chestStateMachine.Update();

        public void UpdateChestUI(EChestState currentState)
        {
            ChestData chestData = chestModel.GetChestData();
            chestView.SetState(currentState);

            int timer = chestData.openDuration * 60;
            chestView.SetTimer(CalculateHours(timer), CalculateMinutes(timer));

            Sprite icon = chestData.chestIcon;
            chestView.SetChestIcon(icon);
            chestView.SetOpeningCost(CalculateChestBuyingCost(timer));
        }

        public void SetChestTime(int hour, int minute) => chestView.SetTimer(hour, minute);

        public void SetChestIcon(Sprite chestSprite) => chestView.SetChestIcon(chestSprite);

        public void SetChestGemPrice(int price) => chestView.SetOpeningCost(price);

        private void CreateChestStateMachine() => chestStateMachine = new ChestStateMachine(this, gameService);

        public void OnChestSelected(ChestView view)
        {
            if (view.Equals(chestView))
            {
                chestStateMachine.ProcessOnClick();
            }
        }

        private void OpenWithGems() => chestStateMachine.ChangeState(EChestState.UNLOCKED);
        public void StartTimer() => chestStateMachine.ChangeState(EChestState.UNLOCKING);

        private void OnChestBought(ChestController controller)
        {
            if (this == controller)
            {
                if (CanBuyChest())
                {
                    gameService.GetPlayerService().DecrementGemsBy(GetChestBuyingCost());
                    OpenWithGems();
                }
                else
                {
                    eventService.OnInsufficientFunds.InvokeEvent();
                }
            }
        }

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
        public EChestType GetChestRarity() => chestModel.GetChestRarity();

        public void SwitchSlot(SlotData slotData)
        {
            this.slotData = slotData;
            chestView.transform.SetParent(null, false);
            chestView.transform.position = slotData.transform.position;
            chestView.transform.SetParent(slotData.transform, true);
        }

        public bool CanBuyChest()
        {
            int requiredGems = GetChestBuyingCost();
            return playerService.HasSufficientGems(requiredGems);
        }

        public int GetChestBuyingCost() => chestStateMachine.GetChestBuyingCost();
        public int GetDefaultBuyingCost() => CalculateChestBuyingCost(chestModel.GetOpenDuration() * 60);

        public bool CanUnlockChest() => gameService.CanUnlockChest();

        public EChestState GetCurrentChestState() => chestStateMachine.GetCurrentStateType();

        private void OnUndo(ChestController controller)
        {
            if (controller == this)
            {
                ResetChest();
                playerService.IncrementGemsBy(GetChestBuyingCost());
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

        public SlotData GetCurrentSlot() => slotData;
    }
}
