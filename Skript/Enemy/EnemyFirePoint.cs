using UnityEngine;

public class EnemyFirePoint : MonoBehaviour
{
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float snowBallSpeed;
    
    private ObjectPool _objectPool;
    private Transform _targetPosition;

    public void Initialize(Transform targetPosition, ObjectPool objectPool)
    {
        _objectPool = objectPool;
        _targetPosition = targetPosition;
    }

    public void ThrowSnowBall()
    {
        SnowBall snowBall = _objectPool.GetObject<SnowBall>();
        snowBall.transform.position = firePoint.position;
        snowBall.StartMove(GetTargetPosition(), snowBallSpeed);
    }

    public Vector3 GetTargetPosition()
    {
        return new Vector3(_targetPosition.position.x, firePoint.position.y, firePoint.position.z);
    }
}