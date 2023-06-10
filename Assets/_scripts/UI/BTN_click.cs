using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BTN_click : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

#region Variables

[SerializeField] Image _buttonImage;
[SerializeField] Sprite _pressedImg, _unPressedImg;
[SerializeField] AudioSource _myAudioSource;
[SerializeField] AudioClip _clickSound;

#endregion


    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed();
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonUnpressed();
    }



    public void ButtonPressed()
    {
        _buttonImage.sprite = _pressedImg;
        _myAudioSource.PlayOneShot(_clickSound);
    }
 
    public void ButtonUnpressed()
    {
        _buttonImage.sprite = _unPressedImg;
    }
}
