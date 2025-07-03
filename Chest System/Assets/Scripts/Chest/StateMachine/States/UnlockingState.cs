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

        EventService eventService;

        private float timer;
        private int costUpdateIntervalInMinutes = 10;

        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController, EventService eventService)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;

            this.eventService = eventService;
        }

        public void OnStateEntered()
        {
            ChestController.SetLockedUI(true);
            timer = ConvertTimeIntoSeconds();
        }

        private int ConvertTimeIntoSeconds()
        {
            return ChestController.GetChestOpenDuration();
        }

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
            int updateIntervalInSeconds = costUpdateIntervalInMinutes * ChestController.GetSecondsPerMinute();
            return (((int)timer % updateIntervalInSeconds) == 0);
        }

        public void OnChestSelected() => eventService.OnUnlockingChestClicked.InvokeEvent(ChestController);
        public int GetChestBuyingCost() => ChestController.CalculateChestBuyingCost(timer);
        public void OnStateExited() { }
    }
}
