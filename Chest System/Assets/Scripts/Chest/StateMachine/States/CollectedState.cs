using ChestSystem.Core;
using ChestSystem.Events;

namespace ChestSystem.Chest
{
    public class CollectedState : IState
    {
        public ChestStateMachine chestStateMachine;
        public ChestController ChestController { get; set; }

        private EventService eventService;

        public CollectedState(ChestStateMachine chestStateMachine, ChestController chestController, EventService eventService)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;

            this.eventService = eventService;
        }

        public void OnStateEntered() => eventService.OnRewardCollected.InvokeEvent(ChestController);
        public int GetChestBuyingCost() => 0;

        public void OnChestSelected() { }
        public void OnStateExited() { }
        public void Update() { }
    }
}
