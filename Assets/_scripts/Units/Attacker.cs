using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : UnitCommon
{
    private float _current = 0f;
    private float _endTime = 1f;

    public override void Awake()
    {
        base.Awake();
        IsMoving = true;
        FlipInitialRotation();
    }
    

    public virtual void Update()
    {
        if (IsMoving)
        {
            Move(Speed);
        }

    }
    public virtual void OnTriggerEnter(Collider other)
    {
        UnitContact(other);
    }



}
