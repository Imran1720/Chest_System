namespace ChestSystem.Chest
{
    public interface IState : IBuyable, IClickable
    {
        public ChestController ChestController { get; set; }
        public void OnStateEntered();
        public void Update();
        public void OnStateExited();
    }
}
