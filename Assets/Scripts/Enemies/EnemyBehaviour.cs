using System;
using UnityEngine;

public class EnemyBehaviour : BuildableObject
{
    [SerializeField] protected EnemyUIHandler EnemyUI;
    
    public event Action<int> OnDeath;

    private int _currentHealth;
    private int _coinsOnDeath;
    
    public override void Init() { }

    public void SetHealthAndReward(int health, int reward)
    {
        _currentHealth = health;
        _coinsOnDeath = reward;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        EnemyUI.UpdateHealth(_currentHealth);

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            
            OnDeath.Invoke(_coinsOnDeath);
        }
    }
}
