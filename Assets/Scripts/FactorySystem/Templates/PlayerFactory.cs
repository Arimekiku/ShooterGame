using UnityEngine;

public class PlayerFactory : GameFactory
{
    private const string PlayerPrefabPath = "Prefabs/Player/Player";
    
    private readonly PlayerInput _playerInput;

    public PlayerFactory(Transform newContainer, PlayerInput playerInput) : base(newContainer)
    {
        PlayerBehaviour playerPrefab = Resources.Load<PlayerBehaviour>(PlayerPrefabPath);
        
        DefaultInstancesPrefabs.Add(playerPrefab);
        
        _playerInput = playerInput;
    }

    public PlayerBehaviour CreateInstance()
    {
        PlayerBehaviour newPlayer = CreateInstance<PlayerBehaviour>();
        newPlayer.Init(_playerInput);

        return newPlayer;
    }
}