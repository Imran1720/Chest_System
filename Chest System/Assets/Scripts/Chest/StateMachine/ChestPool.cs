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

        public ChestPool(ChestView chestPrefab)
        {
            this.chestPrefab = chestPrefab;
        }

        public void SetSlotData(SlotData slotData) => this.slotData = slotData;

        public ChestController GetChest(ChestData chestData)
        {
            slotData.FillSlot();
            if (pooledChestsList.Count > 0)
            {
                PooledChest pooledChest = pooledChestsList.Find(item => !item.isUsed && IsRequiredChest(chestData, item.controller));
                if (pooledChest != null)
                {
                    SetPosition(pooledChest.controller);
                    pooledChest.controller.Reset();
                    pooledChest.isUsed = true;
                    return pooledChest.controller;
                }
            }

            return CreatePooledChest(chestData);
        }

        private void SetPosition(ChestController chestController)
        {
            chestController.SetPosition(slotData.transform);
        }

        private bool IsRequiredChest(ChestData data, ChestController controller)
        {
            return data.chestRarity == controller.GetChestRarity();
        }

        private ChestController CreatePooledChest(ChestData data)
        {
            PooledChest pooledChest = new PooledChest();

            ChestView view = CreateChestView();
            ChestModel model = new ChestModel(data);
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
