using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    #region Variables
    
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] float _health;
    [SerializeField] float _resourcesValue;
    [SerializeField] float _resourcesIncreasedPerSecond;
    private bool _isDead;

    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }


    public GameObject[] Prefabs
    {
         get { return _prefabs; } 
         set { _prefabs = value; }
    }

    public float ResourcesValue
    {
        get { return _resourcesValue; }
        set { _resourcesValue = value; }
        
    }

    public float ResourcesIncreasedPerSecond
    {
        get { return _resourcesIncreasedPerSecond; }
    }

    public float Health
    {
        get { return _health; }
        
        //Health nebude nikdy zaporne
        set
        {
            _health = Mathf.Max(value, 0);
        } 
    }

    
    #endregion

   
    #region Methods
   
    public abstract void SpawnUnit(int unitIndex);

    #endregion
    
}
