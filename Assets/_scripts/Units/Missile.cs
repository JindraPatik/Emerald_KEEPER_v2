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
<<<<<<< HEAD
    [SerializeField] private List<Transform> _missileCheckpoints;
=======
>>>>>>> parent of 8bd5e5f (Stale nefunguje target)

    public override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
        _targets = new List<GameObject>();
<<<<<<< HEAD
        //SetCurrentTarget();
        //SetTargetList();
        //_missileCheckpoints = new List<Transform>();
        _currentTarget = _missileCheckpoints[0];
    }

    public override void Start()
    {
        base.Start();
=======
        SetTarget();
        SetCurrentTarget();
>>>>>>> parent of 8bd5e5f (Stale nefunguje target)
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
<<<<<<< HEAD
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
        
=======
        Debug.Log("Player Fly objects: " + Player.FlyObjects.Count);
        Vector3 direction = SetDirection();
        SetRotation(direction);
        SetSpeed();
>>>>>>> parent of 8bd5e5f (Stale nefunguje target)
    }

    private Vector3 SetDirection()
    {
<<<<<<< HEAD
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
=======
        Vector3 direction = _currentTarget.position - _rb.position;
        direction.Normalize();
        return direction;
>>>>>>> parent of 8bd5e5f (Stale nefunguje target)
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

<<<<<<< HEAD
    public void LaunchRocket()
    {
        SetCurrentTarget();
        SetTargetList();
        SpawnRocket();
    }
=======
    private UnitCommon flyUnit;
>>>>>>> parent of 8bd5e5f (Stale nefunguje target)

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
