using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Player
{
    
    void Update()
    {
        //! není implementovaný cooldown
        PressKey(KeyCode.Keypad1, () => DeployUnit(_unitIndex));  
    }
}
