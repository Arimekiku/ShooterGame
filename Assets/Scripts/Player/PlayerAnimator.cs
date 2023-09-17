using UnityEngine;

public class PlayerAnimator
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Dance = Animator.StringToHash("Dance");
    private static readonly int Run = Animator.StringToHash("Run");
    
    private readonly Animator _playerAnimator;

    public PlayerAnimator(Animator playerAnimator)
    {
        _playerAnimator = playerAnimator;
    }

    public void TriggerRun()
    {
        _playerAnimator.SetTrigger(Run);
    }

    public void TriggerIdle()
    {
        _playerAnimator.SetTrigger(Idle);
    }

    public void TriggerDance()
    {
        _playerAnimator.SetTrigger(Dance);
    }
}