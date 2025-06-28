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
            ChestController.RefreshChestUI(EChestState.LOCKED, ChestController.GetChestOpenDuration());
        }

        public void OnStateExited()
        {
        }

        public void Update()
        {

        }
    }
}
