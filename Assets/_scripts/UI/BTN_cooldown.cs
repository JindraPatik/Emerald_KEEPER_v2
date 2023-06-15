using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTN_cooldown : MonoBehaviour
{
    private bool _isCooldown;
    [SerializeField] Image CD_image;
    [SerializeField] float _cd_Time;
    [SerializeField] Button _button; 

    public bool IsCooldown
    {
        get { return _isCooldown; }
        set { _isCooldown = value;}
    }


    private void Update() 
    {
        if (_isCooldown)
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
