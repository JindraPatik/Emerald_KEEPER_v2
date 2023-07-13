using EK.Crystal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Thief : Harvester, ICollector
{
    private Crystal _crystalRef;
    private Harvester _harvester;
    private Player _playerRef;
    [SerializeField] private float _stolenResources;
    
    public override void Collect()
    {
        IsLoaded = true;
        StopUnitMovement();
        IsMovingUp = true;
        Speed = Speed / 2;
        Speed = MoveDirectionSwitch();
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Pokud Thief narazi do Harvesteru
        if (other.gameObject.TryGetComponent<Harvester>(out _harvester))
        {
            if (MyFaction != _harvester.MyFaction && _harvester.IsLoaded)
            {
                Collect();
                _harvester.Die();
            }
            else if (other != _harvester)
            {
                UnitContact(other);
            }
        }
        else if (!other.gameObject.TryGetComponent<Crystal>(out _crystalRef))
        {
            base.OnTriggerEnter(other);
        }

        //Thief narazi do hrace
        if (other.gameObject.TryGetComponent<Player>(out _playerRef))
        {
            if (MyFaction == _playerRef.PlayerFaction && IsLoaded)
            {
                UnitCurrentPosition = transform.position;
                _playerRef.ResourcesValue += _stolenResources;
                unLoad();
                Die(); 
            }

            if (MyFaction != _playerRef.PlayerFaction && !IsLoaded)
            {
                if (_playerRef.ResourcesValue < _stolenResources)
                {
                    _playerRef.ResourcesValue -= _playerRef.ResourcesValue;
                    _stolenResources = _playerRef.ResourcesValue;
                    Collect();
                }
                else
                {
                    _playerRef.ResourcesValue -= _stolenResources;
                    Collect(); 
                }
            }

            
        }
    }
}
