using ChestSystem.Events;

namespace ChestSystem.Player
{
    public class PlayerModel
    {
        private int coinCount;
        private int gemCount;

        private EventService eventService;

        public PlayerModel(int initialCoinCount, int initialGemCount, EventService eventService)
        {
            coinCount = initialCoinCount;
            gemCount = initialGemCount;
            this.eventService = eventService;
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

        public int GetGemCount() => gemCount;
        public int GetCoinCount() => coinCount;

        public void AddGems(int amount) => SetGemCount(gemCount + amount);
        public void AddCoins(int amount) => SetCoinCount(coinCount + amount);

        private void UpdateData() => eventService.OnCurrencyUpdated.InvokeEvent();
    }
}