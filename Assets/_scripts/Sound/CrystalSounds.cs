using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectCrystalSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    }

    public void PlayeCrystalCollectSound()
    {
        _audioSource.PlayOneShot(_collectCrystalSound);
    }
}


