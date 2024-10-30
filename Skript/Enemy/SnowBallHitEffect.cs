using UnityEngine;

public class SnowBallHitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitEffect;

    public void ActivateHitEffect()
    {
        hitEffect.Play();
    }
}