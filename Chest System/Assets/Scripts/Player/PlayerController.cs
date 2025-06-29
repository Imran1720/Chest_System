using ChestSystem.UI;
using System.Diagnostics;

namespace ChestSystem.Player
{
    public class PlayerController
    {
        private PlayerModel playerModel;

        public PlayerController(PlayerModel playerModel) => this.playerModel = playerModel;

        public void IncrementPlayerCoinsBy(int value)
        {
            int currentPlayerCoins = playerModel.GetCoinCount();
            playerModel.SetCoinCount(currentPlayerCoins + value);
            UIService.Instance.UpdateCurrencies();
        }

        public void IncrementPlayerGemsBy(int value)
        {
            int currentPlayerGems = playerModel.GetGemCount();
            playerModel.SetGemCount(currentPlayerGems + value);
            UIService.Instance.UpdateCurrencies();
        }

        public void DecrementPlayerCoinsBy(int value)
        {
            int currentPlayerCoins = playerModel.GetCoinCount();
            currentPlayerCoins -= value;
            currentPlayerCoins = (currentPlayerCoins <= 0) ? 0 : currentPlayerCoins;

            playerModel.SetCoinCount(currentPlayerCoins);
            UIService.Instance.UpdateCurrencies();
        }

        public void DecrementPlayerGemsBy(int value)
        {
            int currentPlayerGems = playerModel.GetGemCount();
            currentPlayerGems -= value;
            currentPlayerGems = (currentPlayerGems <= 0) ? 0 : currentPlayerGems;

            playerModel.SetGemCount(currentPlayerGems);
            UIService.Instance.UpdateCurrencies();
        }

        public bool HasSufficientCoins(int amount) => playerModel.GetCoinCount() >= amount;
        public bool HasSufficientGems(int amount) => playerModel.GetGemCount() >= amount;

        public int GetCoinCount() => playerModel.GetCoinCount();
        public int GetGemCount() => playerModel.GetGemCount();

        public void RewardPlayer(int coins, int gems)
        {
            playerModel.AddCoins(coins);
            playerModel.AddGems(gems);
        }
    }
}
