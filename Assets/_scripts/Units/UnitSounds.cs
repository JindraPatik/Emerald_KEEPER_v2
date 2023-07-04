using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSounds : MonoBehaviour
{
    [SerializeField] AudioSource _unitAudiSource;
    [SerializeField] AudioClip _unitHitSound;
    
    UnitCommon _unitCommon;

    
    private void Awake()
    {
        _unitCommon = GetComponent<UnitCommon>();   
    }

    private void OnEnable()
    {
        _unitCommon.OnUnitHit += PlayUnitHitSound;
    }

    private void OnDisable()
    {
        _unitCommon.OnUnitHit -= PlayUnitHitSound;
    }

    private void PlayUnitHitSound()
    {
        _unitAudiSource.PlayOneShot(_unitHitSound);
    }
}
