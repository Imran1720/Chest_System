using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class SlotData : MonoBehaviour
    {
        private bool isUsed;

        public void FillSlot() => isUsed = true;
        public void EmptySlot() => isUsed = false;
        public bool IsSlotEmpty() => !isUsed;
    }
}
