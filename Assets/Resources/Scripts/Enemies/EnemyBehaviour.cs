using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected Text _healthUI;
    
    public event Action<int> OnDeath;
    
    protected int _currentHealth;
    protected int _coinsOnDeath;
    
    public virtual void Init()
    {
        _currentHealth = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyHealthKey);
        _coinsOnDeath = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyRewardKey);

        _healthUI.text = _currentHealth.ToString();
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthUI.text = _currentHealth.ToString();

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            
            TotalStatsHandler.UpdateKey(SaveKeyTemplates.TotalEnemiesKey, 1);
            
            OnDeath.Invoke(_coinsOnDeath);
        }
    }
}
