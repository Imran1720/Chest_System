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


        private void Start()
        {
            InitializeButtonListeners();
        }

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

        public void ShowBuyPopUP()
        {
            ShowPopUp();
            buyPopUpBox.SetActive(true);
        }

        public void ShowWarningPopUP()
        {
            ShowPopUp();
            string warning = "Can not get chest";
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
            Debug.Log("Timer Started!!");
        }

        private void BuyWithGems()
        {
            Debug.Log("Chest Bought!!");
        }

        private void ClosePopUp() => ResetPopUp();
    }
}
