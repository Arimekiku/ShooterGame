using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    [SerializeField] private PlayerWeapon _weapon;

    private Rigidbody _body;

    public event Action OnDeath;
    
    public void Init(IBulletFactory bulletBulletFactory, PlayerInput input)
    {
        _weapon.Init(bulletBulletFactory);

        _body = GetComponent<Rigidbody>();
        _body.velocity = _data.Velocity;

        input.OnMovePressed += MoveSideways;
    }

    private void DisableForwardMovement()
    {
        _body.velocity = Vector3.zero;
    }
    
    private void DisablePlayer()
    {
        _weapon.StopAttack();
        _body.useGravity = false;
        
        DisableForwardMovement();
    }
    
    private void MoveSideways(float offset)
    {
        Vector3 movementDirection = new(offset * _data.SidewaysSpeed, 0, 0);
        transform.Translate(movementDirection * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyBehaviour _))
        {
            DisablePlayer();
            OnDeath.Invoke();
            return;
        }

        if (other.TryGetComponent(out BossBehaviour _))
        {
            DisablePlayer();
            OnDeath.Invoke();
            return;
        }
        
        if (other.TryGetComponent(out BossRoadBehaviour _))
        {
            DisableForwardMovement();
            return;
        }

        if (other.TryGetComponent(out RoadBehaviour _))
        {
            DisablePlayer();
            OnDeath.Invoke();
        }
    }
}
