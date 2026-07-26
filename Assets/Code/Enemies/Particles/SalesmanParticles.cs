using UnityEngine;

public class SalesmanParticles : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private EnemyStateController _esc;
    
    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _esc = GetComponent<EnemyStateController>();
    }

    private void Update()
    {
        if (!_esc) return;  // If there's no EnemyStateController, return
        if (_esc.currentState != _esc.chasingState)
        {
            _particleSystem.Stop();
            return;
        }
        if (!_particleSystem.isPlaying) return; // If the particle is still playing, return
        _particleSystem.Play();
    }
}
