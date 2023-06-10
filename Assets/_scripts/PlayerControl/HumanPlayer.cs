using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanPlayer : Player
{
    private BTN_click _btnClick;
    private BTN_cooldown _btnCooldown;
    [SerializeField] GameObject _buttonObject;
    [SerializeField] TMP_Text _btnPrice, _btnStrenght;
    private Unit _myUnit;


    public override void Awake() 
    {
        base.Awake();
        _btnClick = _buttonObject.GetComponent<BTN_click>();
        _btnCooldown = _buttonObject.GetComponent<BTN_cooldown>();
        _myUnit = Prefabs[_unitIndex].GetComponent<Unit>();
        _btnPrice.text = ((int)_myUnit.Price).ToString();
        _btnStrenght.text = ((int)_myUnit.Strenght).ToString();

    }

    public override void Start() 
    {
        base.Start();
    }
    
    
    void Update()
    {
        // PressKey(KeyCode.Z, () => DeployUnit(_unitIndex));  
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
