public abstract class GameState : IGameState, IData
{
    protected GameState()
    {
        
    }

    public abstract void Enter();

    public abstract void Exit();
}