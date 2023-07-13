using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : UnitCommon, IDeath
{
    //private float _current = 0f;
    //private float _endTime = 1f;

    public override void Awake()
    {
        base.Awake();
        IsMoving = true;
        FlipInitialRotation();
    }
    public override void Start()
    {
        base.Start();
    }


    public virtual void Update()
    {
        if (IsMoving)
        {
            Move(Speed);
        }
        else
        {
            Move(SetSpeedToZero());
        }
    }
    //public override void OnTriggerEnter(Collider other)
    //{
    //    UnitContact(other);
    //}

}
