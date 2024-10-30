using Spine.Unity;
using UnityEngine;
using DG.Tweening;

public class RunState : IState
{
    private SkeletonAnimation _skeletonAnimation;
    private Vector3 _doMovePosition; 
    private Transform _myTransform;
    private EnemyCharacter _character;
    
    private Tween _tween;
    
    public RunState(SkeletonAnimation skeletonAnimation, Vector3 doMovePosition, Transform myTransform, EnemyCharacter character)
    {
        _skeletonAnimation = skeletonAnimation;
        _doMovePosition = doMovePosition; 
        _myTransform = myTransform;
        _character = character;
    }

    public void Enter()
    {
        _skeletonAnimation.AnimationName = Constans.Run;

        _tween = _myTransform.DOMove(_doMovePosition, _character.Speed)
            .SetEase(Ease.Linear).OnComplete(() => { Exit(); });
    }

    public void Exit()
    {
        _tween.Kill();
        _skeletonAnimation.AnimationName = Constans.Idle;
    }
}