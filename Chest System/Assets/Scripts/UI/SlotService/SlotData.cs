using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.Slot
{
    public class SlotData : MonoBehaviour
    {
        private bool isUsed;
        [SerializeField] private Image slot;
        [SerializeField] private Sprite emptySlot;
        [SerializeField] private Sprite occupiedSlot;

        public void FillSlot()
        {
            isUsed = true;
            slot.sprite = occupiedSlot;
        }
        public void EmptySlot()
        {
            isUsed = false;
            slot.sprite = emptySlot;
        }
        public bool IsSlotEmpty() => !isUsed;
    }
}
