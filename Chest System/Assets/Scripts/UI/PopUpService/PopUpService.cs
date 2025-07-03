using ChestSystem.Chest;
using ChestSystem.Core;
using ChestSystem.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.PopUp
{
    public class PopUpService : MonoBehaviour
    {
        [Header("PopUp Box")]
        [SerializeField] private GameObject buyPopUpBox;
        [SerializeField] private GameObject warningPopUpBox;
        [SerializeField] private GameObject popUpObject;
        [SerializeField] private Image backgroundImage;

        [Header("Buttons")]
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button buyWithGemButton;
        [SerializeField] private Button closePopUpButton;

        [Header("UI Text")]
        [SerializeField] private TextMeshProUGUI warningMessageText;
        [SerializeField] private TextMeshProUGUI openingCostText;

        [Header("Scriptable Object")]
        [SerializeField] private PopUpMessagesSO messagesList;

        private EventService eventService;
        private CommandInvoker commandInvoker;
        private ChestController chestController;

        private void Start()
        {
            InitializeButtonListeners();
        }

        public void InitializeServices(GameService gameService)
        {
            eventService = gameService.GetEventService();
            AddEventListeners();
        }

        private void OnDisable()
        {
            RemoveEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.OnChestBought.AddListener(OnChestBought);
            eventService.OnRewardCollected.AddListener(ShowRewards);
            eventService.OnInsufficientFunds.AddListener(OnInsufficientFunds);
            eventService.OnLockedChestClicked.AddListener(OnLockedChestClicked);
            eventService.OnUnlockingChestClicked.AddListener(OnUnlockingChestClicked);
        }

        private void RemoveEventListeners()
        {
            eventService.OnChestBought.RemoveListener(OnChestBought);
            eventService.OnRewardCollected.RemoveListener(ShowRewards);
            eventService.OnInsufficientFunds.RemoveListener(OnInsufficientFunds);
            eventService.OnLockedChestClicked.RemoveListener(OnLockedChestClicked);
            eventService.OnUnlockingChestClicked.RemoveListener(OnUnlockingChestClicked);
        }

        private void InitializeButtonListeners()
        {
            startTimerButton.onClick.AddListener(StartTimer);
            closePopUpButton.onClick.AddListener(ClosePopUp);
            buyWithGemButton.onClick.AddListener(BuyWithGems);
        }

        private void ShowChestPurchasePopUp(ChestController controller, bool showStartTimer)
        {
            chestController = controller;
            ShowPopUp();
            startTimerButton.gameObject.SetActive(showStartTimer);
            openingCostText.text = (controller.GetChestBuyingCost()).ToString();
            buyPopUpBox.SetActive(true);
        }

        private void ShowPopUp()
        {
            ClosePopUp();
            SetPopUpElementsActive(true);
        }

        public void ClosePopUp()
        {
            buyPopUpBox.SetActive(false);
            warningPopUpBox.SetActive(false);
            SetPopUpElementsActive(false);
        }

        private void SetPopUpElementsActive(bool isActive)
        {
            closePopUpButton.gameObject.SetActive(isActive);
            popUpObject.SetActive(isActive);
            backgroundImage.enabled = isActive;
        }

        private void ShowWarningPopUp(string warningText)
        {
            ShowPopUp();
            warningMessageText.text = warningText;
            warningPopUpBox.SetActive(true);
        }

        public void ShowRewards(ChestController chestController)
        {
            int gemsCount = chestController.GetGemsToBeRewarded();
            int coinsCount = chestController.GetCoinsToBeRewarded();
            ShowWarningPopUp($"You Got \n{coinsCount} Coins & {gemsCount} Gems!!");
        }

        private void StartTimer()
        {
            chestController.StartTimer();
            ClosePopUp();
        }

        private void BuyWithGems()
        {
            ICommand buyCommand = new BuyChestCommand(chestController, eventService);
            commandInvoker.AddCommand(buyCommand);
        }

        private void OnLockedChestClicked(ChestController controller)
        {
            if (controller.CanUnlockChest())
            {
                ShowUnlockPopUP(controller);
            }
            else
            {
                ShowChestOpeningPopUP();
            }
        }

        public void SetCommandInvoker(CommandInvoker commandInvoker) => this.commandInvoker = commandInvoker;

        //Event Listeners
        private void OnInsufficientFunds() => ShowInsufficientFundPopUP();
        private void OnChestBought(ChestController controller) => ClosePopUp();
        private void OnUnlockingChestClicked(ChestController controller) => ShowBuyPopUP(controller);

        //PopUps      
        private void ShowUnlockPopUP(ChestController controller) => ShowChestPurchasePopUp(controller, true);
        private void ShowBuyPopUP(ChestController controller) => ShowChestPurchasePopUp(chestController, false);

        public void ShowSlotsFullPopUP() => ShowWarningPopUp(messagesList.slotFullMessage);
        private void ShowInsufficientFundPopUP() => ShowWarningPopUp(messagesList.noFundsMessage);
        private void ShowChestOpeningPopUP() => ShowWarningPopUp(messagesList.chestOpeningMessage);
    }
}
