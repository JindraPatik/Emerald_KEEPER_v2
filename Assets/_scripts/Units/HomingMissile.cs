using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : UnitCommon
{
    private List<Transform> _targets;
    private Rigidbody _rb;
    [SerializeField] private Transform _rocketSpawnPointObject;
    [SerializeField] private float _speedExponent;
    private GameObject _target;
    private Vector3 _direction;
    [SerializeField] float _rotateSpeed = 5f;

    public override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _rocketSpawnPointObject = GetComponent<Transform>();
    }

    
    private void FixedUpdate()
    {
        Debug.Log("targets: " + _targets.ToArray());
        SetTarget();
        RocketFlight();
        RotateRocket();
    }

    private void RotateRocket()
    {
        var heading = _direction = _targets[0].transform.position - _rb.transform.position;
        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    public void RocketFlight()
    {
        Speed += _speedExponent * Time.time;
        _rb.velocity = Vector3.up * Speed;
        
    }

    public void LaunchRocket()
    {
        if (_targets != null)
        {
            Vector3 rocketSpawnPoint = new Vector3(_rocketSpawnPointObject.position.x, _rocketSpawnPointObject.position.y, _rocketSpawnPointObject.position.z);
            Instantiate(gameObject, rocketSpawnPoint, Quaternion.identity); 
        }

        else
        {
            Debug.Log("NO TARGET!!!");
        }
    }

    private Unit flyUnit;

    private void SetTarget()
    {
        foreach (GameObject Fly in Player.FlyObject) 
        { 
            if(Player.FlyObject != null) 
            {
                Fly.TryGetComponent<Unit>(out flyUnit);
                if(MyFaction != flyUnit.MyFaction) 
                {
                    _targets.Add(Fly.transform);
                }
            }

            else
            {
                Debug.Log("Target is null");
            }
        }
    }

}
