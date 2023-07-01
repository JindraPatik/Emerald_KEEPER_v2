using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HumanPlayer : Player
{
    private BTN_click _btnClick;
    private BTN_cooldown _btnCooldown;
    KeyboardCooldown _keyboardCooldown;
    [SerializeField] GameObject[] _buttonObjects;
    [SerializeField] List<TMP_Text> _btnPrices, _btnStrenghts;
    private Unit _myUnit;
    public static HumanPlayer HumanPlayerInstance;


    public override void Awake() 
    {
        base.Awake();
        HumanPlayerInstance = this;
        _keyboardCooldown = GetComponent<KeyboardCooldown>();
        _keyboardCooldown.CooldownDisable();


        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            GameObject buttonObject = _buttonObjects[i];
            TMP_Text btnPrice = _btnPrices[i];
            TMP_Text btnStrenght = _btnStrenghts[i];

            _btnClick = buttonObject.GetComponent<BTN_click>();
            _btnCooldown = buttonObject.GetComponent<BTN_cooldown>();

            // Naètení Unit pro každé tlaèítko
            _myUnit = Prefabs[i].GetComponent<Unit>();

            // Pøiøazení hodnot pro tlaèítko
            btnPrice.text = ((int)_myUnit.Price).ToString();
            btnStrenght.text = ((int)_myUnit.Strenght).ToString();

        }
    }

    public override void Start() 
        {
            base.Start();
        }
    
    
    public void Update()
    {
        if(_btnCooldown.IsCooldown != null && !_btnCooldown.IsCooldown && !GameManager.Instance.GameIsPaused)
            {
            if (_keyboardCooldown.IsKeyboardInputEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Z zmackle");
                    DeployUnit(0);
                    _keyboardCooldown.StartCooldown();
                    _btnClick.ButtonPressed();
                    _btnCooldown.CooldownEnable();
                }
                else if (Input.GetKeyUp(KeyCode.Z))
                {
                    _btnClick.ButtonUnpressed();
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    DeployUnit(1);
                    _keyboardCooldown.StartCooldown();
                    _btnClick.ButtonPressed();
                    _btnCooldown.CooldownEnable();
                }
                else if (Input.GetKeyUp(KeyCode.X))
                {
                    _btnClick.ButtonUnpressed();
                } 
            }

        }
    }
}
