using UnityEngine;

namespace ChestSystem.UI.Slot
{
    public class Slot : MonoBehaviour
    {
        private bool isUsed;

        public void FillSlot() => isUsed = true;
        public void EmptySlot() => isUsed = false;
        public bool isSlotEmpty() => isUsed;
    }
}
