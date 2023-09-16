using System;
using UnityEngine;

public class EnemyBehaviour : BuildableObject
{
    [SerializeField] protected EnemyUIHandler _enemyUI;
    
    public event Action<int> OnDeath;
    
    protected int CurrentHealth;
    protected int CoinsOnDeath;
    
    public override void Init()
    {
        CurrentHealth = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyHealthKey);
        CoinsOnDeath = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyRewardKey);
        
        _enemyUI.UpdateHealth(CurrentHealth);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _enemyUI.UpdateHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            
            TotalStatsHandler.UpdateKey(SaveKeyTemplates.TotalEnemiesKey, 1);
            
            OnDeath.Invoke(CoinsOnDeath);
        }
    }
}
