using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelBootstrapper : MonoBehaviour
{
    [Header("Player Preferences")]
    [SerializeField] private PlayerBehaviour _playerPrefab;
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private CinemachineVirtualCamera _defaultCamera;
    private PlayerBehaviour _playerInstance;

    [Header("Level Prefabs")] 
    [SerializeField] private RoadBehaviour _defaultRoadPrefab;
    [SerializeField] private RoadBehaviour _bossRoadPrefab;
    [SerializeField] private PlayerBullet _bulletPrefab;
    [SerializeField] private EnemyBehaviour _enemyPrefab;

    [Header("Level Preferences")] 
    [SerializeField] private LevelBehaviour _levelBehaviour;

    [Header("Input Preferences")] 
    [SerializeField] private InputBehaviour _inputBehaviour;

    [Header("UI")] 
    [SerializeField] private LevelUIHandler _levelUIHandler;

    private DataProvider<GameInput> _inputProvider;
    private DataProvider<GameInstanceFactory> _factoryProvider;
    
    private void Awake()
    {
        InitInputProvider();
        InitFactoryProvider();
        InitPlayer();
        InitLevel();
        InitInputBehaviour();
        InitUIHandler();
    }

    private void InitInputProvider()
    {
        List<GameInput> inputs = new()
        {
            new PlayerInput(_inputBehaviour),
            new PauseInput(_inputBehaviour),
        };

        _inputProvider = new(inputs);
    }

    private void InitFactoryProvider()
    {
        List<GameInstanceFactory> factories = new()
        {
            new EnemyFactory(_levelBehaviour.transform, _enemyPrefab),
            new RoadFactory(_levelBehaviour.transform, _defaultRoadPrefab, _bossRoadPrefab),
            new PlayerBulletFactory(_levelBehaviour.transform, _bulletPrefab)
        };

        _factoryProvider = new(factories);
    }

    private void InitPlayer()
    {
        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        PlayerBulletFactory bulletFactory = _factoryProvider.GetObjectOfType<PlayerBulletFactory>();
        
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
        _playerInstance.Init(bulletFactory, playerInput);

        _defaultCamera.Follow = _playerInstance.transform;
        _defaultCamera.LookAt = _playerInstance.transform;
    }
    
    private void InitLevel()
    {
        LevelBuilder levelBuilder = new(_playerInstance, _factoryProvider);

        LevelInfo levelInfo = levelBuilder.BuildLevelInfo();
        
        _levelBehaviour.Init(levelInfo);
    }
    
    private void InitInputBehaviour()
    {
        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        
        _inputBehaviour.Init(playerInput, _inputProvider, _playerInstance);
    }
    
    private void InitUIHandler()
    {
        PlayerInput cachedPlayerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        PauseInput cachedPauseInput = _inputProvider.GetObjectOfType<PauseInput>();
        
        _levelUIHandler.Init(_playerInstance, _levelBehaviour, cachedPlayerInput, cachedPauseInput);
    }
}
