using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotController
    {
        private Slot slotPrefab;
        private List<Slot> slotList;

        private GameObject slotsContainer;

        public SlotController(Slot slotPrefab, GameObject slotsContainer, int initialSlotsCount)
        {
            this.slotPrefab = slotPrefab;
            this.slotsContainer = slotsContainer;

            slotList = new List<Slot>();

            SpawnInitialSlots(initialSlotsCount);
        }

        private void SpawnInitialSlots(int numberOfSlots)
        {
            for (int i = 0; i < numberOfSlots; i++)
            {
                AddEmptySlot();
            }
        }

        public bool CanAddChest() => GetEmptySlotCount() > 0;

        public Slot GetEmptySlot() => slotList.Find(slot => slot.isSlotEmpty());

        public void AddEmptySlot()
        {
            Slot slot = GameObject.Instantiate(slotPrefab);
            slot.transform.SetParent(slotsContainer.transform, false);
            slotList.Add(slot);
        }

        public void EmptySlot(Slot slotToBeEmptied) => slotToBeEmptied.EmptySlot();

        private int GetEmptySlotCount()
        {
            int count = 0;
            foreach (Slot slot in slotList)
            {
                if (slot.isSlotEmpty()) count++;
            }
            return count;
        }
    }
}
