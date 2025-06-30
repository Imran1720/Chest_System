
using ChestSystem.Chest;
using ChestSystem.Player;
using ChestSystem.UI;
using ChestSystem.UI.PopUp;
using ChestSystem.UI.Slot;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ChestSystem.Core
{
    public class GameService : MonoBehaviour
    {
        [SerializeField] private UIService UIService;
        private PlayerService playerService;
        private ChestService chestService;

        private CommandInvoker commandInvoker;

        [Header("CHEST-DATA")]
        [SerializeField] ChestSO chestSO;
        [SerializeField] ChestView chestPrefab;
        private void Start()
        {
            InitializeSevices();
            commandInvoker = new CommandInvoker();
            GetPopUpService().SetCommandInvoker(commandInvoker);
        }

        private void InitializeSevices()
        {

            playerService = new PlayerService(3, 3);
            chestService = new ChestService(chestSO, chestPrefab);
        }


        private void Update()
        {
            chestService.Update();
        }

        public void ClearCommandHistory() => commandInvoker.ClearHistory();

        public void ClearCommandHistory() => commandInvoker.ClearHistory();

        public ChestService GetChestService() => chestService;
        public PopUpService GetPopUpService() => UIService.GetPopUpService();
        public PlayerService GetPlayerService() => playerService;
        public SlotService GetSlotService() => UIService.GetSlotService();
        public UIService GetUIService() => UIService;
    }
}
