using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanPlayer : Player
{
    private BTN_click _btnClick;
    private BTN_cooldown _btnCooldown;
    private KeyboardCooldown _keyboardCooldown;
    [SerializeField] GameObject _buttonObject;
    [SerializeField] TMP_Text _btnPrice, _btnStrenght;
    private Unit _myUnit;
    public static HumanPlayer HumanPlayerInstance;


    public override void Awake() 
    {
        base.Awake();
        _btnClick = _buttonObject.GetComponent<BTN_click>();
        _btnCooldown = _buttonObject.GetComponent<BTN_cooldown>();
        _keyboardCooldown = GetComponent<KeyboardCooldown>();
        _myUnit = Prefabs[_unitIndex].GetComponent<Unit>();
        _btnPrice.text = ((int)_myUnit.Price).ToString();
        _btnStrenght.text = ((int)_myUnit.Strenght).ToString();
        HumanPlayerInstance = this;


    }

    public override void Start() 
    {
        base.Start();
    }
    
    
    void Update()
    {
        if(!_btnCooldown.IsCooldown)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                    {
                        DeployUnit(_unitIndex);
                        _btnClick.ButtonPressed();
                        _btnCooldown.CooldownEnable();
                    }
                else if (Input.GetKeyUp(KeyCode.Z))
                    {
                        _btnClick.ButtonUnpressed();
                    }
            }
    }
}
