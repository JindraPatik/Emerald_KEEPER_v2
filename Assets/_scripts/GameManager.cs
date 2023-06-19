using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] Canvas InGamemenuCanvas;
    private bool _menuIsActive;
    #endregion

    private void Awake()
    {
        HidePauseGameMenu();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_menuIsActive) 
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _menuIsActive)
        {
            UnpauseGame();
        }
    }

    private void UnpauseGame()
    {
        HidePauseGameMenu();
    }

    #region Methods
    public void PauseGame()
    {
        ShowPauseGameMenu();
    }

    private void ShowPauseGameMenu()
    {
        InGamemenuCanvas.gameObject.SetActive(true);
        _menuIsActive = true;
    }
    private void HidePauseGameMenu()
    {
        InGamemenuCanvas.gameObject.SetActive(false);
        _menuIsActive = false;
    }
    #endregion
}
