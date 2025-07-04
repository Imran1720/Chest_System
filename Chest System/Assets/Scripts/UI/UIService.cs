using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIService : MonoBehaviour
{
    public static UIService Instance;

    private PlayerService playerService;

    [Header("CURRENCY")]
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private TextMeshProUGUI gemCountText;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerService = new PlayerService(100, 100);
        UpdateCurrencies();
    }

    public void UpdateCurrencies()
    {
        UpdateCoinCount();
        UpdateGemCount();
    }

    private void UpdateCoinCount() => coinCountText.text = playerService.GetCoinCount().ToString();
    private void UpdateGemCount() => gemCountText.text = playerService.GetGemCount().ToString();


}
