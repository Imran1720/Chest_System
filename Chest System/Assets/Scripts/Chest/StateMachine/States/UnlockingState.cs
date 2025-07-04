using ChestSystem.Core;
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

        ChestService chestService;

        private float timer;
        private int costUpdateThreshold = 10;
        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
            chestService = GameService.Instance.GetChestService();
        }

        public void OnStateEntered()
        {
            chestService.SetIsChestUnlocking(true);
            ChestController.SetLockedUI(true);
            timer = ChestController.GetChestOpenDuration() * 60;
        }

        public void OnStateExited()
        {
            chestService.SetIsChestUnlocking(false);
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
            int updateVector = costUpdateThreshold * 60;

            return (((int)timer % updateVector) == 0);
        }

        public void OnClick()
        {
            ChestController.ShowBuyPopUP();
        }

        public int GetChestBuyingCost()
        {
            return ChestController.CalculateChestBuyingCost(timer);
        }
    }
}
