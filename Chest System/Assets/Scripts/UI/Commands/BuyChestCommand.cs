using ChestSystem.Events;
namespace ChestSystem.Chest
{
    public class BuyChestCommand : ICommand
    {
        private ChestController chestController;
        private EventService eventService;

        public BuyChestCommand(ChestController chestController, EventService eventService)
        {
            this.chestController = chestController;
            this.eventService = eventService;
        }

        public void Execute()
        {
            eventService.OnChestBought.InvokeEvent(chestController);
        }

        public void Undo() => eventService.OnUndo.InvokeEvent(chestController);
    }
}
