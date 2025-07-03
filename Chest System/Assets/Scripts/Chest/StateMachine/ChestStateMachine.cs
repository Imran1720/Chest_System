using ChestSystem.Core;
using ChestSystem.Events;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class ChestStateMachine
    {
        private ChestController controller;
        private Dictionary<EChestState, IState> stateDictionary = new Dictionary<EChestState, IState>();

        private IState currentState;

        public ChestStateMachine(ChestController controller, EventService eventService)
        {
            this.controller = controller;
            CreateStates(eventService);
        }

        public void Update() => currentState?.Update();

        private void CreateStates(EventService eventService)
        {
            stateDictionary.Add(EChestState.UNLOCKED, new UnlockedState(this, controller));
            stateDictionary.Add(EChestState.LOCKED, new LockedState(this, controller, eventService));
            stateDictionary.Add(EChestState.UNLOCKING, new UnlockingState(this, controller, eventService));
            stateDictionary.Add(EChestState.COLLECTED, new CollectedState(this, controller, eventService));
        }

        public void ChangeState(EChestState state)
        {
            currentState?.OnStateExited();
            currentState = GetState(state);
            currentState?.OnStateEntered();
        }

        public EChestState GetCurrentStateType()
        {
            foreach (var item in stateDictionary)
            {
                if (item.Value == currentState)
                    return item.Key;
            }
            return EChestState.LOCKED;
        }

        public void ProcessOnClick() => currentState.OnChestSelected();

        public int GetChestBuyingCost() => currentState.GetChestBuyingCost();

        private IState GetState(EChestState state) => stateDictionary[state];
    }
}
