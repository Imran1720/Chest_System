using ChestSystem.UI;
using ChestSystem.UI.PopUp;
using ChestSystem.UI.Slot;

namespace ChestSystem.Chest
{
    public class BuyChestCommand : ICommand
    {
        private ChestController chestController;
        private PopUpService popUpService;
        int requiredGems;

        public BuyChestCommand(ChestController chestController, PopUpService popUpService)
        {
            this.chestController = chestController;
            this.popUpService = popUpService;
        }

        public void Execute()
        {
            requiredGems = chestController.GetChestBuyingCost();
            if (UIService.Instance.GetPlayerService().HasSufficientGemss(requiredGems))
            {
                chestController.OpenWithGems();
                UIService.Instance.GetPlayerService().DecrementGemsBy(requiredGems);
                UIService.Instance.UpdateCurrencies();
                popUpService.ClosePopUp();
            }
            else
            {
                popUpService.ClosePopUp();
                popUpService.ShowInsufficientFundPopUP();
            }
        }

        public void Undo()
        {
            chestController.Reset();
            UIService.Instance.GetPlayerService().IncrementGemsBy(requiredGems);
        }
    }
}
