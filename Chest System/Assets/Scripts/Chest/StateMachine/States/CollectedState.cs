using ChestSystem.UI;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class CollectedState : IState
    {
        public ChestController ChestController { get; set; }
        public ChestStateMachine chestStateMachine;
        public CollectedState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
        }
        public void OnClick()
        {
        }

        public void OnStateEntered()
        {
            ChestController.SetViewInactive();
            ChestController.EmptyCurrentSlot();
            UIService.Instance.GetChestService().ReturnChestToPool(ChestController);
        }

        public void OnStateExited()
        {
        }

        public void Update()
        {
        }
    }
}
