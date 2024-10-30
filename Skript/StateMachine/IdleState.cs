using Spine.Unity;

public class IdleState :  IState
{
    private SkeletonAnimation _skeletonAnimation;

    public IdleState(SkeletonAnimation skeletonAnimation)
    {
        _skeletonAnimation = skeletonAnimation;
    }
    
    public void Enter()
    {
        _skeletonAnimation.AnimationName = Constans.Idle;
    }

    public void Exit()
    {
        _skeletonAnimation.AnimationName = Constans.Idle;
    }
}
