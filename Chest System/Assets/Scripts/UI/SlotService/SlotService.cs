using ChestSystem.Events;
using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotService
    {
        private SlotController slotController;
        public SlotService(SlotData slotPrefab, GameObject container, int initialSlotCount, EventService eventService)
        {
            slotController = new SlotController(slotPrefab, container, initialSlotCount, eventService);
        }

        public bool IsEmptySlotAvailable() => slotController.IsEmptySlotAvailable();

        public SlotData GetEmptySlot() => slotController.GetEmptySlot();

        public void AddEmptySlot() => slotController.AddEmptySlot();
        public void FillSlot(SlotData slot) => slotController.FillSlot(slot);
        public void EmptySlot(SlotData slot) => slotController.EmptySlot(slot);

        public void RemoveEventListeners() => slotController.RemoveEventListeners();
    }
}
