using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem _rocketFlameParticle;
    [SerializeField] ParticleSystem _jetSmokeParticle;
    [SerializeField] ParticleSystem _explosionPaticle;
    Rigidbody _myRigidbody;

    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();

        _rocketFlameParticle.Play();
        _jetSmokeParticle.Play();
    }

    private void OnDestroy()
    {
        
        _rocketFlameParticle.Stop();
        _jetSmokeParticle.Stop();
        SpawnParticlesInPlace(_explosionPaticle);
        
    }

    public void SpawnParticlesInPlace(ParticleSystem particleSystem)
    {
        Vector3 myPosition = _myRigidbody.transform.position;
        ParticleSystem particlesObjectToSpawn = Instantiate(_explosionPaticle, myPosition, Quaternion.identity);
        particleSystem.Play();
    }
   
}
