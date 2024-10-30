using Spine.Unity;

public class ShootState : IState
{
    private SkeletonAnimation _skeletonAnimation;
    private EnemyFirePoint _firePoint;

    public ShootState(SkeletonAnimation skeletonAnimation, EnemyFirePoint firePoint)
    {
        _skeletonAnimation = skeletonAnimation;
        _firePoint = firePoint;
    }
    
    public void Enter()
    {
        _skeletonAnimation.AnimationName = Constans.Shoot;
        _firePoint.ThrowSnowBall();
    }

    public void Exit()
    {
        _skeletonAnimation.AnimationName = Constans.Idle;
    }
}
