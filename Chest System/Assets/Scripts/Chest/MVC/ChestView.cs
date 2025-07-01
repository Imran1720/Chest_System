using ChestSystem.Chest;
using ChestSystem.Events;
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

    private EventService eventService;

    private void Start() => defaultBGColor = background.color;
    public void SetServices(EventService eventService) => this.eventService = eventService;
    public void OnPointerDown(PointerEventData eventData) => eventService.OnChestSelected.InvokeEvent(this);
    public void SetState(EChestState state) => chestStateText.text = GetChestStateText(state);
    public void SetTimer(int hour, int minute)
    {
        OpenDurationText.text = "";
        OpenDurationText.text += minute + "M";
    }

    public void SetChestIcon(Sprite icon) => chestIcon.sprite = icon;
    public void SetOpeningCost(int cost) => openingCostText.text = cost.ToString();

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

    public void SetLockedUI(bool value, EChestState chestState)
    {
        SetState(chestState);
        lockedStateUI.SetActive(value);
        payUI.SetActive(value);
        OpenUI.SetActive(!value);
    }

    public void Reset() => background.color = defaultBGColor;
    public void SetOpenedChestBG() => background.color = openBGColor;
}
