using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text _winText;
    [SerializeField] TMP_Text _looseText;
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    private void OnEnable()
    {
        _player.OnPlayerDies += ShowGameEndCondition;
    }

    private void OnDisable()
    {
        _player.OnPlayerDies -= ShowGameEndCondition;
    }

    private void Start()
    {
        _winText.enabled = false;
        _looseText.enabled = false;
    }

    public void ShowGameEndCondition()
    {
        _winText.enabled = (_player.PlayerFaction == Unit.Faction.Enemy);
        _looseText.enabled = (_player.PlayerFaction == Unit.Faction.Player);
    }

}

