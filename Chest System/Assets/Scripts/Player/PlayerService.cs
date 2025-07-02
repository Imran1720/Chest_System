using ChestSystem.Chest;
using ChestSystem.Events;
using ChestSystem.UI;
using UnityEngine;
namespace ChestSystem.Player
{
    public class PlayerService
    {
        PlayerController playerController;
        public PlayerService(int initialCoinCount, int initalGemCount, EventService eventService)
        {
            PlayerModel playerModel = new PlayerModel(initialCoinCount, initalGemCount, eventService);
            playerController = new PlayerController(playerModel);

            eventService.OnRewardCollected.AddListener(RewardPlayer);
        }

        public bool HasSufficientCoins(int value) => playerController.HasSufficientCoins(value);
        public bool HasSufficientGemss(int value) => playerController.HasSufficientGems(value);

        public int GetCoinCount() => playerController.GetCoinCount();
        public int GetGemCount() => playerController.GetGemCount();

        public void IncrementCoinsBy(int value) => playerController.IncrementPlayerCoinsBy(value);
        public void IncrementGemsBy(int value) => playerController.IncrementPlayerGemsBy(value);

        public void DecrementCoinsBy(int value) => playerController.DecrementPlayerCoinsBy(value);
        public void DecrementGemsBy(int value) => playerController.DecrementPlayerGemsBy(value);

        public void RewardPlayer(ChestController controller) => playerController.RewardPlayer(controller.GetCoinsToBeRewarded(), controller.GetGemsToBeRewarded());
    }
}
