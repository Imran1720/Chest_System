using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class CommandInvoker
    {
        private Stack<ICommand> undoCommandHistory;

        public CommandInvoker()
        {
            undoCommandHistory = new Stack<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            command.Execute();
            undoCommandHistory.Push(command);
        }

        public void UndoCommand()
        {
            if (undoCommandHistory.Count > 0)
            {
                ICommand command = undoCommandHistory.Pop();
                command.Undo();
            }
        }

        public void ClearHistory() => undoCommandHistory.Clear();
    }
}
