using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanPlayer : Player
{
    private BTN_click _btnClick;
    private BTN_cooldown _btnCooldown;
    public KeyboardCooldown _keyboardCooldown;
    [SerializeField] List<GameObject> _buttonObjects;
    [SerializeField] List<TMP_Text> _btnPrices, _btnStrenghts;
    private Unit _myUnit;
    public static HumanPlayer HumanPlayerInstance;


    public override void Awake() 
    {
        base.Awake();
        //_btnClick = _buttonObject.GetComponent<BTN_click>();
        //_btnCooldown = _buttonObject.GetComponent<BTN_cooldown>();
        //_keyboardCooldown = GetComponent<KeyboardCooldown>();
        //_myUnit = Prefabs[_unitIndex].GetComponent<Unit>();
        //_btnPrice.text = ((int)_myUnit.Price).ToString();
        //_btnStrenght.text = ((int)_myUnit.Strenght).ToString();

        for (int i = 0; i < _buttonObjects.Count; i++)
        {
            GameObject buttonObject = _buttonObjects[i];
            TMP_Text btnPrice = _btnPrices[i];
            TMP_Text btnStrenght = _btnStrenghts[i];

            BTN_click _btnClick = buttonObject.GetComponent<BTN_click>();
            BTN_cooldown _btnCooldown = buttonObject.GetComponent<BTN_cooldown>();
            KeyboardCooldown _keyboardCooldown = GetComponent<KeyboardCooldown>();

            // Naètení Unit pro každé tlaèítko
            _myUnit = Prefabs[i].GetComponent<Unit>();

            // Pøiøazení hodnot pro tlaèítko
            btnPrice.text = ((int)_myUnit.Price).ToString();
            btnStrenght.text = ((int)_myUnit.Strenght).ToString();

        }
        HumanPlayerInstance = this;
    }

    public override void Start() 
    {
        base.Start();
    }
    
    
    void Update()
    {
        if(!_btnCooldown.IsCooldown && !GameManager.Instance.GameIsPaused)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                    {
                        DeployUnit(UnitIndex);
                        _btnClick.ButtonPressed();
                        _btnCooldown.CooldownEnable();
                    }
                else if (Input.GetKeyUp(KeyCode.Z))
                    {
                        _btnClick.ButtonUnpressed();
                    }

                if (Input.GetKeyDown(KeyCode.X))
                    {
                        DeployUnit(UnitIndex); 
                    }
                else if (Input.GetKeyUp(KeyCode.X))
                {
                    _btnClick.ButtonUnpressed();
                }

        }
    }
}
