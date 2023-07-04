using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HumanPlayer : Player
{
    private BTN_click _btnClick;
    private BTN_cooldown _btnCooldown;
    KeyboardCooldown _keyboardCooldown;
    [SerializeField] List<GameObject> _buttonObjects;
    [SerializeField] List<TMP_Text> _btnPrices, _btnStrenghts;
    private Unit _myUnit;
    public static HumanPlayer HumanPlayerInstance;
    


    public override void Awake() 
    {
        base.Awake();
        HumanPlayerInstance = this;
        _keyboardCooldown = GetComponent<KeyboardCooldown>();
        _keyboardCooldown.CooldownDisable();


        for (int i = 0; i < _buttonObjects.Count; i++)
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
        Debug.Log("my faction: " + PlayerFaction + "enemy targets count: " + Player.PlayerInstance.FlyAttackersTargets.Count);

        if(!_btnCooldown.IsCooldown && !GameManager.Instance.GameIsPaused)
            {
            
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Z");
                    SendUnit(0);
                }
                else if (Input.GetKeyUp(KeyCode.Z))
                {
                    _btnClick = _buttonObjects[0].GetComponent<BTN_click>();
                    _btnClick.ButtonUnpressed();
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    Debug.Log("X");
                    SendUnit(1);
                }
                else if (Input.GetKeyUp(KeyCode.X))
                {
                    _btnClick = _buttonObjects[1].GetComponent<BTN_click>();
                    _btnClick.ButtonUnpressed();
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Debug.Log("C");
                    SendUnit(2);
                }
                else if (Input.GetKeyUp(KeyCode.C))
                {
                    _btnClick = _buttonObjects[2].GetComponent<BTN_click>();
                    _btnClick.ButtonUnpressed();
                }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                if (Input.GetKeyDown(KeyCode.A))
                {
                    SendUnit(3);
                }
                else if (Input.GetKeyUp(KeyCode.A))
                {
                    _btnClick = _buttonObjects[3].GetComponent<BTN_click>();
                    _btnClick.ButtonUnpressed();
                }
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //_btnClick = _buttonObjects[4].GetComponent<BTN_click>();
                    _hoomingMissile.LaunchRocket();
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    //_btnClick = _buttonObjects[4].GetComponent<BTN_click>();
                    //_btnClick.ButtonUnpressed();
            }

=======
=======
>>>>>>> parent of 118f54e (Raketka letÃ­ nahoru)
=======
>>>>>>> parent of 118f54e (Raketka letÃ­ nahoru)
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("C");
                SendUnit(3);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _btnClick = _buttonObjects[3].GetComponent<BTN_click>();
                _btnClick.ButtonUnpressed();
            }
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 118f54e (Raketka letÃ­ nahoru)
=======
>>>>>>> parent of 118f54e (Raketka letÃ­ nahoru)
=======
>>>>>>> parent of 118f54e (Raketka letÃ­ nahoru)

        }
    }

    private void SendUnit(int unitindex)
    {
        if (_keyboardCooldown.IsKeyboardInputEnabled)
        {
            DeployUnit(unitindex);
            _keyboardCooldown.StartCooldown();
            _btnClick = _buttonObjects[unitindex].GetComponent<BTN_click>();
            _btnClick.ButtonPressed();

            _btnCooldown = _buttonObjects[unitindex].GetComponent<BTN_cooldown>();
            _btnCooldown.CooldownEnable();
        }
    }

    
}
