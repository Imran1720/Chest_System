using ChestSystem.Chest;
using ChestSystem.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI chestStateText;
    [SerializeField] private TextMeshProUGUI OpenDurationText;
    [SerializeField] private TextMeshProUGUI openingCostText;

    [SerializeField] private Image chestIcon;

    [SerializeField] private GameObject lockedStateUI;
    [SerializeField] private GameObject payUI;
    [SerializeField] private GameObject OpenUI;

    [SerializeField] private Image background;
    [SerializeField] private Color openBGColor;
    private Color defaultBGColor;


    private ChestController chestController;

    private void Start()
    {
        defaultBGColor = background.color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        chestController.OnSelectingChest();
    }

    public void SetState(EChestState state) => chestStateText.text = GetChestStateText(state);
    public void SetTimer(int hour, int minute)
    {
        OpenDurationText.text = "";
        //if (hour > 0)
        //{
        //    OpenDurationText.text += hour + "H ";
        //}
        OpenDurationText.text += minute + "M";
    }

    public void SetOpeningCost(int cost) => openingCostText.text = cost.ToString();

    public void SetChestIcon(Sprite icon) => chestIcon.sprite = icon;

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

    public void SetLockedUI(bool value)
    {
        SetState(chestController.GetCurrentChestState());
        lockedStateUI.SetActive(value);
        payUI.SetActive(value);
        OpenUI.SetActive(!value);
    }

    public void SetOpenedChestBG() => background.color = openBGColor;
    public void SetChestController(ChestController chestController) => this.chestController = chestController;
    public void Reset()
    {
        background.color = defaultBGColor;
    }
}
