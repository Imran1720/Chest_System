using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI;
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

        private UIService uiService;
        private SlotService slotService;
        private EventService eventService;

        private bool isUnlockingChest = false;

        private List<ChestController> chestControllersList = new List<ChestController>();

        public ChestService(ChestSO chestSO, ChestView chestPrefab, EventService eventService)
        {
            this.chestSO = chestSO;
            this.chestPrefab = chestPrefab;
            this.eventService = eventService;

            chestPool = new ChestPool(chestPrefab, uiService, eventService);

            AddEventListeners();
        }

        public void InitializeSevices(GameService gameService)
        {
            uiService = gameService.GetUIService();
            slotService = uiService.GetSlotService();
        }

        private void AddEventListeners() => eventService.OnRewardCollected.AddListener(ReturnChestToPool);

        public void CreateChest(SlotData slotData)
        {
            chestPool.SetSlotData(slotData);

            ChestController controller = chestPool.GetChest(GetRandomChest());
            if (!IsSimilarTypeOfControllerAvailable(controller))
            {
                chestControllersList.Add(controller);
            }

            slotService.FillSlot(slotData);
            controller.SetViewActive();
        }

        private bool IsSimilarTypeOfControllerAvailable(ChestController controller)
        {
            ChestController chestController = chestControllersList.Find(item => item == controller);
            return chestController != null;
        }

        public void Update()
        {
            foreach (var controller in chestControllersList)
            {
                controller?.Update();
            }
        }

        public bool CanUnlockChest() => !isUnlockingChest;
        public void SetIsChestUnlocking(bool value) => isUnlockingChest = value;
        private ChestData GetRandomChest() => chestSO.ChestTypeList[GetRandomIndex()];
        private int GetRandomIndex() => Random.Range(0, chestSO.ChestTypeList.Length);
        public void ReturnChestToPool(ChestController controller) => chestPool.ReturnChest(controller);
    }
}
