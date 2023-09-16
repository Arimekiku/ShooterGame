using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInfo PlayerInfo;
    [SerializeField] private PlayerWeapon PlayerWeapon;

    private Rigidbody _body;
    
    public event Action OnDeath;
    public PlayerWeapon Weapon => PlayerWeapon;
    
    public void Init(PlayerInput input)
    {
        _body = GetComponent<Rigidbody>();
        _body.velocity = PlayerInfo.Velocity;

        input.OnMovePressed += MoveSideways;
    }

    private void DisableForwardMovement()
    {
        _body.velocity = Vector3.zero;
    }
    
    private void DisablePlayer()
    {
        PlayerWeapon.StopAttack();
        _body.useGravity = false;
        
        DisableForwardMovement();
    }
    
    private void MoveSideways(float offset)
    {
        Vector3 movementDirection = new(offset * PlayerInfo.SidewaysSpeed, 0, 0);
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
