using ChestSystem.Core;
using ChestSystem.Events;

namespace ChestSystem.Player
{
    public class PlayerModel
    {
        private int coinCount;
        private int gemCount;
        private EventService eventService;

        public PlayerModel(int initialCoinCount, int initialGemCount)
        {
            coinCount = initialCoinCount;
            gemCount = initialGemCount;
            eventService = GameService.Instance.GetEventService();
        }

        public void SetCoinCount(int count)
        {
            coinCount = count;
            UpdateData();
        }
        public void SetGemCount(int count)
        {
            gemCount = count;
            UpdateData();
        }

        public void UpdateData() => eventService.OnCurrencyUpdated.InvokeEvent();
        public int GetCoinCount() => coinCount;
        public int GetGemCount() => gemCount;

        public void AddCoins(int amount) => SetCoinCount(coinCount + amount);
        public void AddGems(int amount) => SetGemCount(gemCount + amount);
    }
}