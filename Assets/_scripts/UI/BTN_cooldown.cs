using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTN_cooldown : MonoBehaviour
{
    private bool _isCooldown = false;
    [SerializeField] Image CD_image;
    private float _cd_Time;
    [SerializeField] Button _button; 
    private KeyboardCooldown _keyboardCooldown;

    public bool IsCooldown
    {
        get { return _isCooldown; }
        set { _isCooldown = value;}
    }
    private void Start()
    {
        _keyboardCooldown = HumanPlayer.HumanPlayerInstance.GetComponent<KeyboardCooldown>();
        _cd_Time = _keyboardCooldown.KeyboardCDTime;
    }
    private void Update() 
    {
        if (_isCooldown && !GameManager.Instance.GameIsPaused)
        {
            ButtonCooldownActive();
        }
    }
    private void ButtonCooldownActive()
    {
        CD_image.fillAmount += 1 / _cd_Time * Time.deltaTime;
        _button.interactable = false;

        if (CD_image.fillAmount >= 1)
        {
            CooldownDisable();
            CD_image.fillAmount = 0;
            _button.interactable = true;
        }

    }

    public void CooldownEnable()
    {
        _isCooldown = true;
    }

    public void CooldownDisable()
    {
        _isCooldown = false;
    }

}
