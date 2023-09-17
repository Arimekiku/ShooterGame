using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInfo PlayerInfo;
    [SerializeField] private PlayerWeapon PlayerWeapon;
    [SerializeField] private Animator Animator;

    private Rigidbody _body;
    
    public event Action OnDeath;
    public PlayerWeapon Weapon => PlayerWeapon;
    public PlayerAnimator PlayerAnimator { get; private set; }
    
    public void Init(PlayerInput input)
    {
        _body = GetComponent<Rigidbody>();

        PlayerAnimator = new(Animator);

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
        
        PlayerAnimator.TriggerIdle();
    }
    
    private void EnableForwardMovement()
    {
        _body.velocity = PlayerInfo.Velocity;
        
        PlayerAnimator.TriggerRun();
    }
    
    private void MoveSideways(float offset)
    {
        Vector3 movementDirection = new(offset * PlayerInfo.SidewaysSpeed, 0, 0);
        transform.Translate(movementDirection * Time.fixedDeltaTime);
    }
}
