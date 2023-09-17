using System.Collections.Generic;
using UnityEngine;

public class GameFlowHandler : MonoBehaviour
{
    public GameFlowStateMachine StateMachine { get; private set; }
    
    private SaveDataHandler _dataHandler;

    public void Init(List<GameState> states, SaveDataHandler dataHandler)
    {
        StateMachine = new(states);

        _dataHandler = dataHandler;
    }
    
    private void OnApplicationQuit()
    {
        _dataHandler.SaveGame();
    }

    private void OnDestroy()
    {
        _dataHandler.SaveGame();
    }
}