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
        SetTarget();
        SetCurrentTarget();
    }

    private void SetCurrentTarget()
    {
        if (_targets != null && _targets.Count > 0)
        {
            _currentTarget = _targets[0].transform;
        }
        else
        {
            Debug.Log("Targets is NULL or empty");
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("Player Fly objects: " + Player.FlyObjects.Count);
        Vector3 direction = SetDirection();
        SetRotation(direction);
        SetSpeed();
    }

    private Vector3 SetDirection()
    {
        Vector3 direction = _currentTarget.position - _rb.position;
        direction.Normalize();
        return direction;
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

    public void LaunchRocket()
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

    private UnitCommon flyUnit;

    private void SetTarget()
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
