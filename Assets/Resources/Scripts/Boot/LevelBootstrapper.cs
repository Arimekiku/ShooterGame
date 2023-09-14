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
    [SerializeField] private Transform _bulletContainer;

    [Header("Input Preferences")] 
    [SerializeField] private InputBehaviour _inputBehaviour;

    [Header("UI")] 
    [SerializeField] private LevelUIHandler _levelUIHandler;

    private InstancesProvider<GameInput> _inputProvider;
    private InstancesProvider<GameInstanceFactory> _factoryProvider;

    private readonly List<EnemyBehaviour> _enemies = new();
    private readonly List<RoadBehaviour> _roads = new();
    
    private int _enemyCount;
    private int _roadSegmentCount;
    
    private void Awake()
    {
        InitInputProvider();
        InitFactoryProvider();
        InitPlayer();
        InitInputBehaviour();
        InitRoad();
        InitEnemies();
        InitLevel();
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
            new EnemyFactory(_enemyPrefab, _levelBehaviour.transform),
            new RoadFactory(_defaultRoadPrefab, _bossRoadPrefab, _enemyCount, _levelBehaviour.transform),
            new PlayerBulletFactory(_bulletPrefab, _bulletContainer)
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
    
    private void InitInputBehaviour()
    {
        PlayerInput playerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        
        _inputBehaviour.Init(playerInput, _inputProvider, _playerInstance);
    }

    private void InitRoad()
    {
        RoadFactory roadFactory = _factoryProvider.GetObjectOfType<RoadFactory>();
        
        _roadSegmentCount = PlayerPrefs.GetInt(SaveKeyTemplates.RoadCountKey);
        _enemyCount = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyCountKey);
        
        for (int i = 0; i < _roadSegmentCount; i++)
        {
            RoadBehaviour roadInstance = roadFactory.CreateInstance();
            _roads.Add(roadInstance);
        }
        
        BossRoadBehaviour bossRoadInstance = roadFactory.CreateInstance() as BossRoadBehaviour;
        _roads.Add(bossRoadInstance);
        _enemies.Add(bossRoadInstance.Boss);
    }
    
    private void InitEnemies()
    {
        EnemyFactory enemyFactory = _factoryProvider.GetObjectOfType<EnemyFactory>();

        for (int i = 0; i < _enemyCount; i++)
        {
            EnemyBehaviour enemyInstance = enemyFactory.CreateInstance();
            _enemies.Add(enemyInstance);
        }
    }
    
    private void InitLevel()
    {
        LevelBuilder levelBuilder = new(_playerInstance, _roads, _enemies);

        LevelInfo levelInfo = levelBuilder.BuildLevelInfo();
        
        _levelBehaviour.Init(levelInfo);
    }
    
    private void InitUIHandler()
    {
        PlayerInput cachedPlayerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        PauseInput cachedPauseInput = _inputProvider.GetObjectOfType<PauseInput>();
        
        _levelUIHandler.Init(_playerInstance, _levelBehaviour, cachedPlayerInput, cachedPauseInput);
    }
}
