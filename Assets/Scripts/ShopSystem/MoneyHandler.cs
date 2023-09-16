using System;

public class MoneyHandler
{
    public event Action<int> OnCoinsUpdate;
    
    public void UpdateCoins(int totalCoins)
    {
        OnCoinsUpdate.Invoke(totalCoins);
    }

    public bool TryDepleteCoins(ref int totalCoins, int depleteCount)
    {
        if (totalCoins < depleteCount)
            return false;

        totalCoins -= depleteCount;
        UpdateCoins(totalCoins);
        
        return true;
    }
}