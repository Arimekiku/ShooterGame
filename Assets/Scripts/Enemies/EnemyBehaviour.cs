using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBehaviour : BuildableObject
{
    [FormerlySerializedAs("_enemyUI")] [SerializeField] protected EnemyUIHandler EnemyUI;
    
    public event Action<int> OnDeath;
    
    protected int CurrentHealth;
    protected int CoinsOnDeath;
    
    public override void Init() { }

    public void SetHealthAndReward(int health, int reward)
    {
        CurrentHealth = health;
        CoinsOnDeath = reward;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        EnemyUI.UpdateHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            
            OnDeath.Invoke(CoinsOnDeath);
        }
    }
}
