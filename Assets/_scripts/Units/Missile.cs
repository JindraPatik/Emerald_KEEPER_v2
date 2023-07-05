using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unit;
using static UnityEngine.GraphicsBuffer;

public class Missile : UnitCommon
{
    private Transform _currentTarget;
    [SerializeField] private float _rotationForce;
    [SerializeField] float _speedExponent;
    private Rigidbody _rb;
    private List<GameObject> _targets;

    public override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _targets = new List<GameObject>();
        //SetCurrentTarget();
        //SetTargetList();
    }

    public override void Start()
    {
        base.Start();
        //SetTargetList();
        //SetCurrentTarget();
        Debug.Log("Player Fly objects: " + Player.FlyObjects.Count);
        //SetRotation(direction);
        //Vector3 direction = SetDirection();
        SetSpeed();
    }


    private void FixedUpdate()
    {
        if (_currentTarget != null)
        {
            SetRotation(SetDirection()); 
            SetSpeed();
        }
        else
        {
            Debug.Log("No current target");
        }
    }


    private Vector3 SetDirection()
    {
        if (_currentTarget != null)
        {
            Vector3 direction = _currentTarget.position - MyRigidBody.position;
            direction.Normalize();
            return direction; 
        }
        else
        {
            return Vector3.forward;
        }
    }

    private void SetRotation(Vector3 direction)
    {
        Vector3 rotationAmonut = Vector3.Cross(transform.forward, direction);
        _rb.angularVelocity = rotationAmonut * _rotationForce;
    }

    private void SetSpeed()
    {
        Speed += _speedExponent;
        _rb.velocity = transform.forward * Speed;
    }

    public void SpawnRocket()
    {
        if (_targets != null && _targets.Count > 0)
        {
            Vector3 rocketSpawnPoint = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z);
            Instantiate(gameObject, rocketSpawnPoint, Quaternion.identity);
        }

        else
        {
            Debug.Log("NO TARGET!!!");
        }
    }

    public void LaunchRocket()
    {
        SetTargetList();
        SetCurrentTarget();
        SpawnRocket();
    }
    private UnitCommon flyUnit;

    private void SetCurrentTarget()
    {
        if (_targets != null && _targets.Count > 0)
        {
            _currentTarget = _targets[0].transform;
        }
        else
        {
            _currentTarget = null;
            Debug.Log("Targets is NULL or empty");
        }
    }
    private void SetTargetList()
    {
        _targets.Clear();
        foreach (GameObject fly in Player.FlyObjects)
        {
            if (fly != null)
            {
                fly.TryGetComponent<UnitCommon>(out flyUnit);
                if (MyFaction != flyUnit.MyFaction)
                {
                    _targets.Add(fly);
                }
            }

            else
            {
                Debug.Log("Target is null");
            }
        }
    }
}
