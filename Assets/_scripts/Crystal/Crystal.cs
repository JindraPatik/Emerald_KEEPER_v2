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
    [SerializeField] ParticleSystem _spawnParticle;
    [SerializeField] ParticleSystem _deathParticle;
    

    public int CrystalValue
    {
        get { return _crystalValue; }
    }

        #endregion
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _spawnParticle.Play();
        }

        #region Methods

        //Destroy me
        public void Die()
    {
        Destroy(gameObject);
    }

    #endregion
}

}
