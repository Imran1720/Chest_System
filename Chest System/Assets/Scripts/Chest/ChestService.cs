using ChestSystem.UI.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        ChestSO chestSO;
        ChestView chestPrefab;
        ChestPool chestPool;

        private bool isUnlockingChest = true;

        private List<ChestController> chestControllersList = new List<ChestController>();

        public ChestService(ChestSO chestSO, ChestView chestPrefab)
        {
            this.chestSO = chestSO;
            this.chestPrefab = chestPrefab;

            chestPool = new ChestPool(chestPrefab);
        }

        public void CreateChest(SlotData slotData)
        {
            chestPool.SetSlotData(slotData);
            ChestController controller = chestPool.GetChest(GetRandomChest());
            if (chestControllersList.Find(item => item == controller) == null)
            {
                chestControllersList.Add(controller);
            }
            controller.SetViewActive();
        }

        public void Update()
        {
            foreach (var controller in chestControllersList)
            {
                controller?.Update();
            }
        }

        private ChestData GetRandomChest()
        {
            int randomChestIndex = Random.Range(0, chestSO.ChestTypeList.Length);
            return chestSO.ChestTypeList[randomChestIndex];
        }

        public bool CanUnlockChest() => isUnlockingChest;

        public void SetUnlockingChest(bool value) => isUnlockingChest = value;

        public void ReturnChestToPool(ChestController controller) => chestPool.ReturnChest(controller);
    }
}
