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
        Debug.Log("Player Fly objects: " + Player.FlyObjects.Count);
>>>>>>> parent of cd16cc9 (Raketa l√≠t√° ale nakokot :D)
    }

    private void SetCurrentTarget()
    {
        if (_targets != null && _targets.Count > 0)
        {
            _currentTarget = _missileCheckpoints[0];
        }
        else
        {
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
=======
        _direction = SetDirection();
        SetRotation(_direction);
        SetSpeed();
>>>>>>> parent of cd16cc9 (Raketa l√≠t√° ale nakokot :D)
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
        int nextIndex = (currentIndex + 1) % _missileCheckpoints.Count; // Pokud jsme na poslednÌm checkpointu, p¯ejÌt na prvnÌ checkpoint
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
        SetCurrentTarget();
        SetTargetList();
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
