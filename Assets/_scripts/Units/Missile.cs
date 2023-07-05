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
    [SerializeField] private float _aimingDelay;
    [SerializeField] private float _rocketAutodestructionTime;
    private List<GameObject> _targets;
    private Vector3 _direction;
    private Attacker flyUnit;

    public override void Awake()
    {
        base.Awake();
        _targets = new List<GameObject>();
        Debug.Log("Player Fly objects: " + Player.FlyObjects.Count);
    }

    private void FixedUpdate()
    {
        SetTargetList();
        SetCurrentTarget();
        SetSpeed();

        if (_currentTarget != null)
        {
            StartCoroutine(AimingActivation());
        }
        else
        {
            Debug.Log("No current target");
        }
    }

    public override void Start()
    {
        StartCoroutine(DieWithDelay(_rocketAutodestructionTime)); //chipne za 6sec
    }
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


    IEnumerator AimingActivation()
    {
        yield return new WaitForSeconds(_aimingDelay);

        SetRotation(SetDirection());
        SetDirection();
    }

    private Vector3 SetDirection()
    {
        if (_currentTarget != null)
        {
            Vector3 direction = _currentTarget.transform.position - transform.position;
            direction.Normalize();
            return direction;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void SetRotation(Vector3 direction)
    {
        Vector3 rotationAmonut = Vector3.Cross(MyRigidBody.transform.up, direction);
        MyRigidBody.angularVelocity = rotationAmonut * _rotationForce;
    }

    private void SetSpeed()
    {
        Speed += _speedExponent;
        MyRigidBody.velocity = transform.up * Speed;
    }

    public void SpawnRocket()
    {
        Vector3 rocketSpawnPoint = new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z);
        Instantiate(gameObject, rocketSpawnPoint, Quaternion.identity);
    }

    public void LaunchRocket()
    {
        SpawnRocket();
    }


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
                Debug.Log("Zadne target v _targets");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fly")
        {
            Die();
        }
    }

    IEnumerator DieWithDelay(float dieDelay) 
    {
        yield return new WaitForSeconds(dieDelay);
        Die();
    }

}