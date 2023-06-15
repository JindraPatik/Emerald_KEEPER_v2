using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text _winText;
    [SerializeField] TMP_Text _looseText;
    private Player _playerHuman;
    private Player _playerEnemy;

    private void Awake()
    {
        _playerHuman = FindAnyObjectByType<HumanPlayer>();
        _playerEnemy = FindAnyObjectByType<Enemy>();
    }

    private void OnEnable()
    {
        _playerHuman.OnPlayerDies += HumanPlayerDied;
        _playerEnemy.OnPlayerDies += EnemyPlayerDied;
    }

    private void OnDisable()
    {
        _playerHuman.OnPlayerDies -= HumanPlayerDied;
        _playerEnemy.OnPlayerDies -= EnemyPlayerDied;
    }

    private void Start()
    {
        _winText.enabled = false;
        _looseText.enabled = false;
    }

    private void EnemyPlayerDied()
    {
        _winText.enabled = true;
        _looseText.enabled = false;
    }

    private void HumanPlayerDied()
    {
        _winText.enabled = false;
        _looseText.enabled = true;

    }

}

