using ChestSystem.Chest;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.PopUp
{
    public class PopUpService : MonoBehaviour
    {
        [Header("POPUP-BOX")]
        [SerializeField] private GameObject buyPopUpBox;
        [SerializeField] private GameObject warningPopUpBox;
        [SerializeField] private GameObject popUpObject;
        [SerializeField] private Image backgroundImage;

        [Header("BUTTONS")]
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button buyWithGemButton;
        [SerializeField] private Button closePopUpButton;

        [Header("MESSAGE")]
        [SerializeField] private TextMeshProUGUI warningMessageText;

        [SerializeField] private TextMeshProUGUI openingCostText;

        private ChestController chestController;
        private CommandInvoker commandInvoker;

        private void Start()
        {
            InitializeButtonListeners();
        }

        public void SetCommandInvoker(CommandInvoker commandInvoker) => this.commandInvoker = commandInvoker;

        private void InitializeButtonListeners()
        {
            startTimerButton.onClick.AddListener(StartTimer);
            buyWithGemButton.onClick.AddListener(BuyWithGems);
            closePopUpButton.onClick.AddListener(ClosePopUp);
        }

        private void HidePopUp() => popUpObject.SetActive(false);
        private void ShowPopUp()
        {
            backgroundImage.enabled = true;
            popUpObject.SetActive(true);
            closePopUpButton.gameObject.SetActive(true);
        }

        public void ShowUnlockPopUP(ChestController controller)
        {
            chestController = controller;
            startTimerButton.gameObject.SetActive(true);
            openingCostText.text = (chestController.GetChestBuyingCost()).ToString();
            ShowPopUp();
            buyPopUpBox.SetActive(true);
        }

        public void ShowBuyPopUP(ChestController controller)
        {
            chestController = controller;
            openingCostText.text = (chestController.GetChestBuyingCost()).ToString();
            ShowPopUp();
            buyPopUpBox.SetActive(true);
            startTimerButton.gameObject.SetActive(false);
        }

        public void ShowSlotsFullPopUP()
        {
            ShowPopUp();
            string warning = "Chest Slots Full!!";
            warningMessageText.text = warning;
            warningPopUpBox.SetActive(true);
        }

        public void ShowChestOpeningPopUP()
        {
            ShowPopUp();
            string warning = "Already chest is opening!!";
            warningMessageText.text = warning;
            warningPopUpBox.SetActive(true);
        }

        public void ShowInsufficientFundPopUP()
        {
            ShowPopUp();
            string warning = "Insufficient Funds!!";
            warningMessageText.text = warning;
            warningPopUpBox.SetActive(true);
        }

        private void ResetPopUp()
        {
            closePopUpButton.gameObject.SetActive(false);
            buyPopUpBox.SetActive(false);
            warningPopUpBox.SetActive(false);
            HidePopUp();
            backgroundImage.enabled = false;
        }


        private void StartTimer()
        {
            chestController.StartTimer();
            ClosePopUp();
        }

        private void BuyWithGems()
        {
            ICommand buyCommand = new BuyChestCommand(chestController, this);
            commandInvoker.AddCommand(buyCommand);
        }

        public void ClosePopUp() => ResetPopUp();
    }
}
