using ChestSystem.Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour, IPointerDownHandler
    {
        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI chestStateText;
        [SerializeField] private TextMeshProUGUI openDurationText;
        [SerializeField] private TextMeshProUGUI openingCostText;

        [Header("UI Image")]
        [SerializeField] private Image chestIcon;
        [SerializeField] private Image background;

        [Header("Game Objects")]
        [SerializeField] private GameObject lockedStateUI;
        [SerializeField] private GameObject payUI;
        [SerializeField] private GameObject openUI;

        [Header("Color")]
        [SerializeField] private Color openBGColor;
        private Color defaultBGColor;

        private EventService eventService;

        private void Start()
        {
            defaultBGColor = background.color;
        }

        public void SetTimer(int hour, int minute)
        {
            //hour - future addition
            openDurationText.text = $"{minute}M";
        }

        public string GetChestStateText(EChestState state)
        {
            switch (state)
            {
                case EChestState.COLLECTED:
                    return "COLLECTED";
                case EChestState.UNLOCKING:
                    return "UNLOCKING";
                case EChestState.UNLOCKED:
                    return "UNLOCKED";
                default:
                    return "LOCKED";
            }
        }

        public void SetLockedUI(bool isLocked, EChestState chestState)
        {
            SetState(chestState);
            lockedStateUI.SetActive(isLocked);
            payUI.SetActive(isLocked);
            openUI.SetActive(!isLocked);
        }

        public void OnPointerDown(PointerEventData eventData) => eventService?.OnChestSelected.InvokeEvent(this);

        public void SetServices(EventService eventService) => this.eventService = eventService;
        public void SetState(EChestState state) => chestStateText.text = GetChestStateText(state);


        public void Reset() => background.color = defaultBGColor;
        public void SetChestIcon(Sprite icon) => chestIcon.sprite = icon;
        public void SetOpenedChestBG() => background.color = openBGColor;
        public void SetOpeningCost(int cost) => openingCostText.text = cost.ToString();
    }
}
