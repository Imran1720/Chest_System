using ChestSystem.Chest;
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
        public static UIService Instance;
        private PlayerService playerService;
        private SlotService slotService;
        private PopUpService popUpService;
        private ChestService chestService;

        [Header("PopUp-UI")]
        [SerializeField] private PopUpService popUpServicePrefab;

        [Header("CURRENCY")]
        [SerializeField] private TextMeshProUGUI coinCountText;
        [SerializeField] private TextMeshProUGUI gemCountText;

        [Header("SLOT-DATA")]
        [SerializeField] private GameObject slotContainer;
        [SerializeField] private SlotData slotPrefab;
        [SerializeField] private int initialSlotCount;
        [SerializeField] private Button addSlotButton;

        [Header("CHEST-DATA")]
        [SerializeField] ChestSO chestSO;
        [SerializeField] ChestView chestPrefab;
        [SerializeField] private Button generateChestButton;

        [SerializeField] private Button undoButton;
        private CommandInvoker commandInvoker;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitializeSevices();
            InitializeButtonListeners();
            UpdateCurrencies();
        }


        private void InitializeButtonListeners()
        {
            addSlotButton.onClick.AddListener(AddSlot);
            generateChestButton.onClick.AddListener(GenerateChest);
            undoButton.onClick.AddListener(UndoPurchase);
        }

        private void InitializeSevices()
        {
            playerService = new PlayerService(3, 3);
            slotService = new SlotService(slotPrefab, slotContainer, initialSlotCount);
            chestService = new ChestService(chestSO, chestPrefab);
            popUpService = Instantiate(popUpServicePrefab);
            popUpService.transform.SetParent(transform, false);
            commandInvoker = new CommandInvoker();

            popUpService.SetCommandInvoker(commandInvoker);
        }

        public void UpdateCurrencies()
        {
            UpdateCoinCount();
            UpdateGemCount();
        }

        public void ClearCommandHistory() => commandInvoker.ClearHistory();

        private void UpdateCoinCount() => coinCountText.text = playerService.GetCoinCount().ToString();
        private void UpdateGemCount() => gemCountText.text = playerService.GetGemCount().ToString();

        private void AddSlot() => slotService.AddEmptySlot();

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

        private void UndoPurchase() => commandInvoker.UndoCommand();

        public ChestService GetChestService() => chestService;
        public PopUpService GetPopUpService() => popUpService;
        public PlayerService GetPlayerService() => playerService;
        public SlotService GetSlotService() => slotService;
    }
}
