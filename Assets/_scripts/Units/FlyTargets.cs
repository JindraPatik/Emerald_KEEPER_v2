using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTargets : MonoBehaviour
{
    #region Variables

    private Player _player;
    private Unit.Faction _myFaction;
    public List<GameObject> Flyunits = new List<GameObject>();
    private UnitCommon _unitCommon;


    #endregion

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        _unitCommon.OnUnitSpawn -= AddFlyToList;
        _unitCommon.OnUnitDie -= RemoveFlyToList;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _myFaction = _player.PlayerFaction;
    }
    private void Start()
    {
        _unitCommon = _player.UnitCommon.GetComponent<UnitCommon>();
        _unitCommon.OnUnitSpawn += AddFlyToList;
        _unitCommon.OnUnitDie += RemoveFlyToList;
    }

    private void Update()
    {
        Debug.Log("List count:" + Flyunits.Count);
    }

    private void AddFlyToList()
    {
        Debug.Log("Unit added");
        Flyunits.Add(_player.SpawnedUnit);
    }

    private void RemoveFlyToList() 
    {
        Debug.Log("Unit removed");
        Flyunits.Remove(_player.SpawnedUnit);
    }

}
