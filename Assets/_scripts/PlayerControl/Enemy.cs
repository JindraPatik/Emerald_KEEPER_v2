using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Player
{
    KeyboardCooldown _keyboardCooldown;

    public override void Awake()
    {
        base.Awake();
        _keyboardCooldown = GetComponent<KeyboardCooldown>();
        _keyboardCooldown.CooldownDisable();
    }

    void Update()
    {
        if (!GameManager.Instance.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                if (_keyboardCooldown.IsKeyboardInputEnabled)
                {
                    DeployUnit(UnitIndex);
                    _keyboardCooldown.StartCooldown();
                }
            } 
        }
    }
}
