using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "SO/Enemy Factory")]
public class EnemyFactory : ScriptableObject, IEnemyFactory
{
    [SerializeField] private EnemyBehaviour _enemyPrefab;
    
    private Transform _parent;
    private Vector3 _offset;
    private Vector3 _enemySpawnPosition;

    public void Init(float rangeBetweenEnemies, Transform newParent)
    {
        _offset = new(0, 0, rangeBetweenEnemies);

        _enemySpawnPosition = Vector3.zero;
        _parent = newParent;
    }

    public EnemyBehaviour CreateInstance()
    {
        EnemyBehaviour newEnemyInstance = Instantiate(_enemyPrefab, _parent);
        newEnemyInstance.transform.position = _enemySpawnPosition;
        newEnemyInstance.Init();

        _enemySpawnPosition = new Vector3(0, 0, _enemySpawnPosition.z) + _offset;
        _enemySpawnPosition.x = Random.Range(-2f, 2f);

        return newEnemyInstance;
    }
}
