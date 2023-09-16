using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private MainMenuUIHandler _uiHandler;

    private void Awake()
    {
        MoneyHandler moneyHandler = new();
        UpgradeHandler upgradeHandler = new(moneyHandler);
        LevelBalanceHandler balanceHandler = new();
        
        _uiHandler.Init(upgradeHandler, moneyHandler, balanceHandler);
    }
}