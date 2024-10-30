using System;
using Spine.Unity;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private Transform firePointTransform;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private HippoMovement hippoMovement;
    [SerializeField] private Slieder powerSlider;

    public event Action OnReloadTrigger;
    
    private void Start()
    {
        objectPool.RegisterPrefab<SnowBall>(snowBallPrefab, 5);
        hippoMovement.OnShoot += ThrowSnowBall;
    }

    private void ThrowSnowBall()
    {
        SnowBall snowBall = objectPool.GetObject<SnowBall>();
        snowBall.transform.position = firePointTransform.position;
        snowBall.StartMoveWitchVelocity(powerSlider.GetPowerValue());
        OnReloadTrigger?.Invoke();
    }

    private void OnDestroy()
    {
        hippoMovement.OnShoot -= ThrowSnowBall;
    }
}