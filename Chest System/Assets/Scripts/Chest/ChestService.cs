using ChestSystem.UI.Slot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        ChestSO chestSO;
        ChestView chestPrefab;
        ChestController chestController;
        private bool isUnlockingChest = true;

        private List<ChestController> chestControllersList = new List<ChestController>();

        public ChestService(ChestSO chestSO, ChestView chestPrefab)
        {
            this.chestSO = chestSO;
            this.chestPrefab = chestPrefab;
        }

        public void CreateChest(SlotData slotData)
        {
            ChestView chestView = GameObject.Instantiate(chestPrefab, slotData.transform.position, Quaternion.identity);
            chestView.transform.SetParent(slotData.transform, true);

            ChestModel chestModel = new ChestModel(GetRandomChest());

            chestController = new ChestController(chestView, chestModel, slotData);

            chestControllersList.Add(chestController);
            slotData.FillSlot();
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
    }
}
