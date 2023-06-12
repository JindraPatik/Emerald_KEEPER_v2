using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParticleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem _rocketFlameParticle;
    [SerializeField] ParticleSystem _jetSmokeParticle;
    [SerializeField] GameObject _explosionPaticleGO;
    Rigidbody _myRigidbody;
    private UnitCommon _unitCommon;


    private void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _unitCommon = GetComponent<UnitCommon>();
    }

    private void OnEnable()
    {
        _unitCommon.OnUnitDie += SpawnDeathParticles;
    }
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

    public void SpawnDeathParticles()
    {
        Vector3 myPosition = _myRigidbody.transform.position;
        GameObject particlesObjectToSpawn = Instantiate(_explosionPaticleGO, myPosition, Quaternion.identity);
    }
   
}
