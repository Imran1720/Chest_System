
using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class LockedState : IState
    {
        public ChestController ChestController { get; set; }
        private ChestStateMachine chestStateMachine;

        private EventService eventService;

        public LockedState(ChestStateMachine chestStateMachine, ChestController controller, EventService eventService)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = controller;

            this.eventService = eventService;
        }

        public void OnStateEntered()
        {
            ChestController.SetViewActive();
            ChestController.SetLockedUI(true);
            ChestController.UpdateChestUI(EChestState.LOCKED);
        }
        public void OnChestSelected() => eventService.OnLockedChestClicked.InvokeEvent(ChestController);
        public int GetChestBuyingCost() => ChestController.GetDefaultBuyingCost();

        public void OnStateExited() { }
        public void Update() { }
    }
}

