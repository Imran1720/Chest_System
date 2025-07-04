using ChestSystem.UI;
using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class UnlockingState : IState
    {
        public ChestController ChestController { get; set; }
        private ChestStateMachine chestStateMachine;

        private float timer;
        private int costUpdateThreshold = 10;
        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
        }

        public void OnStateEntered()
        {
            UIService.Instance.GetChestService().SetUnlockingChest(false);
            ChestController.SetLockedUI(true);
            timer = ChestController.GetChestOpenDuration() * 60;
        }

        public void OnStateExited()
        {
            // throw new System.NotImplementedException();
            UIService.Instance.GetChestService().SetUnlockingChest(true);
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
            // pop up to buy with gems
            UIService.Instance.GetPopUpService().ShowBuyPopUP(ChestController);
        }

        public int GetChestBuyingCost()
        {
            return ChestController.CalculateChestBuyingCost(timer);
        }
    }
}
