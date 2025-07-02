using ChestSystem.Core;
using ChestSystem.UI;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class UnlockedState : IState
    {
        public ChestController ChestController { get; set; }
        private ChestStateMachine chestStateMachine;
        public UnlockedState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
        }

        public void OnClick()
        {
            chestStateMachine.ChangeState(EChestState.COLLECTED);
        }

        public void OnStateEntered()
        {
            ChestController.SetOpenedChestBG();
            ChestController.SetLockedUI(false);
        }

        public void OnStateExited() { }

        public void Update()
        {
        }
        public int GetChestBuyingCost()
        {
            return 0;
        }
    }
}
