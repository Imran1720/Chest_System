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

        public void OnStateEntered()
        {
            ChestController.SetOpenedChestBG();
            ChestController.SetLockedUI(false);
        }

        public void OnChestSelected() => chestStateMachine.ChangeState(EChestState.COLLECTED);
        public int GetChestBuyingCost() => 0;

        public void OnStateExited() { }
        public void Update() { }
    }
}
