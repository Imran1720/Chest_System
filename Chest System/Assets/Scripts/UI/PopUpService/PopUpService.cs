using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.PopUp
{
    public class PopUpService : MonoBehaviour
    {
        [Header("POPUP-BOX")]
        [SerializeField] private GameObject popUpBox;

        [Header("BUTTONS")]
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button buyWithGemButton;
        [SerializeField] private Button closePopUpButton;

        [Header("MESSAGE")]
        [SerializeField] private TextMeshProUGUI messageText;

    }
}
