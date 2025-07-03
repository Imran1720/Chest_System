using ChestSystem.Core;
using ChestSystem.Events;

namespace ChestSystem.Chest
{
    public class CollectedState : IState
    {
        public ChestStateMachine chestStateMachine;
        public ChestController ChestController { get; set; }
        private EventService eventService;

        public CollectedState(ChestStateMachine chestStateMachine, ChestController chestController, GameService gameService)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;

            eventService = gameService.GetEventService();
        }

        public int GetChestBuyingCost() => 0;
        public void OnStateEntered() => eventService.OnRewardCollected.InvokeEvent(ChestController);

        public void Update() { }
        public void OnChestSelected() { }
        public void OnStateExited() { }
    }
}
