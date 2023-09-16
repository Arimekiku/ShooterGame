using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private MainMenuUIHandler UIHandler;

    private SaveDataHandler _dataHandler;

    private void Awake()
    {
        _dataHandler = FindObjectOfType<SaveDataHandler>();
        _dataHandler.LoadGame();
        
        MoneyHandler moneyHandler = new();
        UpgradeHandler upgradeHandler = new(moneyHandler);
        
        UIHandler.Init(upgradeHandler, moneyHandler, _dataHandler.DataInfo);
    }
}