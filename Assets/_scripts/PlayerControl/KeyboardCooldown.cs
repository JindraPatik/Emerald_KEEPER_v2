using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCooldown : MonoBehaviour
{
    private bool _isCooldown;
    private float _currentCDTime = 0f;
    private bool _keyboardInputEnabled = true;
    [SerializeField] float _keyboardCDTime = 1f; //zatim je CD magical number

    public bool IsKeyboardInputEnabled
    {
        get { return _keyboardInputEnabled; }
        set { _keyboardInputEnabled = value; }
    }
    public float KeyboardCDTime
    {
        get { return _keyboardCDTime; }
    }
    public bool IsCooldown
    { 
        get { return _isCooldown; } 
        set { _isCooldown = value;}
    }
    private void Update()
    {
        if (_isCooldown) 
        {
            KeyboardCooldownActive();
        }
    }
    public void StartCooldown()
    {
        CooldownEnable();
        _keyboardInputEnabled = false;
        _currentCDTime = 0f;
    }
    private void KeyboardCooldownActive()
    {
        _currentCDTime += 1 / _keyboardCDTime * Time.deltaTime;
        if (_currentCDTime >= 1)
        {
            CooldownDisable();
            _currentCDTime = 0f;
            _keyboardInputEnabled = true;
        }
    }
    public void CooldownEnable() => _isCooldown = true;
    public void CooldownDisable() => _isCooldown = false;
}
