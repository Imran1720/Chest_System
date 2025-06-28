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
        private int costUpdateThreshold;
        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
        }

        public void OnStateEntered()
        {
            UIService.Instance.GetChestService().SetUnlockingChest(false);
            ChestController.SetLockedUI(true);
            ChestController.SetChestUnlockingUI();
            timer = ChestController.GetChestOpenDuration() * 60;
            costUpdateThreshold = 10;
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
        }

        private bool CanUpdateCost()
        {
            int updateVector = costUpdateThreshold * 60;

            return (((int)timer % updateVector) == 0);
        }

        public void OnClick()
        {
            // pop up to buy with gems
            Debug.Log("Changing to Unlocked State");
            chestStateMachine.ChangeState(EChestState.UNLOCKED);
        }
    }
}
