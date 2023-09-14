using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : BuildableObject
{
    [SerializeField] protected Text _healthUI;
    
    public event Action<int> OnDeath;
    
    protected int CurrentHealth;
    protected int CoinsOnDeath;
    
    public override void Init()
    {
        CurrentHealth = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyHealthKey);
        CoinsOnDeath = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyRewardKey);

        _healthUI.text = CurrentHealth.ToString();
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        _healthUI.text = CurrentHealth.ToString();

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            
            TotalStatsHandler.UpdateKey(SaveKeyTemplates.TotalEnemiesKey, 1);
            
            OnDeath.Invoke(CoinsOnDeath);
        }
    }
}
