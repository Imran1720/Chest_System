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

        private ChestService chestService;
        private PopUpService popUpService;

        private float timer;
        private int costUpdateThreshold = 10;
        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;

            chestService = GameService.Instance.GetChestService();
            popUpService = GameService.Instance.GetPopUpService();
        }

        public void OnStateEntered()
        {
            chestService.SetUnlockingChest(false);
            ChestController.SetLockedUI(true);
            timer = ChestController.GetChestOpenDuration() * 60;
        }

        public void OnStateExited()
        {
            chestService.SetUnlockingChest(true);
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
            popUpService.ShowBuyPopUP(ChestController);
        }

        public int GetChestBuyingCost()
        {
            return ChestController.CalculateChestBuyingCost(timer);
        }
    }
}
