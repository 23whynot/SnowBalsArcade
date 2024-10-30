using System;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.Serialization;


public class HippoMovement : MonoBehaviour, IHippo
{
    [SerializeField] private Transform hippoTransform, maxPointOnY, minPointOnY;
    [SerializeField] private ReloadingProgressBar reloadingProgressBar;
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;

    private bool isShooting;
    private bool animationShootEnd;

    public event Action OnShoot;

    public void Shoot()
    {
        if (reloadingProgressBar.isReloading())
        {
            isShooting = true;
            animationShootEnd = false;
            skeletonAnimation.AnimationName = Constans.Shoot;
        }
    }

    private void Start()
    {
        skeletonAnimation.state.Complete += OnAnimationComplete;
    }

    private void Update()
    {
        if (isShooting) return;

        if (joystick.Vertical > 0.9f)
        {
            Run(Vector3.up);
        }
        else if (joystick.Vertical < -0.9f)
        {
            Run(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.Space) & reloadingProgressBar.isReloading())
        {
            Shoot();
        }
        else
        {
            Idle();
        }
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == Constans.Shoot)
        {
            isShooting = false;
            animationShootEnd = true;
            if (animationShootEnd)
            {
                OnShoot?.Invoke();
            }
        }
    }

    private void Idle()
    {
        skeletonAnimation.AnimationName = Constans.Idle;
    }

    private void Run(Vector3 vector3)
    {
        skeletonAnimation.AnimationName = Constans.Run;
        
        if (hippoTransform.position.y <= maxPointOnY.transform.position.y && hippoTransform.position.y >= minPointOnY.transform.position.y)
        {
            
            hippoTransform.position += vector3 * speed * Time.deltaTime;
            
            if (hippoTransform.position.y > maxPointOnY.position.y)
            {
                hippoTransform.position = new Vector3(hippoTransform.position.x, maxPointOnY.position.y, hippoTransform.position.z);
                Idle();
            }
            else if (hippoTransform.position.y < minPointOnY.position.y)
            {
                hippoTransform.position = new Vector3(hippoTransform.position.x, minPointOnY.position.y, hippoTransform.position.z);
                Idle();
            }
        }
        
    }
    
    private void OnDestroy()
    {
        skeletonAnimation.state.Complete -= OnAnimationComplete;
    }
}