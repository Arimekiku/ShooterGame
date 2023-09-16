public class UpgradeHandler
{
    public const int AttackDamageUpgradePrice = 10;
    public const int AttackSpeedUpgradePrice = 15;
    
    private const int AttackDamageUpgradeAmount = 1;
    private const float AttackSpeedUpgradeAmount = 0.1f;
    
    private readonly MoneyHandler _moneyHandler;
    
    public UpgradeHandler(MoneyHandler newMoneyHandler)
    {
        _moneyHandler = newMoneyHandler;
    }

    public void MakeAttackDamageUpgrade(ref int attackDamage, ref int totalCoins)
    {
        MakeUpgrade(ref attackDamage, AttackDamageUpgradePrice, AttackDamageUpgradeAmount, ref totalCoins);
    }

    public void MakeAttackSpeedUpgrade(ref float attackSpeed, ref int totalCoins)
    {
        MakeUpgrade(ref attackSpeed, AttackSpeedUpgradePrice, AttackSpeedUpgradeAmount, ref totalCoins);
    }

    private void MakeUpgrade(ref float upgradeStat, int price, float amount, ref int totalCoins)
    {
        if (_moneyHandler.TryDepleteCoins(ref totalCoins, price))
            upgradeStat += amount;
    }
    
    private void MakeUpgrade(ref int upgradeStat, int price, int amount, ref int totalCoins)
    {
        if (_moneyHandler.TryDepleteCoins(ref totalCoins, price))
            upgradeStat += amount;
    }
}