using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelEntryPoint : MonoBehaviour
{
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
    
    private void Awake()
    {
        SaveLoadedData();

        InitInputProvider();
        InitFactoryProvider();
        InitPlayer();
        InitLevel();
        InitCameraSystem();
        InitInputBehaviour();
        InitUIHandler();
    }

    private void SaveLoadedData()
    {
        _dataHandler = FindObjectOfType<SaveDataHandler>();
        _dataHandler.SaveGame();
    }

    private void InitInputProvider()
    {
        List<GameInput> inputs = new()
        {
            new PlayerInput(InputBehaviour),
            new PauseInput(InputBehaviour),
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
            new PlayerBulletFactory(LevelBehaviour.transform),
            new PlayerFactory(PlayerContainer, playerInput)
        };

        _factoryProvider = new(factories);
    }

    private void InitPlayer()
    {
        PlayerFactory playerFactory = _factoryProvider.GetObjectOfType<PlayerFactory>();
        _playerInstance = playerFactory.CreateInstance();
        
        PlayerBulletFactory bulletFactory = _factoryProvider.GetObjectOfType<PlayerBulletFactory>();
        PlayerWeaponInfo weaponInfo = new(_dataHandler.DataInfo.AttackSpeed, _dataHandler.DataInfo.AttackDamage);
        _playerInstance.Weapon.Init(bulletFactory, weaponInfo);
    }
    
    private void InitLevel()
    {
        LevelBuilder levelBuilder = new(_playerInstance, _factoryProvider, _dataHandler.DataInfo.RoadCount, _dataHandler.DataInfo.EnemyCount);

        LevelInfo levelInfo = levelBuilder.BuildLevelInfo();
        
        LevelBehaviour.Init(levelInfo, _dataHandler);
    }
    
    private void InitCameraSystem()
    {
        DefaultCamera.Follow = _playerInstance.transform;
        DefaultCamera.LookAt = _playerInstance.transform;
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
