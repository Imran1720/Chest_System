using ChestSystem.Core;
using ChestSystem.Player;
using ChestSystem.UI;
using ChestSystem.UI.PopUp;
using ChestSystem.UI.Slot;

namespace ChestSystem.Chest
{
    public class BuyChestCommand : ICommand
    {
        private ChestController chestController;

        private PopUpService popUpService;
        private PlayerService playerService;
        private UIService uiService;

        int requiredGems;

        public BuyChestCommand(ChestController chestController, GameService gameService)
        {
            this.chestController = chestController;
            InitializeServices(GameService.Instance);
        }

        private void InitializeServices(GameService gameService)
        {
            popUpService = gameService.GetPopUpService();
            playerService = gameService.GetPlayerService();
            uiService = gameService.GetUIService();
        }

        public void Execute()
        {
            requiredGems = chestController.GetChestBuyingCost();
            if (playerService.HasSufficientGemss(requiredGems))
            {
                chestController.OpenWithGems();
                playerService.DecrementGemsBy(requiredGems);
                uiService.UpdateCurrencies();
                popUpService.ClosePopUp();
            }
            else
            {
                popUpService.ShowInsufficientFundPopUP();
            }
        }

        public void Undo()
        {
            chestController.Reset();
            playerService.IncrementGemsBy(requiredGems);
        }
    }
}
