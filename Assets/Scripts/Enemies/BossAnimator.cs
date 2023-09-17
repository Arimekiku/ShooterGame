using UnityEngine;

public class BossAnimator
{
    private static readonly int Walk = Animator.StringToHash("Walk");
    
    private readonly Animator _bossAnimator;

    public BossAnimator(Animator bossAnimator)
    {
        _bossAnimator = bossAnimator;
    }

    public void TriggerWalk()
    {
        _bossAnimator.SetTrigger(Walk);
    }
}