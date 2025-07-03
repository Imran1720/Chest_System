using ChestSystem.Chest;
using ChestSystem.Core;
using ChestSystem.Events;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotController
    {
        private SlotData slotPrefab;
        private List<SlotData> slotList;

        private GameObject slotsContainer;
        private EventService eventService;

        public SlotController(SlotData slotPrefab, GameObject slotsContainer, int initialSlotsCount, EventService eventService)
        {
            this.slotPrefab = slotPrefab;
            this.slotsContainer = slotsContainer;
            this.eventService = eventService;

            slotList = new List<SlotData>();
            SpawnInitialSlots(initialSlotsCount);

            AddEventListeners();
        }

        private void SpawnInitialSlots(int numberOfSlots)
        {
            for (int i = 0; i < numberOfSlots; i++)
            {
                AddEmptySlot();
            }
        }

        public void AddEmptySlot()
        {
            SlotData slot = GameObject.Instantiate(slotPrefab);
            slot.transform.SetParent(slotsContainer.transform, false);
            slotList.Add(slot);
        }

        public void EmptySlot(SlotData slotToBeEmptied)
        {
            SlotData slot = slotList.Find(slotListItem => slotListItem.Equals(slotToBeEmptied));
            slot?.EmptySlot();
        }
        public void FillSlot(SlotData slotToBeFilled)
        {
            SlotData slot = slotList.Find(slotListItem => slotListItem.Equals(slotToBeFilled));
            slot?.FillSlot();
        }

        private int GetEmptySlotCount()
        {
            int count = 0;
            foreach (SlotData slot in slotList)
            {
                if (slot.IsSlotEmpty()) count++;
            }
            return count;
        }

        private void AddEventListeners() => eventService.OnRewardCollected.AddListener(OnRewardCollected);
        public void RemoveEventListeners() => eventService.OnRewardCollected.RemoveListener(OnRewardCollected);

        public bool IsEmptySlotAvailable() => GetEmptySlotCount() > 0;
        public SlotData GetEmptySlot() => slotList.Find(slot => slot.IsSlotEmpty());

        private void OnRewardCollected(ChestController controller) => EmptySlot(controller.GetCurrentSlot());
    }
}
