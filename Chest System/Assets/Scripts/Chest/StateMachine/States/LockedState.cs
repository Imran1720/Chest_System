
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
            if (ChestController.CanUnlockChest())
            {
                ChestController.ShowUnlockPopUP();
            }
            else
            {
                ChestController.ShowChestOpeningPopUP();
            }
        }

        public int GetChestBuyingCost()
        {
            return ChestController.GetDefaultBuyingCost();
        }
    }
}

