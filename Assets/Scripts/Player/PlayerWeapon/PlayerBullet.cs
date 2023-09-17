using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Vector3 Velocity;

    private const float MaximumRange = 15f;
    
    private int _damage;
    private Rigidbody _body;
    private Vector3 _initialPosition;

    public void Init(Vector3 position, int damage)
    {
        _body = GetComponent<Rigidbody>();
        _body.velocity = Velocity;
        
        transform.position = position;
        _initialPosition = position;

        _damage = damage;
    }
    
    private void FixedUpdate()
    {
        if (_initialPosition == Vector3.zero)
            return;
        
        if (Math.Abs(_initialPosition.z - transform.position.z) > MaximumRange)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyBehaviour enemy))
        {
            enemy.TakeDamage(_damage);
            
            Destroy(gameObject);
        }
    }
}