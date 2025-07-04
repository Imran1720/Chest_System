public class PlayerController
{
    private PlayerModel playerModel;

    public PlayerController(PlayerModel playerModel) => this.playerModel = playerModel;

    public void IncrementPlayerCoinsBy(int value)
    {
        int currentPlayerCoins = playerModel.GetCoinCount();
        playerModel.SetCoinCount(currentPlayerCoins + value);
    }

    public void IncrementPlayerGemsBy(int value)
    {
        int currentPlayerGems = playerModel.GetGemCount();
        playerModel.SetGemCount(currentPlayerGems + value);
    }

    public void DecrementPlayerCoinsBy(int value)
    {
        int currentPlayerCoins = playerModel.GetCoinCount();
        currentPlayerCoins -= value;
        currentPlayerCoins = (currentPlayerCoins <= 0) ? 0 : currentPlayerCoins;

        playerModel.SetCoinCount(currentPlayerCoins);
    }

    public void DecrementPlayerGemsBy(int value)
    {
        int currentPlayerGems = playerModel.GetGemCount();
        currentPlayerGems -= value;
        currentPlayerGems = (currentPlayerGems <= 0) ? 0 : currentPlayerGems;

        playerModel.SetGemCount(currentPlayerGems);
    }

    public bool HasSufficientCoins(int amount) => playerModel.GetCoinCount() >= amount;
    public bool HasSufficientGems(int amount) => playerModel.GetGemCount() >= amount;


    public int GetCoinCount() => playerModel.GetCoinCount();
    public int GetGemCount() => playerModel.GetGemCount();
}
