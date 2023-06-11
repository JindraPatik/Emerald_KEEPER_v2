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
    //public float MyCurrentSpeed;

    #endregion

    #region Initial Methods

    //Initialization
    public virtual void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody>();
        _myCollider = GetComponent<Collider>();
    }

    public virtual void Start()
    {
        _myTransform = transform;
        InitialMoveSpeedDirection();
    }

    #endregion


    #region Movement methods

    //base moving
    public override void Move(float speed)
    {
        MyRigidBody.velocity = new Vector3(speed, 0f, 0f);
        _myTransform = transform;
    }

    //switch to direction
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
        //MyCurrentSpeed = Speed;
        StopUnitMovement();
        yield return new WaitForSeconds(_fightTime);
        MoveAgain();
    }

    #endregion

    #region Methods

    //interface die
    public void Die()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitContact(other);
    }

    //Logic of fight between units
    public override void UnitContact(Collider otherObject)
    {

        var otherAttacker = otherObject.gameObject.GetComponentInChildren<Unit>();

        if (otherAttacker != null && MyFaction != otherAttacker.MyFaction)
        {
            if (Strenght > otherAttacker.Strenght)
            {
                if (otherAttacker is IDeath otherAttackerDeath)
                {
                    StartCoroutine(StopToAttack());
                    otherAttackerDeath.Die();
                }
                // Debug.Log("Utocnik byl zabit!");
            }

            else if (Strenght < otherAttacker.Strenght)
            {
                Die();
                // Debug.Log("Moje jednotka byla zabita.");
            }

            else if (Strenght == otherAttacker.Strenght)
            {
                Die();
                if (otherAttacker is IDeath otherAttackerDeath)
                {
                    otherAttackerDeath.Die();
                }
                // Debug.Log("Jednotky se navzajem znicily.");
            }

        }

    }


    #endregion

}