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
    private List<GameObject> _targets;
    private Vector3 _direction;

    public override void Awake()
    {
        base.Awake();
        _targets = new List<GameObject>();
        Debug.Log("Player Fly objects: " + Player.FlyObjects.Count);
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
        _direction = SetDirection();
        SetRotation(_direction);
        SetSpeed();
    }

    private Vector3 SetDirection()
    {
        Vector3 direction = _currentTarget.position - MyRigidBody.transform.position;
        direction.Normalize();
        return direction;
    }

    private void SetRotation(Vector3 direction)
    {
        Vector3 rotationAmonut = Vector3.Cross(transform.forward, direction);
        MyRigidBody.angularVelocity = rotationAmonut * _rotationForce;
    }

    private void SetSpeed()
    {
        Speed += _speedExponent;
        MyRigidBody.velocity = transform.forward * Speed;
    }

    public void SpawnRocket()
    {
        if (_targets != null && _targets.Count > 0) //tohle nefunguje 
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
        SpawnRocket();
        SetTargetList();
        SetCurrentTarget();
        SetDirection();
    }

    private Attacker flyUnit;

    private void SetTargetList()
    {
        _targets.Clear();

        foreach (GameObject fly in Player.FlyObjects)
        {
            if (fly != null)
            {
                fly.TryGetComponent<Attacker>(out flyUnit);
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
