using DG.Tweening;
using Spine.Unity;
using UnityEngine;

public class TakeDamageState : IState
{
    private SkeletonAnimation _skeletonAnimation;
    private Transform _spawnpoint, _myTransform;
    private EnemyCharacter _enemyCharacter;
    private Tween _tween;

    public TakeDamageState(Transform spawnPoint, Transform myTransform, SkeletonAnimation skeletonAnimation,
        EnemyCharacter character)
    {
        _spawnpoint = spawnPoint;
        _skeletonAnimation = skeletonAnimation;
        _myTransform = myTransform;
        _enemyCharacter = character;
    }

    public void Enter()
    {
        _skeletonAnimation.Skeleton.ScaleX = -1f;
        _skeletonAnimation.AnimationName = Constans.Run;
        _tween = _myTransform.transform.DOMove(_spawnpoint.position, _enemyCharacter.Speed).SetEase(Ease.Linear)
            .OnComplete(Exit);
    }

    public void Exit()
    {
        _skeletonAnimation.Skeleton.ScaleX = 1f;
        _tween.Kill();
    }
}