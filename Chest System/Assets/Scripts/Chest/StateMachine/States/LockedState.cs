
using ChestSystem.UI;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class LockedState : IState
    {
        public ChestController ChestController { get; set; }
        private ChestStateMachine chestStateMachine;

        public LockedState(ChestStateMachine chestStateMachine, ChestController controller)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = controller;
        }
        public void OnStateEntered()
        {
            ChestController.SetViewActive();
            ChestController.SetLockedUI(true);
            ChestController.UpdateChestUI(EChestState.LOCKED);
        }

        public void OnStateExited()
        {
        }

        public void Update()
        {

        }

        public void OnClick()
        {
            // pop up to start timer or buy
            if (UIService.Instance.GetChestService().CanUnlockChest())
            {
                UIService.Instance.GetPopUpService().ShowUnlockPopUP(ChestController);
            }
            else
            {
                UIService.Instance.GetPopUpService().ShowChestOpeningPopUP();
            }
        }

        public int GetChestBuyingCost()
        {
            return ChestController.GetDefaultBuyingCost();
        }
    }
}

