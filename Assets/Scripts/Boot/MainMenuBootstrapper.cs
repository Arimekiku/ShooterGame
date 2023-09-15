using UnityEngine;

public class MainMenuBootstrapper : MonoBehaviour
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