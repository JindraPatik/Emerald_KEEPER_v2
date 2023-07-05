using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTimer : MonoBehaviour
{
    [SerializeField] int _counterTime;
    [SerializeField] TMP_Text _counterText;
    private int _counterDecreaser = 1;

    private void Awake()
    {
        _counterText.enabled = true;
        StartCountdown();
    }

    private void Start()
    {
        
    }

    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }
    
    private IEnumerator CountdownCoroutine() 
    { 
        while (_counterTime > 0) 
        { 
            _counterText.text = _counterTime.ToString();
            yield return new WaitForSeconds(1f);
            _counterTime -= _counterDecreaser;
        }
            

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        if (_counterTime == 0) 
        {
            _counterText.text = ("FIGHT!");
            yield return new WaitForSeconds(1f);
        }
        _counterText.enabled = false;
        GameManager.Instance.GameIsPaused = false;
    }

}
