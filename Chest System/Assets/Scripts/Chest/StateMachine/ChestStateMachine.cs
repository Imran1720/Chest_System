using System;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class ChestStateMachine
    {
        private ChestController controller;

        private Dictionary<EChestState, IState> chestStatesList = new Dictionary<EChestState, IState>();
        public IState currentState;

        public ChestStateMachine(ChestController controller)
        {
            this.controller = controller;
            CreateStates();
        }

        public void Update() => currentState?.Update();

        private void CreateStates()
        {
            chestStatesList.Add(EChestState.LOCKED, new LockedState(this, controller));
            chestStatesList.Add(EChestState.UNLOCKING, new UnlockingState(this, controller));
        }

        public void ChangeState(EChestState state)
        {
            currentState?.OnStateExited();
            currentState = GetState(state);
            currentState?.OnStateEntered();
        }

        private IState GetState(EChestState state) => chestStatesList[state];
    }
}
