using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester_Sounds : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _collectCrystalSound;


    public void PlayCollectCrystalSound() => _audioSource.PlayOneShot(_collectCrystalSound);
    
}
