﻿using UnityEngine;
using UnityEngine.Serialization;

public class BossBehaviour : EnemyBehaviour
{
    [SerializeField] private Vector3 Velocity;

    private bool _playerIsHere;

    public override void Init() { }

    public override void TakeDamage(int damage)
    {
        if (!_playerIsHere)
            return;
        
        base.TakeDamage(damage);
    }

    private void FixedUpdate()
    {
        if (!_playerIsHere)
            return;
        
        transform.Translate(Velocity * Time.fixedDeltaTime);
    }

    public void EnableBoss()
    {
        _playerIsHere = true;
    }
}