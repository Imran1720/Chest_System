using ChestSystem.Chest;
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

    private ChestController chestController;

    public void OnPointerDown(PointerEventData eventData)
    {
        chestController.StartChestTimer();
    }

    public void SetState(EChestState state) => chestStateText.text = GetChestStateText(state);
    public void SetTimer(int minute)
    {
        //if (hour > 0)
        //{
        //    OpenDurationText.text += hour + "H";
        //}
        OpenDurationText.text = minute + "M";
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

    public void SetChestController(ChestController chestController) => this.chestController = chestController;
}
