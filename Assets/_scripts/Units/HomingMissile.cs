using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : UnitCommon
{
    private List<Transform> _targets;
    private Rigidbody _myRigidBody;
    [SerializeField] private Transform _rocketSpawnPointObject;
    [SerializeField] private float _speedExponent;
    //private float _speed = 0f;
    

    public override void Awake()
    {
        base.Awake();
        _myRigidBody = GetComponent<Rigidbody>();
        _rocketSpawnPointObject = GetComponent<Transform>();
    }

    private void Update()
    {
        RocketFlight();
    }
    
    public void RocketFlight()
    {
        Speed += _speedExponent * Time.time;
        _myRigidBody.velocity = Vector3.up * Speed;
        
    }

    public void LaunchRocket()
    {
        Vector3 rocketSpawnPoint = new Vector3(_rocketSpawnPointObject.position.x, _rocketSpawnPointObject.position.y, _rocketSpawnPointObject.position.z);
        Instantiate(gameObject, rocketSpawnPoint, Quaternion.identity);
    }

}
