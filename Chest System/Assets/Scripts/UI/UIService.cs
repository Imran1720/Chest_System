using ChestSystem.Chest;
using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI.PopUp;
using ChestSystem.UI.Slot;
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
        private EventService eventService;
        private PlayerService playerService;

        [Header("PopUp UI")]
        [SerializeField] private PopUpService popUpServicePrefab;

        [Header("Currency")]
        [SerializeField] private TextMeshProUGUI gemCountText;
        [SerializeField] private TextMeshProUGUI coinCountText;

        [Header("Slot Data")]
        [SerializeField] private SlotData slotPrefab;
        [SerializeField] private int initialSlotCount;
        [SerializeField] private GameObject slotContainer;

        [Header("Buttons")]
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

        public void CreateSlotService(EventService eventService)
        {
            slotService = new SlotService(slotPrefab, slotContainer, initialSlotCount, eventService);
        }

        public void InitializeServices(GameService gameService)
        {
            playerService = gameService.GetPlayerService();
            chestService = gameService.GetChestService();
            eventService = gameService.GetEventService();

            SetUpPopUpService(gameService);

            AddEventListeners();
            UpdateCurrencies();
        }

        private void SetUpPopUpService(GameService gameService)
        {
            popUpService = Instantiate(popUpServicePrefab);
            popUpService.transform.SetParent(transform, false);
            popUpService.InitializeServices(gameService);
        }

        private void AddEventListeners()
        {
            eventService.OnChestBought.AddListener(OnChestBought);
            eventService.OnCurrencyUpdated.AddListener(OnCurrencyUpdated);
        }

        private void OnDisable()
        {
            slotService.RemoveEventListeners();
            eventService.OnChestBought.RemoveListener(OnChestBought);
            eventService.OnCurrencyUpdated.RemoveListener(OnCurrencyUpdated);
        }

        public void UpdateCurrencies()
        {
            UpdateCoinCount();
            UpdateGemCount();
        }

        private void GenerateChest()
        {
            eventService.OnButtonClickSoundRequested.InvokeEvent();
            if (!slotService.IsEmptySlotAvailable())
            {
                popUpService.ShowSlotsFullPopUP();
                return;
            }

            SlotData slotData = slotService.GetEmptySlot();
            chestService.CreateChest(slotData);
        }
        private void AddSlot()
        {
            eventService.OnButtonClickSoundRequested.InvokeEvent();
            slotService.AddEmptySlot();
        }
        private void UndoPurchase()
        {
            eventService.OnButtonClickSoundRequested.InvokeEvent();
            eventService.OnUndoClicked.InvokeEvent();
        }

        public SlotService GetSlotService() => slotService;
        public void OnCurrencyUpdated() => UpdateCurrencies();
        public PopUpService GetPopUpService() => popUpService;
        private void OnChestBought(ChestController controller) => UpdateCurrencies();
        private void UpdateGemCount() => gemCountText.text = playerService.GetGemCount().ToString();
        private void UpdateCoinCount() => coinCountText.text = playerService.GetCoinCount().ToString();
    }
}
