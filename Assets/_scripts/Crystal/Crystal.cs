using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace EK.Crystal
{
    public class Crystal : MonoBehaviour, IDeath
{
    #region Variables
    
    [SerializeField] int _crystalValue;
    Transform _transform;
    private Harvester _harvester;
    CrystalParticleSpawner _crystalParticleSpawner;

    public int CrystalValue
    {
        get { return _crystalValue; }
    }
        private void Awake()
        {
            _crystalParticleSpawner = GetComponent<CrystalParticleSpawner>();
        }
        #endregion
        private void Start()
        {
            _transform = GetComponent<Transform>();
        }
        #region Methods
        public void Die()
    {
        Destroy(gameObject);
    }
    #endregion
}

}
