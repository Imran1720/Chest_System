namespace ChestSystem.Chest
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}
