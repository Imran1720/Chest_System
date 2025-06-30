using System.Collections.Generic;
using UnityEngine;

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

        public void Update()
        {
            currentState?.Update();
        }

        private void CreateStates()
        {
            chestStatesList.Add(EChestState.LOCKED, new LockedState(this, controller));
            chestStatesList.Add(EChestState.UNLOCKING, new UnlockingState(this, controller));
            chestStatesList.Add(EChestState.UNLOCKED, new UnlockedState(this, controller));
            chestStatesList.Add(EChestState.COLLECTED, new CollectedState(this, controller));
        }

        public void ChangeState(EChestState state)
        {
            currentState?.OnStateExited();
            currentState = GetState(state);
            currentState?.OnStateEntered();
        }

        public void ProcessOnClick() => currentState.OnClick();

        private IState GetState(EChestState state) => chestStatesList[state];

        public EChestState GetCurrentStateType()
        {
            foreach (var item in chestStatesList)
            {
                if (item.Value == currentState)
                    return item.Key;
            }
            return EChestState.LOCKED;
        }
        public int GetChestBuyingCost()
        {
            return currentState.GetChestBuyingCost();
        }
    }
}
