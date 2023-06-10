using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem _rocketFlameParticle;
    [SerializeField] ParticleSystem _jetSmokeParticle;

    void Start()
    {
        _rocketFlameParticle.Play();
        _jetSmokeParticle.Play();
    }

    private void OnDestroy()
    {
        _rocketFlameParticle.Stop();
        _jetSmokeParticle.Stop();
    }
   
}
