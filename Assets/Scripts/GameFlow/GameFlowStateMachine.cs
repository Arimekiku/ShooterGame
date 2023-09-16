using System.Collections.Generic;
using UnityEngine;

public class GameFlowStateMachine : MonoBehaviour, IGameStateSwitcher
{
    private readonly DataProvider<GameState> _gameStates;
    private IGameState _currentState;

    public GameFlowStateMachine()
    {
        MainMenuState mainMenuState = new();
        LevelPerformState levelPerformState = new();
        LevelEndState levelEndState = new();

        List<GameState> gameStates = new()
        {
            mainMenuState,
            levelPerformState,
            levelEndState
        };
        _gameStates = new(gameStates);
        
        _currentState = mainMenuState;
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : GameState
    {
        _currentState.Exit();
        _currentState = _gameStates.GetObjectOfType<T>();
        _currentState.Enter();
    }
}