using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalParticleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem _spawnParticle;
    [SerializeField] GameObject _deathParticle;

    void Start()
    {
        _spawnParticle.Play();
    }
   
    public void PlayCrystalDeathparticles()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z); 
        GameObject deathParticleInstance = Instantiate(_deathParticle, spawnPosition, Quaternion.identity);
        Destroy(deathParticleInstance, 0.5f);
    }

}
