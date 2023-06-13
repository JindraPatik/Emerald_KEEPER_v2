using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioSource _playerAudioSource;
    [SerializeField] AudioClip _playerHitSound;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.OnPlayerHit += PlayerHasbeenHitsound;
    }

    private void OnDisable()
    {
        _player.OnPlayerHit -= PlayerHasbeenHitsound;
    }

    private void PlayerHasbeenHitsound()
    {
        _playerAudioSource.PlayOneShot(_playerHitSound);
    }
}
