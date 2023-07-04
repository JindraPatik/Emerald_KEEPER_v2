using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTargets : MonoBehaviour
{
    #region Variables

    private Player _player;
    private Unit.Faction _myFaction;
    public List<Unit> FlyAttackers = new List<Unit>();
    private Unit unit;

    #endregion

    private void OnEnable()
    {
        _player.OnUnitDeployed += AddFlyUnitToList;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _myFaction = _player.PlayerFaction;
    }

    private void Update()
    {
        Debug.Log("List count:" + FlyAttackers.Count);
    }

    private void AddFlyUnitToList()
    {
        if((_myFaction != unit.MyFaction) && (unit.tag == "Fly"))
        {
            FlyAttackers.Add(unit);

        }
    }



}
