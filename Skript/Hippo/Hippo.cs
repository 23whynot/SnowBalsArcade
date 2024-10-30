using UnityEngine;

public class Hippo : MonoBehaviour
{
    [SerializeField] private Health _health;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<ISnowBall>(out var snowBall))
        {
            _health.MinusHP();
        }
    }
}