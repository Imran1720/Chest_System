using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class UnlockingState : IState
    {
        public ChestController ChestController { get; set; }
        private ChestStateMachine chestStateMachine;

        private float timer;
        public UnlockingState(ChestStateMachine chestStateMachine, ChestController chestController)
        {
            this.chestStateMachine = chestStateMachine;
            ChestController = chestController;
        }

        public void OnStateEntered()
        {
            ChestController.SetChestUnlockingUI();
            timer = ChestController.GetChestOpenDuration() * 60;
        }

        public void OnStateExited()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            Debug.Log((int)timer);
            UpdateTime();
            if ((int)timer % 60 == 0)
            {
                UpdateCost();
            }
        }

        private void UpdateCost()
        {
            //int timeInMinute = ((int)timer / 60);
            //float timeforCalculation = (float)timeInMinute / 10;
            //int gems = (int)Mathf.Ceil(timeforCalculation);
            ChestController.SetChestGemPrice((int)(timer / 60));
        }

        private void UpdateTime()
        {

        }
    }
}
