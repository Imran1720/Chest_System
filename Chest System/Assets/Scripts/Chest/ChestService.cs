using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        private ChestSO chestSO;
        private ChestPool chestPool;
        private ChestView chestPrefab;

        private SlotService slotService;
        private EventService eventService;

        private bool isUnlockingChest = false;

        private List<ChestController> chestControllersList = new List<ChestController>();

        public ChestService(ChestSO chestSO, ChestView chestPrefab, GameService gameService)
        {
            this.chestSO = chestSO;
            this.chestPrefab = chestPrefab;

            chestPool = new ChestPool(chestPrefab, gameService);

            InitializeServices(gameService);
            AddEventListeners();
        }

        private void InitializeServices(GameService gameService)
        {
            slotService = gameService.GetSlotService();
            eventService = gameService.GetEventService();
        }

        public void CreateChest(SlotData slotData)
        {
            chestPool.SetSlotData(slotData);
            slotService.FillSlot(slotData);

            ChestController controller = chestPool.GetChest(GetRandomChest());
            if (!IsSimilarTypeOfControllerAvailable(controller))
            {
                chestControllersList.Add(controller);
            }

            controller.SetViewActive();
        }

        private bool IsSimilarTypeOfControllerAvailable(ChestController controller)
        {
            ChestController chestController = chestControllersList.Find(item => item == controller);
            return chestController != null;
        }

        public void Update()
        {
            foreach (ChestController controller in chestControllersList)
            {
                controller?.Update();
            }
        }

        public bool IsChestUnlocking()
        {
            ChestController controller = chestControllersList.Find(chest => chest.GetCurrentChestState().Equals(EChestState.UNLOCKING));
            isUnlockingChest = controller != null;
            return isUnlockingChest;
        }

        private void AddEventListeners() => eventService.OnRewardCollected.AddListener(ReturnChestToPool);
        public void RemoveEventListeners() => eventService.OnRewardCollected.RemoveListener(ReturnChestToPool);

        public void ReturnChestToPool(ChestController controller) => chestPool.ReturnChest(controller);

        private ChestData GetRandomChest() => chestSO.ChestTypeList[GetRandomIndex()];

        private int GetRandomIndex() => Random.Range(0, chestSO.ChestTypeList.Length);
    }
}
