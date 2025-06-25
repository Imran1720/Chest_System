using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    PlayerController playerController;
    public PlayerService(int initialCoinCount, int initalGemCount)
    {
        PlayerModel playerModel = new PlayerModel(initialCoinCount, initalGemCount);
        playerController = new PlayerController(playerModel);
    }

    public bool HasSufficientCoins(int value) => playerController.HasSufficientCoins(value);
    public bool HasSufficientGemss(int value) => playerController.HasSufficientGems(value);

    public int GetCoinCount() => playerController.GetCoinCount();
    public int GetGemCount() => playerController.GetGemCount();

    public void IncrementCoinsBy(int value) => playerController.IncrementPlayerCoinsBy(value);
    public void IncrementGemsBy(int value) => playerController.IncrementPlayerGemsBy(value);

    public void DecrementCoinsBy(int value) => playerController.DecrementPlayerCoinsBy(value);
    public void DecrementGemsBy(int value) => playerController.DecrementPlayerGemsBy(value);
}
