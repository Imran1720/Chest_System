namespace ChestSystem.Chest
{
    public interface IState
    {
        public ChestController ChestController { get; set; }
        public void OnStateEntered();
        public void Update();
        public void OnStateExited();

    }
}
