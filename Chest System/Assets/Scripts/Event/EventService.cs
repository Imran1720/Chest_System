using ChestSystem.Chest;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController<ChestView> OnChestSelected { get; private set; }
        public EventController OnInsufficientFunds { get; private set; }
        public EventController OnCurrencyUpdated { get; private set; }
        public EventController<ChestController> OnChestBought { get; private set; }
        public EventController<ChestController> OnUndo { get; private set; }
        public EventController<ChestController> OnRewardCollected { get; private set; }


        public EventService()
        {
            OnChestSelected = new EventController<ChestView>();
            OnInsufficientFunds = new EventController();
            OnCurrencyUpdated = new EventController();
            OnChestBought = new EventController<ChestController>();
            OnUndo = new EventController<ChestController>();
            OnRewardCollected = new EventController<ChestController>();
        }
    }
}
