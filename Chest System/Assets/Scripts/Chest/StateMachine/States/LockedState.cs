
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

        public LockedState(ChestStateMachine chestStateMachine, ChestController controller, GameService gameService)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = controller;

            eventService = gameService.GetEventService();
        }

        public void OnStateEntered()
        {
            ChestController.SetViewActive();
            ChestController.SetLockedUI(true);
            ChestController.UpdateChestUI(EChestState.LOCKED);
        }

        public void Update() { }
        public void OnStateExited() { }

        public int GetChestBuyingCost() => ChestController.GetDefaultBuyingCost();
        public void OnClick() => eventService.OnLockedChestClicked.InvokeEvent(ChestController);
    }
}

