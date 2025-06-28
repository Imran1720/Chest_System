using ChestSystem.UI.Slot;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        ChestSO chestSO;
        ChestView chestPrefab;
        ChestController chestController;

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

            slotData.FillSlot();
        }

        public void Update() => chestController?.Update();

        private ChestData GetRandomChest()
        {
            int randomChestIndex = Random.Range(0, chestSO.ChestTypeList.Length);
            return chestSO.ChestTypeList[randomChestIndex];
        }
    }
}
