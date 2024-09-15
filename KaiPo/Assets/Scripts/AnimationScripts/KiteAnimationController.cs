using UnityEngine;

public class KiteAnimationController
{
    private Animator _animator;

    private static int RFAkey = Animator.StringToHash("RFA");
    private static int SFAkey = Animator.StringToHash("SFA");

    public KiteAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAnimation(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.SFA:
                PlaySFA();
                break;
            case AnimationType.RFA:
                PlayRFA();
                break;
        }
    }

    //making function for all the triggers
    private void PlaySFA()
    {
        _animator.SetTrigger(SFAkey);       //----------------------------------------------
    }

    private void PlayRFA()
    {
        _animator.SetTrigger(RFAkey);   //----------------------------------------------
    }

}

