using ChestSystem.Core;
using ChestSystem.Events;
using ChestSystem.UI;
using ChestSystem.UI.PopUp;
using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class UnlockingState : IState
    {
        public ChestController ChestController { get; set; }
        private ChestStateMachine chestStateMachine;

        GameService gameService;
        EventService eventService;

        private float timer;
        private int costUpdateThreshold = 10;
        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController, GameService gameService)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;

            this.gameService = gameService;
            eventService = gameService.GetEventService();
        }

        public void OnStateEntered()
        {
            gameService.SetIsChestUnlocking(true);
            ChestController.SetLockedUI(true);
            timer = ChestController.GetChestOpenDuration() * 60;
        }

        public void OnStateExited() => gameService.SetIsChestUnlocking(false);

        public void Update()
        {
            timer -= Time.deltaTime;
            if (CanUpdateCost())
            {
                ChestController.UpdateCost(timer);
            }
            ChestController.UpdateTime(timer);

            if (timer <= 0)
            {
                chestStateMachine.ChangeState(EChestState.UNLOCKED);
            }
        }

        private bool CanUpdateCost()
        {
            int updateVector = costUpdateThreshold * 60;

            return (((int)timer % updateVector) == 0);
        }

        public void OnClick() => eventService.OnUnlockingChestClicked.InvokeEvent(ChestController);

        public int GetChestBuyingCost() => ChestController.CalculateChestBuyingCost(timer);
    }
}
