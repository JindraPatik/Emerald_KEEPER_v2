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
    [SerializeField] private List<Transform> _missileCheckpoints;

    public override void Awake()
    {
        base.Awake();
        _targets = new List<GameObject>();
        _missileCheckpoints = new List<Transform>();
    }

    public override void Start()
    {
        base.Start();
        SetTargetList();
        SetCurrentTarget();
    }

    private void SetCurrentTarget()
    {
        if (_targets != null && _targets.Count > 0)
        {
            _currentTarget = _missileCheckpoints[0];
        }
        else
        {
            _currentTarget = null;
            Debug.Log("Targets is NULL or empty");
        }
    }

    private void FixedUpdate()
    {
        if (_currentTarget != null)
        {
            SetRotation(SetDirection()); 
            SetSpeed();

            if (HasReachedCheckpoint())
            {
                MoveToNextCheckpoint();
            }
        }
        else
        {
            Debug.Log("No current target");
        }
    }

    private bool HasReachedCheckpoint()
    {
        float distanceThreshold = 1f;
        float distance = Vector3.Distance(transform.position, _currentTarget.position);
        return distance <= distanceThreshold;
    }

    private void MoveToNextCheckpoint()
    {
        int currentIndex = 0;
        int nextIndex = (currentIndex + 1) % _missileCheckpoints.Count; // Pokud jsme na posledním checkpointu, pøejít na první checkpoint
        if (nextIndex == 0)
        {
            _currentTarget = _targets[0].transform;
        }
        else
        {
            _currentTarget = _missileCheckpoints[nextIndex];
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
            return Vector3.up;
        }
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
    }

    private Attacker flyUnit;

    private void SetTargetList()
    {
        _targets.Clear();
        //_missileCheckpoints.Clear();

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
