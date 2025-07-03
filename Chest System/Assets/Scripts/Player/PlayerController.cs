using ChestSystem.Chest;
using ChestSystem.Events;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerController
    {
        private PlayerModel playerModel;
        private EventService eventService;

        public PlayerController(PlayerModel playerModel, EventService eventService)
        {
            this.playerModel = playerModel;
            this.eventService = eventService;

            AddEventListeners();
        }

        public void DecrementPlayerGemsBy(int value)
        {
            int currentPlayerGems = playerModel.GetGemCount() - value;
            playerModel.SetGemCount(Mathf.Max(0, currentPlayerGems));
        }

        public void DecrementPlayerCoinsBy(int value)
        {
            int currentPlayerCoins = playerModel.GetCoinCount() - value;
            playerModel.SetCoinCount(Mathf.Max(0, currentPlayerCoins));
        }

        public void RewardPlayer(int coins, int gems)
        {
            playerModel.AddGems(gems);
            playerModel.AddCoins(coins);
        }

        private void OnRewardPlayer(ChestController controller)
        {
            RewardPlayer(controller.GetCoinsToBeRewarded(), controller.GetGemsToBeRewarded());
        }

        public int GetGemCount() => playerModel.GetGemCount();
        public int GetCoinCount() => playerModel.GetCoinCount();

        public bool HasSufficientGems(int amount) => playerModel.GetGemCount() >= amount;
        public bool HasSufficientCoins(int amount) => playerModel.GetCoinCount() >= amount;

        private void AddEventListeners() => eventService.OnRewardCollected.AddListener(OnRewardPlayer);
        public void RemoveEventListeners() => eventService.OnRewardCollected.RemoveListener(OnRewardPlayer);

        public void IncrementPlayerGemsBy(int value) => playerModel.SetGemCount(playerModel.GetGemCount() + value);
        public void IncrementPlayerCoinsBy(int value) => playerModel.SetCoinCount(playerModel.GetCoinCount() + value);
    }
}
