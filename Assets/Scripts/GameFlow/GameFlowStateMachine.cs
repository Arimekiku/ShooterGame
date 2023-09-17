using System.Collections.Generic;

public class GameFlowStateMachine : IGameStateSwitcher
{
    private readonly List<GameState> _states;
    private GameState _currentState;
    
    public GameFlowStateMachine(List<GameState> states)
    {
        _states = states;
    }
    
    public void SwitchState<T>() where T : GameState
    {
        GameState seekingState = _states.Find(s => s is T);
        _currentState?.Exit();

        _currentState = seekingState ?? throw new($"Invalid state requested ${typeof(T)}");
        _currentState?.Enter();
    }
}