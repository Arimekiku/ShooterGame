using System;
using UnityEngine;

public class PlayerBullet : BuildableObject
{
    [SerializeField] private Vector3 _velocity;

    private const float MaximumRange = 15f;
    
    private int _damage;
    private Rigidbody _body;
    private Vector3 _initialPosition;

    public override void Init()
    {
        _damage = PlayerPrefs.GetInt(SaveKeyTemplates.AttackDamageKey);

        _body = GetComponent<Rigidbody>();
        _body.velocity = _velocity;
        
        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
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