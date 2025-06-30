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
            ChestController.SetViewInactive();
            UIService.Instance.GetChestService().ReturnChestToPool(ChestController);
            UIService.Instance.GetPlayerService().RewardPlayer(ChestController.GetCoinsToBeRewarded(), ChestController.GetGemsToBeRewarded());
            UIService.Instance.UpdateCurrencies();
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
