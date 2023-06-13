using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioSource _playerAudioSource;
    [SerializeField] AudioClip _playerHitSound;
    [SerializeField] AudioClip _deliverySound;
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnEnable()
    {
        _player.OnPlayerHit += PlayerHasbeenHitsound;
        _player.OnCrysralDelivery += CrystalHasBeenDeliveredSound;
    }

    private void OnDisable()
    {
        _player.OnPlayerHit -= PlayerHasbeenHitsound;
        _player.OnCrysralDelivery -= CrystalHasBeenDeliveredSound;
    }

    private void PlayerHasbeenHitsound()
    {
        _playerAudioSource.PlayOneShot(_playerHitSound);
    }

    private void CrystalHasBeenDeliveredSound()
    {
        _playerAudioSource.PlayOneShot(_deliverySound);
    }
}
