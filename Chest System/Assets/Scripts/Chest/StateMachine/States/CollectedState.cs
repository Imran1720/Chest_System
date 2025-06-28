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
        }

        public void OnStateExited()
        {
        }

        public void Update()
        {
        }
    }
}
