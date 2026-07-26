using UnityEngine;

public class FrogParticles : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayParticle()
    {
        _particleSystem.Play();
    }
}
