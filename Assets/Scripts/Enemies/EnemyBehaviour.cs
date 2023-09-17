using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected EnemyUIHandler EnemyUI;
    
    public event Action<int> OnDeath;

    private int _currentHealth;
    private int _coinsOnDeath;
    
    public void SetHealthAndReward(int health, int reward)
    {
        _currentHealth = health;
        _coinsOnDeath = reward;
        
        EnemyUI.UpdateHealth(_currentHealth);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehaviour player))
            player.Death();
    }
}
