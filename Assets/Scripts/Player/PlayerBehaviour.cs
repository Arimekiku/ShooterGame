using System;
using UnityEngine;

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

        input.OnMovePressed += MoveSideways;
    }
    
    public void DisablePlayer()
    {
        PlayerWeapon.StopAttack();
        _body.useGravity = false;
        
        DisableForwardMovement();
    }

    public void EnablePlayer()
    {
        PlayerWeapon.StartAttack();
        _body.useGravity = true;
        
        EnableForwardMovement();
    }
    
    public void Death()
    {
        DisablePlayer();
        OnDeath.Invoke();
    }


    public void DisableForwardMovement()
    {
        _body.velocity = Vector3.zero;
    }
    
    private void EnableForwardMovement()
    {
        _body.velocity = PlayerInfo.Velocity;
    }
    
    private void MoveSideways(float offset)
    {
        Vector3 movementDirection = new(offset * PlayerInfo.SidewaysSpeed, 0, 0);
        transform.Translate(movementDirection * Time.fixedDeltaTime);
    }
}
