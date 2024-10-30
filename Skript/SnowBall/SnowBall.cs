using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SnowBall : MonoBehaviour, IPoolableObject, ISnowBall
{
    [SerializeField] private Transform snowBallTransform;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private SnowBallHitEffect snowBallHitEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Coroutine _hitCoroutine;
    private Tween _tween;

    private float _hitCoroutineDelya = 2f;
    
    public bool IsActive { get; private set; }

    public void Activate()
    {
        if (_hitCoroutine != null)
        {
            StopCoroutine(_hitCoroutine);
            _hitCoroutine = null;
        }
        
        if (_tween != null)
        {
            _tween.Kill();
        }
        
        gameObject.SetActive(true);
        capsuleCollider.enabled = true;
        spriteRenderer.enabled = true;
        IsActive = true;
    }

    public void Deactivate()
    {
        if (_hitCoroutine != null)
        {
            StopCoroutine(_hitCoroutine);
            _hitCoroutine = null;
        }
        
        if (_tween != null)
        {
            _tween.Kill();
        }
        IsActive = false;
        gameObject.SetActive(false);
    }

    public void StartMove(Vector3 target, float speed)
    {
        _tween = snowBallTransform.DOMove(target, speed).SetEase(Ease.Linear).OnComplete(Deactivate);
    }

    public void StartMoveWitchVelocity(float speed)
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnHit()
    {
        _hitCoroutine = StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        _rigidbody2D.velocity = Vector2.zero;
        capsuleCollider.enabled = false;
        spriteRenderer.enabled = false;
        snowBallHitEffect.ActivateHitEffect();
        yield return new WaitForSeconds(_hitCoroutineDelya);
        Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
            OnHit();
    }
}