using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestPool
    {
        private SlotData slotData;
        private ChestView chestPrefab;

        private GameService gameService;
        private EventService eventService;

        private List<PooledChest> pooledChestsList = new List<PooledChest>();

        public ChestPool(ChestView chestPrefab, GameService gameService)
        {
            this.chestPrefab = chestPrefab;
            this.gameService = gameService;

            InitializeServices();
        }

        public ChestController GetChest(ChestData chestData)
        {
            if (pooledChestsList.Count > 0)
            {
                PooledChest pooledChest = pooledChestsList.Find(item => (!item.isUsed && IsRequiredChest(chestData, item.controller)));
                if (pooledChest != null)
                {
                    SwitchSlot(pooledChest.controller);
                    pooledChest.controller.ResetChest();

                    pooledChest.controller.ResetChest();
                    pooledChest.isUsed = true;
                    return pooledChest.controller;
                }
            }
            return CreatePooledChest(chestData);
        }

        private ChestController CreatePooledChest(ChestData data)
        {
            PooledChest pooledChest = new PooledChest();

            ChestView view = CreateChestView();
            ChestModel model = new ChestModel(data);
            pooledChest.controller = CreateController(view, model, gameService);

            pooledChest.isUsed = true;
            pooledChestsList.Add(pooledChest);

            return pooledChest.controller;
        }

        private ChestController CreateController(ChestView view, ChestModel model, GameService gameService)
        {
            return new ChestController(view, model, slotData, gameService);
        }

        private ChestView CreateChestView()
        {
            ChestView chestView = GameObject.Instantiate(chestPrefab, slotData.transform.position, Quaternion.identity);
            chestView.transform.SetParent(slotData.transform, true);
            chestView.SetServices(eventService);
            return chestView;
        }

        public void ReturnChest(ChestController controller)
        {
            PooledChest pooledChest = pooledChestsList.Find(item => item.controller.Equals(controller));
            pooledChest.isUsed = false;
        }

        public void SetSlotData(SlotData slotData) => this.slotData = slotData;
        private void InitializeServices() => eventService = gameService.GetEventService();
        private void SwitchSlot(ChestController chestController) => chestController.SwitchSlot(slotData);

        private bool IsRequiredChest(ChestData data, ChestController controller) => (data.chestRarity == controller.GetChestRarity());
    }
}
