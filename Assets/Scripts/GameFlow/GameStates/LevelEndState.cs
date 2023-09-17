public class LevelEndState : GameState
{
    private readonly SaveDataHandler _dataHandler;
    private readonly LevelInfo _levelInfo;

    public LevelEndState(SaveDataHandler dataHandler, LevelInfo levelInfo)
    {
        _dataHandler = dataHandler;
        _levelInfo = levelInfo;
    }

    public override void Enter()
    {
        _dataHandler.DataInfo.TotalLevelPassed++;
        _dataHandler.DataInfo.CurrentCoinsInGame += _levelInfo.EarnedCoins;
        _dataHandler.DataInfo.TotalCoinsEarned += _levelInfo.EarnedCoins;
        _dataHandler.DataInfo.TotalEnemyKilled += _levelInfo.Enemies.Length;
        
        _dataHandler.SaveGame();
    }

    public override void Exit() { }
}