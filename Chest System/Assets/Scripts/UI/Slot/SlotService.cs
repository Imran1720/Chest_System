using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotService
    {
        private SlotController slotController;
        public SlotService(SlotData slotPrefab, GameObject container, int initialSlotCount)
        {
            slotController = new SlotController(slotPrefab, container, initialSlotCount);
        }

        public bool IsEmptySlotAvailable() => slotController.IsEmptySlotAvailable();
        public SlotData GetEmptySlot() => slotController.GetEmptySlot();
        public void AddEmptySlot() => slotController.AddEmptySlot();
        public void EmptySlot(SlotData slot) => slotController.EmptySlot(slot);
        public void FillSlot(SlotData slot) => slotController.FillSlot(slot);

    }
}
