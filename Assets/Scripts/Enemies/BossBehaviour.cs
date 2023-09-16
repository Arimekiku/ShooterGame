using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    [SerializeField] private Vector3 _velocity;

    private bool _playerIsHere;

    public override void Init()
    {
        CurrentHealth = PlayerPrefs.GetInt(SaveKeyTemplates.BossHealthKey);
        CoinsOnDeath = PlayerPrefs.GetInt(SaveKeyTemplates.EnemyRewardKey);

        _enemyUI.UpdateHealth(CurrentHealth);
    }

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
        
        transform.Translate(_velocity * Time.fixedDeltaTime);
    }

    public void EnableBoss()
    {
        _playerIsHere = true;
    }
}