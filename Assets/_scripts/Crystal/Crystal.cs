using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace EK.Crystal
{
    public class Crystal : MonoBehaviour, IDeath
{
    #region Variables
    
    [SerializeField] int _crystalValue;
    private Harvester _harvester;

    public int CrystalValue
    {
        get { return _crystalValue; }
    }

    #endregion
    
   
    #region Methods
   
    //Destroy me
    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}

}
