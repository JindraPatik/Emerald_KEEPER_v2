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
        if (Input.GetKeyDown(KeyCode.Keypad1) && _keyboardCooldown.IsKeyboardInputEnabled)
        {
            DeployUnit(_unitIndex);
            _keyboardCooldown.StartCooldown();
        }
    }
}
