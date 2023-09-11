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

    [Header("Level Preferences")] 
    [SerializeField] private LevelHandler _levelHandler;

    [Header("Input Preferences")] 
    [SerializeField] private InputBehaviour _inputBehaviour;
    private InputProvider _inputProvider;

    [Header("Factories")] 
    [SerializeField] private PlayerBulletFactory _bulletFactory;
    [SerializeField] private RoadFactory _roadFactory;
    [SerializeField] private EnemyFactory _enemyFactory;

    [Header("UI")] 
    [SerializeField] private LevelUIHandler _levelUIHandler;

    private BossBehaviour _bossInstance;
    private readonly List<EnemyBehaviour> _enemies = new();
    private readonly List<RoadBehaviour> _roads = new();
    
    private float _rangeBetweenEnemies;
    private int _enemyCount;
    private int _roadSegmentCount;
    
    private void Awake()
    {
        InitInputProvider();
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

    private void InitPlayer()
    {
        _playerInstance = Instantiate(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
        _playerInstance.Init(_bulletFactory, _inputProvider.GetInput<PlayerInput>() as PlayerInput);

        _defaultCamera.Follow = _playerInstance.transform;
        _defaultCamera.LookAt = _playerInstance.transform;
    }
    
    private void InitInputBehaviour()
    {
        _inputBehaviour.Init(_inputProvider.GetInput<PlayerInput>(), _inputProvider, _playerInstance);
    }

    private void InitRoad()
    {
        _roadSegmentCount = PlayerPrefs.GetInt(SaveKeyTemplates.RoadCountKey);
        _enemyCount = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyCountKey);
        
        _roadFactory.Init(_roadSegmentCount, _levelHandler.transform);

        for (int i = 0; i < _roadSegmentCount; i++)
        {
            RoadBehaviour roadInstance = _roadFactory.CreateInstance();
            
            _roads.Add(roadInstance);
        }
        
        _rangeBetweenEnemies = _roadFactory.TotalTrackLength / _enemyCount;
        
        BossRoadBehaviour bossRoadInstance = _roadFactory.CreateInstance() as BossRoadBehaviour;
        _roads.Add(bossRoadInstance);
        _bossInstance = bossRoadInstance.Boss;
    }
    
    private void InitEnemies()
    {
        _enemyFactory.Init(_rangeBetweenEnemies, _levelHandler.transform);

        for (int i = 0; i < _enemyCount; i++)
        {
            EnemyBehaviour enemyInstance = _enemyFactory.CreateInstance();
            
            _enemies.Add(enemyInstance);
        }
    }
    
    private void InitLevelHandler()
    {
        _levelHandler.Init(_playerInstance, _roads, _enemies, _bossInstance);
    }
    
    private void InitUIHandler()
    {
        PlayerInput cachedPlayerInput = _inputProvider.GetInput<PlayerInput>() as PlayerInput;
        PauseInput cachedPauseInput = _inputProvider.GetInput<PauseInput>() as PauseInput;
        
        _levelUIHandler.Init(_playerInstance, _levelHandler, cachedPlayerInput, cachedPauseInput);
    }
}
