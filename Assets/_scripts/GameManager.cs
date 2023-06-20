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
    private CrystalSpawner _crystalSpawner;
    private bool _gameIsPaused;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public bool GameIsPaused
    {
        get { return _gameIsPaused; }
        set { _gameIsPaused = value; }
    }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;

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

    #region Methods
    private void UnpauseGame()
    {
        HidePauseGameMenu();
        _gameIsPaused = false;
    }
    public void PauseGame()
    {
        ShowPauseGameMenu();
        _gameIsPaused = true;
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
