using EK.Crystal;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Picker : Harvester, ICollector, IDeath
{
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void Collect()
    {
        IsLoaded = true;
        StopUnitMovement();
        Speed = MoveDirectionSwitch();
        MoveAgain();
    }


    private Booster boosterBox;
    private Player player;
    private Unit unit;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Booster>(out boosterBox))
        {
            Collect();
            boosterBox.Die();
        }
        if (other.gameObject.TryGetComponent<Player>(out player) && IsLoaded)
        {
            unLoad();
            Die();
        }

        if (other.gameObject.TryGetComponent<Unit>(out unit))
        {
            Die();
        }


    }
}