using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelEntryPoint : MonoBehaviour
{
    [Header("Game Flow")]
    [SerializeField] private GameFlowHandler GameFlow;
    
    [Header("Player Preferences")]
    [SerializeField] private Transform PlayerContainer;
    
    [Header("Camera Preferences")]
    [SerializeField] private CinemachineVirtualCamera DefaultCamera;

    [Header("Level Preferences")] 
    [SerializeField] private LevelBehaviour LevelBehaviour;

    [Header("Input Preferences")] 
    [SerializeField] private InputBehaviour InputBehaviour;

    [Header("UI")] 
    [SerializeField] private LevelUIHandler LevelUIHandler;
    
    private DataProvider<GameInput> _inputProvider;
    private DataProvider<GameFactory> _factoryProvider;
    private PlayerBehaviour _playerInstance;
    private SaveDataHandler _dataHandler;
    private LevelInfo _levelInfo;
    
    private void Awake()
    {
        FindDataHandler();
        
        InitInputProvider();
        InitFactoryProvider();
        InitPlayer();
        InitLevel();
        InitCameraSystem();
        InitGameFlow();
        InitInputBehaviour();
        InitUIHandler();
    }

    private void FindDataHandler()
    {
        _dataHandler = FindObjectOfType<SaveDataHandler>();
    }

    private void InitInputProvider()
    {
        List<GameInput> inputs = new()
        {
            new PauseInput(InputBehaviour),
            new PlayerInput(InputBehaviour)
        };

        _inputProvider = new(inputs);
    }

    private void InitFactoryProvider()
    {
        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        
        List<GameFactory> factories = new()
        {
            new EnemyFactory(LevelBehaviour.transform),
            new RoadFactory(LevelBehaviour.transform),
            new PlayerBulletFactory(LevelBehaviour.transform, _dataHandler.DataInfo.AttackDamage),
            new PlayerFactory(PlayerContainer, playerInput)
        };

        _factoryProvider = new(factories);
    }

    private void InitPlayer()
    {
        PlayerFactory playerFactory = _factoryProvider.GetObjectOfType<PlayerFactory>();
        _playerInstance = playerFactory.CreateInstance();
        
        PlayerBulletFactory bulletFactory = _factoryProvider.GetObjectOfType<PlayerBulletFactory>();
        _playerInstance.Weapon.Init(bulletFactory, _dataHandler.DataInfo.AttackSpeed);
    }
    
    private void InitLevel()
    {
        LevelBuilder levelBuilder = new(_playerInstance, _factoryProvider, _dataHandler.DataInfo.RoadCount, _dataHandler.DataInfo.EnemyCount);

        _levelInfo = levelBuilder.BuildLevelInfo();
        
        LevelBehaviour.Init(_levelInfo, _dataHandler);
        LevelBehaviour.OnLevelEnd += _playerInstance.PlayerAnimator.TriggerDance;
    }
    
    private void InitCameraSystem()
    {
        DefaultCamera.Follow = _playerInstance.transform;
        DefaultCamera.LookAt = _playerInstance.transform;
    }
    
    private void InitGameFlow()
    {
        List<GameState> gameStates = new()
        {
            new LevelPerformState(_playerInstance),
            new LevelEndState(_dataHandler, _levelInfo),
            new LevelPauseState()
        };

        GameFlow.Init(gameStates, _dataHandler);
        GameFlow.StateMachine.SwitchState<LevelPerformState>();
        
        PauseInput pauseInput = _inputProvider.GetObjectOfType<PauseInput>();
        pauseInput.OnEscapePressed += GameFlow.StateMachine.SwitchState<LevelPerformState>;

        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        playerInput.OnEscapePressed += GameFlow.StateMachine.SwitchState<LevelPauseState>;
        
        LevelBehaviour.OnLevelEnd += GameFlow.StateMachine.SwitchState<LevelEndState>;
    }
    
    private void InitInputBehaviour()
    {
        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        
        InputBehaviour.Init(playerInput, _inputProvider);
    }
    
    private void InitUIHandler()
    {
        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        PauseInput pauseInput = _inputProvider.GetObjectOfType<PauseInput>();
        
        LevelUIHandler.Init(_playerInstance, LevelBehaviour, playerInput, pauseInput);
    }
}
