using ChestSystem.Core;
using ChestSystem.UI;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class CollectedState : IState
    {
        public ChestController ChestController { get; set; }
        public ChestStateMachine chestStateMachine;
        public CollectedState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
        }
        public void OnClick()
        {
        }

        public void OnStateEntered()
        {
            GameService.Instance.GetEventService().OnRewardCollected.InvokeEvent(ChestController);
        }

        public void OnStateExited()
        {
        }

        public void Update()
        {
        }
        public int GetChestBuyingCost()
        {
            return 0;
        }
    }
}
