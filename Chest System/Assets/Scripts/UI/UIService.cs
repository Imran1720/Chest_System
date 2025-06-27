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



        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CreateData();
            InitializeSevices();
            InitializeButtonListeners();
            UpdateCurrencies();
        }

        private void CreateData()
        {
            popUpService = Instantiate(popUpServicePrefab);
            popUpService.transform.SetParent(transform, false);
        }

        private void InitializeButtonListeners()
        {
            addSlotButton.onClick.AddListener(AddSlot);
        }

        private void InitializeSevices()
        {
            playerService = new PlayerService(100, 100);
            slotService = new SlotService(slotPrefab, slotContainer, initialSlotCount);
        }

        public void UpdateCurrencies()
        {
            UpdateCoinCount();
            UpdateGemCount();
        }


        private void Update()
        {
            //if (Input.GetKeyUp(KeyCode.A))
            //{
            //    int timerValueInMinutes = 11;
            //    float time = (float)timerValueInMinutes / 10;
            //    int buyingCost = (int)Mathf.Ceil(time) * 10;
            //    Debug.Log(time + " " + buyingCost);
            //}

            //if (Input.GetKeyUp(KeyCode.S))
            //{
            //    popUpService.ShowWarningPopUP();
            //}

        }
        private void UpdateCoinCount() => coinCountText.text = playerService.GetCoinCount().ToString();
        private void UpdateGemCount() => gemCountText.text = playerService.GetGemCount().ToString();

        private void AddSlot() => slotService.AddEmptySlot();
    }
}
