using ChestSystem.Chest;

namespace ChestSystem.Events
{
    public class EventService
    {
        // UI Events
        public EventController OnUndoClicked { get; private set; }
        public EventController OnCurrencyUpdated { get; private set; }
        public EventController OnInsufficientFunds { get; private set; }

        // Reward Processing Event
        public EventController OnRewardProcessing { get; private set; }

        // View Controller Linked Event
        public EventController<ChestView> OnChestSelected { get; private set; }

        // Chest State based OnClick Events
        public EventController<ChestController> OnChestBought { get; private set; }
        public EventController<ChestController> OnProcessingUndo { get; private set; }
        public EventController<ChestController> OnRewardCollected { get; private set; }
        public EventController<ChestController> OnLockedChestClicked { get; private set; }
        public EventController<ChestController> OnUnlockingChestClicked { get; private set; }

        public EventService()
        {
            OnUndoClicked = new EventController();
            OnRewardProcessing = new EventController();
            OnCurrencyUpdated = new EventController();
            OnInsufficientFunds = new EventController();

            OnChestSelected = new EventController<ChestView>();

            OnChestBought = new EventController<ChestController>();
            OnProcessingUndo = new EventController<ChestController>();
            OnRewardCollected = new EventController<ChestController>();
            OnLockedChestClicked = new EventController<ChestController>();
            OnUnlockingChestClicked = new EventController<ChestController>();
        }
    }
}
