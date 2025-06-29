using ChestSystem.UI.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestPool
    {
        private ChestView chestPrefab;
        private ChestData chestData;
        private SlotData slotData;

        private List<PooledChest> pooledChestsList = new List<PooledChest>();

        public ChestPool(ChestView chestPrefab, ChestData chestData)
        {
            this.chestPrefab = chestPrefab;
            this.chestData = chestData;
        }

        public void SetSlotData(SlotData slotData) => this.slotData = slotData;

        public ChestController GetChest()
        {
            if (pooledChestsList.Count != 0)
            {
                PooledChest pooledChest = pooledChestsList.Find(item => !item.isUsed);
                if (pooledChest != null)
                {
                    pooledChest.isUsed = true;
                    return pooledChest.controller;
                }
            }

            return CreatePooledChest();
        }

        private ChestController CreatePooledChest()
        {
            PooledChest pooledChest = new PooledChest();

            ChestView view = CreateChestView();
            ChestModel model = new ChestModel(chestData);
            pooledChest.controller = CreateController(view, model);
            pooledChest.isUsed = true;
            pooledChestsList.Add(pooledChest);

            return pooledChest.controller;
        }

        private ChestController CreateController(ChestView view, ChestModel model) => new ChestController(view, model, slotData);

        private ChestView CreateChestView()
        {
            ChestView chestView = GameObject.Instantiate(chestPrefab, slotData.transform.position, Quaternion.identity);
            chestView.transform.SetParent(slotData.transform, true);

            return chestView;
        }

        public void ReturnChest(ChestController controller)
        {
            PooledChest pooledChest = pooledChestsList.Find(item => item.controller.Equals(controller));
            pooledChest.isUsed = false;
        }
    }
}
