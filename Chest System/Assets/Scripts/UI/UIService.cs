using ChestSystem.Chest;
using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI.PopUp;
using ChestSystem.UI.Slot;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        private SlotService slotService;
        private PopUpService popUpService;
        private ChestService chestService;
        private PlayerService playerService;
        private EventService eventService;

        [Header("PopUp-UI")]
        [SerializeField] private PopUpService popUpServicePrefab;

        [Header("CURRENCY")]
        [SerializeField] private TextMeshProUGUI gemCountText;
        [SerializeField] private TextMeshProUGUI coinCountText;

        [Header("SLOT-DATA")]
        [SerializeField] private SlotData slotPrefab;
        [SerializeField] private int initialSlotCount;
        [SerializeField] private GameObject slotContainer;

        [Header("BUTTONS")]
        [SerializeField] private Button undoButton;
        [SerializeField] private Button addSlotButton;
        [SerializeField] private Button generateChestButton;

        private void Start()
        {
            InitializeButtonListeners();
        }

        private void InitializeButtonListeners()
        {
            addSlotButton.onClick.AddListener(AddSlot);
            undoButton.onClick.AddListener(UndoPurchase);
            generateChestButton.onClick.AddListener(GenerateChest);
        }

        public void CreateSlotService(EventService eventService) => slotService = new SlotService(slotPrefab, slotContainer, initialSlotCount, eventService);

        public void InitializeSevices(GameService gameService)
        {
            chestService = gameService.GetChestService();
            playerService = gameService.GetPlayerService();
            eventService = gameService.GetEventService();

            popUpService = Instantiate(popUpServicePrefab);
            popUpService.transform.SetParent(transform, false);
            popUpService.InitializeServices(gameService);


            AddEventListeners();
            UpdateCurrencies();
        }

        private void AddEventListeners()
        {
            eventService.OnChestBought.AddListener(OnChestBought);
            eventService.OnCurrencyUpdated.AddListener(OnCurrencyUpdated);
        }
        private void OnDisable()
        {
            eventService.OnChestBought.RemoveListener(OnChestBought);
            eventService.OnCurrencyUpdated.RemoveListener(OnCurrencyUpdated);
        }

        public void UpdateCurrencies()
        {
            UpdateCoinCount();
            UpdateGemCount();
        }

        private void AddSlot() => slotService.AddEmptySlot();
        private void UndoPurchase() => eventService.OnUndoClicked.InvokeEvent();
        private void UpdateGemCount() => gemCountText.text = playerService.GetGemCount().ToString();
        private void UpdateCoinCount() => coinCountText.text = playerService.GetCoinCount().ToString();


        private void OnChestBought(ChestController controller) => UpdateCurrencies();
        private void GenerateChest()
        {
            if (!slotService.IsEmptySlotAvailable())
            {
                popUpService.ShowSlotsFullPopUP();
                return;
            }

            SlotData slotData = slotService.GetEmptySlot();
            chestService.CreateChest(slotData);
        }

        public void OnCurrencyUpdated() => UpdateCurrencies();

        public SlotService GetSlotService() => slotService;
        public PopUpService GetPopUpService() => popUpService;
    }
}
