using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotController
    {
        private SlotData slotPrefab;
        private List<SlotData> slotList;

        private GameObject slotsContainer;

        public SlotController(SlotData slotPrefab, GameObject slotsContainer, int initialSlotsCount)
        {
            this.slotPrefab = slotPrefab;
            this.slotsContainer = slotsContainer;

            slotList = new List<SlotData>();

            SpawnInitialSlots(initialSlotsCount);
        }

        private void SpawnInitialSlots(int numberOfSlots)
        {
            for (int i = 0; i < numberOfSlots; i++)
            {
                AddEmptySlot();
            }
        }

        public bool IsEmptySlotAvailable() => GetEmptySlotCount() > 0;

        public SlotData GetEmptySlot() => slotList.Find(slot => slot.isSlotEmpty());

        public void AddEmptySlot()
        {
            SlotData slot = GameObject.Instantiate(slotPrefab);
            slot.transform.SetParent(slotsContainer.transform, false);
            slotList.Add(slot);
        }

        public void EmptySlot(SlotData slotToBeEmptied)
        {
            SlotData slot = slotList.Find(item => item.Equals(slotToBeEmptied));
            slot.EmptySlot();
        }
        public void FillSlot(SlotData slotToBeFilled)
        {
            SlotData slot = slotList.Find(item => item.Equals(slotToBeFilled));
            slot.FillSlot();
        }

        private int GetEmptySlotCount()
        {
            int count = 0;
            foreach (SlotData slot in slotList)
            {
                if (slot.isSlotEmpty()) count++;
            }
            return count;
        }
    }
}
