using System;
using System.Collections;
using UnityEngine;

public class UnitCommon : Unit, IDeath
{
    #region Variables

    public Rigidbody MyRigidBody;
    private Collider _myCollider;
    private Transform _myTransform;
    [HideInInspector] public float MyInitialSpeed;
    [SerializeField] float _fightTime;
    public bool IsMoving = true;
    public Vector3 UnitPosition;

    public  Action OnUnitDie;
    public Action OnUnitSpawn;
    public Action OnUnitHit;

    #endregion

    #region Initial Methods

    public virtual void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
        _myCollider = GetComponent<Collider>();
    }

    public virtual void OnEnable()
    {
        GameManager.Instance.OnPauseGame += StopUnitMovement;
        GameManager.Instance.OnResumeGame += MoveAgain;
        OnUnitSpawn += AddUnitToFlyList;
        OnUnitDie += RemoveUnitFromFlyList;

        
    }

    public virtual void OnDisable()
    {
        GameManager.Instance.OnPauseGame -= StopUnitMovement;
        GameManager.Instance.OnResumeGame -= MoveAgain;
        OnUnitSpawn -= AddUnitToFlyList;
        OnUnitDie -= RemoveUnitFromFlyList;
    }


    public virtual void Start()
    {
        _myTransform = transform;
        InitialMoveSpeedDirection();
        OnUnitSpawn?.Invoke();
    }

    
    #endregion

    #region Movement methods

    public override void Move(float speed)
    {
        MyRigidBody.velocity = new Vector3(speed, 0f, 0f);
        _myTransform = transform;
    }

    public float MoveDirectionSwitch()
    {
        return Speed = -Speed;
    }

    public float SetSpeedToZero()
    {
        return 0f;
    }


    //If enemy switch direction
    public float InitialMoveSpeedDirection()
    {
        if (MyFaction == Faction.Enemy)
        {
            Speed = -Speed;
        }
        else if (MyFaction == Faction.Player)
        {
             Speed = Speed;
        }
        MyInitialSpeed = Speed;

        return Speed;
    }

    public void StopUnitMovement()
    {
        IsMoving = false;
    }
    public void MoveAgain()
    {
        IsMoving = true;
    }

    public Quaternion FlipInitialRotation()
    {
        _myTransform = transform;
        if (MyFaction == Faction.Enemy)
        {
            _myTransform.rotation = Quaternion.Euler(new Vector3(_myTransform.rotation.x, _myTransform.rotation.y - 180f, _myTransform.rotation.z)); //otoci harvestera na zacatku
            transform.rotation = _myTransform.rotation;
        }
        else if (MyFaction == Faction.Player)
        {
            transform.rotation = _myTransform.rotation;
        }
        return _myTransform.rotation;
    }
    IEnumerator StopToAttack()
    {
        StopUnitMovement();
        yield return new WaitForSeconds(_fightTime);
        MoveAgain();
    }

    #endregion

    #region Methods

    //interface die
    public void Die()
    {
        OnUnitDie?.Invoke();
        UnitPosition = transform.position;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitContact(other);
    }
    public override void UnitContact(Collider otherObject)
    {

        var otherAttacker = otherObject.gameObject.GetComponentInChildren<Unit>();
        var thiefRef = otherObject.gameObject.GetComponent<Thief>();

        if (otherAttacker != null && MyFaction != otherAttacker.MyFaction)
        {
            otherAttacker.TryGetComponent<Thief>(out thiefRef);

            if (Strenght > otherAttacker.Strenght)
            {
                OnUnitHit?.Invoke();

                if (otherAttacker is IDeath otherAttackerDeath)
                {
                    StartCoroutine(StopToAttack());
                    otherAttackerDeath.Die();
                }
                Debug.Log("Utocnik byl zabit!");
            }

            else if ((Strenght < otherAttacker.Strenght))
            {
                Die();
                Debug.Log("Moje jednotka byla zabita.");
            }

            else if (Strenght == otherAttacker.Strenght)
            {
                Die();
                if (otherAttacker is IDeath otherAttackerDeath)
                {
                    otherAttackerDeath.Die();
                }
                Debug.Log("Jednotky se navzajem znicily.");
            }



        }

    }

    public void AddUnitToFlyList()
    {
        if (this.gameObject.tag == "Fly")
        {
            Player.FlyObjects.Add(this.gameObject);   
        }     
    }

    public void RemoveUnitFromFlyList()
    {
        if (this.gameObject.tag == "Fly")
        {
            Player.FlyObjects.Remove(this.gameObject);
        }
    }

    #endregion

}