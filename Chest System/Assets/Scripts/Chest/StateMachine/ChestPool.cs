using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI;
using ChestSystem.UI.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestPool
    {
        private ChestView chestPrefab;
        private SlotData slotData;

        private GameService gameService;
        private EventService eventService;
        private List<PooledChest> pooledChestsList = new List<PooledChest>();

        public ChestPool(ChestView chestPrefab, GameService gameService)
        {
            this.chestPrefab = chestPrefab;
            this.gameService = gameService;

            InitializeSerivces();
        }

        private void InitializeSerivces()
        {
            eventService = gameService.GetEventService();
        }

        public void SetSlotData(SlotData slotData) => this.slotData = slotData;

        public ChestController GetChest(ChestData chestData)
        {
            if (pooledChestsList.Count > 0)
            {
                PooledChest pooledChest = pooledChestsList.Find(item => (!item.isUsed && IsRequiredChest(chestData, item.controller)));
                if (pooledChest != null)
                {
                    SwitchSlot(pooledChest.controller);
                    pooledChest.controller.ResetChest();
                    pooledChest.isUsed = true;
                    return pooledChest.controller;
                }
            }

            return CreatePooledChest(chestData);
        }

        private void SwitchSlot(ChestController chestController) => chestController.SwitchSlot(slotData);

        private bool IsRequiredChest(ChestData data, ChestController controller) => (data.chestRarity == controller.GetChestRarity());

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

        private ChestController CreateController(ChestView view, ChestModel model, GameService gameService) => new ChestController(view, model, slotData, gameService);

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
    }
}
