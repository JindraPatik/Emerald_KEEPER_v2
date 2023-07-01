using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour 
{
    #region Variables
    
    [SerializeField] float _speed;
    [SerializeField] int _strenght;
    [SerializeField] Transform _spawnpoint;
    [SerializeField] Faction _myFaction;
    [SerializeField] float _price;
    public enum Faction {Player, Enemy};

    #endregion

    #region Variable properities

    public Faction MyFaction
    {
        get { return _myFaction; }
        set { _myFaction = value; }
    }

    public float Price
    {
        get { return _price; }
    }

    public float Speed 
    {
        get {return _speed;}
        set { _speed = value; }
    
    }

    public int Strenght {get {return _strenght;} }

    public Transform SpawnPoint {get {return _spawnpoint;}}



    #endregion

   

    #region Common Methods
   
    public abstract void Move(float speed);

    public abstract void UnitContact(Collider other);

    #endregion
    
}
