using ChestSystem.Chest;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController<ChestView> OnChestSelected { get; private set; }
        public EventController OnInsufficientFunds { get; private set; }
        public EventController OnCurrencyUpdated { get; private set; }
        public EventController<ChestController> OnChestBought { get; private set; }
        public EventController<ChestController> OnProcessingUndo { get; private set; }
        public EventController OnUndoClicked { get; private set; }
        public EventController<ChestController> OnRewardCollected { get; private set; }
        public EventController<ChestController> OnLockedChestSelected { get; private set; }
        public EventController OnProcessingReward { get; private set; }
        public EventController<ChestController> OnLockedChestClicked { get; private set; }
        public EventController<ChestController> OnUnlockingChestClicked { get; private set; }

        public EventService()
        {
            OnChestSelected = new EventController<ChestView>();
            OnInsufficientFunds = new EventController();
            OnCurrencyUpdated = new EventController();
            OnChestBought = new EventController<ChestController>();
            OnProcessingUndo = new EventController<ChestController>();
            OnUndoClicked = new EventController();
            OnRewardCollected = new EventController<ChestController>();
            OnProcessingReward = new EventController();
            OnLockedChestClicked = new EventController<ChestController>();
            OnUnlockingChestClicked = new EventController<ChestController>();
        }
    }
}
