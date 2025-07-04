using ChestSystem.Events;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class CommandInvoker
    {
        private Stack<ICommand> undoCommandHistory;
        private EventService eventService;

        public CommandInvoker(EventService eventService)
        {
            this.eventService = eventService;
            undoCommandHistory = new Stack<ICommand>();

            AddEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.OnUndoClicked.AddListener(UndoCommand);
            eventService.OnRewardProcessing.AddListener(ClearHistory);
        }

        public void RemoveEventListeners()
        {
            eventService.OnUndoClicked.RemoveListener(UndoCommand);
            eventService.OnRewardProcessing.RemoveListener(ClearHistory);
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
