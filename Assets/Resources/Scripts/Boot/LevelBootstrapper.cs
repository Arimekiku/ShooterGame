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
    [SerializeField] private LevelHandler _levelHandler;

    [Header("Input Preferences")] 
    [SerializeField] private InputBehaviour _inputBehaviour;
    private InstancesProvider<GameInput> _inputProvider;

    [Header("UI")] 
    [SerializeField] private LevelUIHandler _levelUIHandler;

    private InstancesProvider<GameInstanceFactory> _factoryProvider;

    private BossBehaviour _bossInstance;
    private readonly List<EnemyBehaviour> _enemies = new();
    private readonly List<RoadBehaviour> _roads = new();
    
    private float _rangeBetweenEnemies;
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
        InitLevelHandler();
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
            new EnemyFactory(_enemyPrefab, _rangeBetweenEnemies, _levelHandler.transform),
            new RoadFactory(_defaultRoadPrefab, _bossRoadPrefab, _enemyCount, _levelHandler.transform),
            new PlayerBulletFactory(_bulletPrefab)
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
        
        _rangeBetweenEnemies = roadFactory.TotalTrackLength / _enemyCount;
        
        BossRoadBehaviour bossRoadInstance = roadFactory.CreateInstance() as BossRoadBehaviour;
        _roads.Add(bossRoadInstance);
        _bossInstance = bossRoadInstance.Boss;
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
    
    private void InitLevelHandler()
    {
        _levelHandler.Init(_playerInstance, _roads, _enemies, _bossInstance);
    }
    
    private void InitUIHandler()
    {
        PlayerInput cachedPlayerInput = _inputProvider.GetObjectOfType<PlayerInput>();
        PauseInput cachedPauseInput = _inputProvider.GetObjectOfType<PauseInput>();
        
        _levelUIHandler.Init(_playerInstance, _levelHandler, cachedPlayerInput, cachedPauseInput);
    }
}
