using UnityEngine;
using Spine.Unity;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IPoolableObject
{
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private EnemyFirePoint myFirePoint;
    [SerializeField] private Transform myTransform;
    [SerializeField] private float _runToStartPositionTime, _runToSpawnPointTime, _shootTime, _idleTime, _runTime;


    private Transform _maxPositionOnY, _minPositionOnY, _spawnPoint, _target;
    private Coroutine behavior, timeToDie;
    private StateMachine _stateMachine;
    private EnemyCharacter _character;
    private ObjectPool _objectPool;
    private Spawner _spawner;
    private Score _score;

    private int _numberOfRuns = 2;

    private bool _isReady;

    public bool IsActive { get; private set; }

    public void Initialize(Transform maxPositionOnY, Transform minPositionOnY, ObjectPool objectPool, Transform Target,
        Transform SpawnPoint, EnemyCharacter character, Spawner spawner, Score score)
    {
        _objectPool = objectPool;
        _spawnPoint = SpawnPoint;
        _character = character;
        _spawner = spawner;
        _maxPositionOnY = maxPositionOnY;
        _minPositionOnY = minPositionOnY;
        _target = Target;
        _score = score;
        
        _stateMachine = new StateMachine();

        _shootTime = skeletonAnimation.skeleton.Data.FindAnimation(Constans.Shoot).Duration;
        myFirePoint.Initialize(_target, _objectPool);
        behavior = StartCoroutine(Behavior());
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        IsActive = true;
    }

    public void Deactivate()
    {
        if (timeToDie != null)
        {
            StopCoroutine(TimeToDie());
            timeToDie = null;
        }
        gameObject.SetActive(false);
        IsActive = false;
    }

    private void Start()
    {
        skeletonAnimation.Skeleton.SetSkin(_character.SkinName);
    }

    private void Update()
    {
        skeletonAnimation.GetComponent<Renderer>().sortingOrder = (int)(-transform.position.y * 5);
    }

    private IEnumerator Behavior()
    {
        while (true)
        {
            if (!_isReady)
            {
                Run();
                yield return new WaitForSeconds(_runToStartPositionTime);
                _isReady = true;
            }

            if (_isReady)
            {
                for (int i = 0; i < _numberOfRuns; i++)
                {
                    Run();
                    yield return new WaitForSeconds(_runTime);
                }
                Idle();
                yield return new WaitForSeconds(_idleTime);
                Shoot();
                yield return new WaitForSeconds(_shootTime);
            }
        }
    }

    private void Run() =>
        _stateMachine.ChangeState(new RunState(skeletonAnimation, GetRandomPosition(), myTransform, _character));

    private void Shoot() => _stateMachine.ChangeState(new ShootState(skeletonAnimation, myFirePoint));

    private void Idle() => _stateMachine.ChangeState(new IdleState(skeletonAnimation));

    private void TakeDamage() =>
        _stateMachine.ChangeState(new TakeDamageState(_spawnPoint, myTransform, skeletonAnimation, _character));
    
    private Vector3 GetRandomPosition()
    {
        return new Vector3(_maxPositionOnY.position.x,
            Random.Range(_maxPositionOnY.position.y, _minPositionOnY.position.y),
            _maxPositionOnY.position.z);
    }

    private IEnumerator TimeToDie()
    {
        if (behavior != null)
        {
            StopCoroutine(behavior);
            behavior = null;
        }

        TakeDamage();

        yield return new WaitForSeconds(_runToSpawnPointTime);

        _spawner.EnemyNowDeactivated();
        _isReady = false;
        Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.TryGetComponent<SnowBall>(out var snowBall))
        {
            timeToDie = StartCoroutine(TimeToDie());
            _score.PlusScore(_character.ScoreCost);
        }
    }
}