using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private MainMenuUIHandler UIHandler;

    private SaveDataHandler _dataHandler;
    private MoneyHandler _moneyHandler;
    private UpgradeHandler _upgradeHandler;

    private void Awake()
    {
        LoadSavedData();
        
        InitEconomy();
        InitUI();
    }

    private void LoadSavedData()
    {
        _dataHandler = FindObjectOfType<SaveDataHandler>();
        _dataHandler.LoadGame();
    }
    
    private void InitEconomy()
    {
        _moneyHandler = new();
        _upgradeHandler = new(_moneyHandler);
    }
    
    private void InitUI()
    {
        UIHandler.Init(_upgradeHandler, _moneyHandler, _dataHandler.DataInfo);
    }
}