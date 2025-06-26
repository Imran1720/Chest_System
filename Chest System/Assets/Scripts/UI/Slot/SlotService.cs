using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotService
    {
        private SlotController slotController;
        public SlotService(Slot slotPrefab, GameObject container, int initialSlotCount)
        {
            slotController = new SlotController(slotPrefab, container, initialSlotCount);
        }

        public bool CanAddChest() => slotController.CanAddChest();
        public Slot GetEmptySlot() => slotController.GetEmptySlot();
        public void AddEmptySlot() => slotController.AddEmptySlot();
        public void EmptySlot(Slot slot) => slotController.EmptySlot(slot);
    }
}
