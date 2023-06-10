using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class CameraController : MonoBehaviour
{
    [SerializeField] float _cameraMoveSpeed = 0.5f;

    private bool _isDragging = false;
    private Vector2 _dragStartPosition;
    private float _currentRotation;
    private Quaternion _defaultRotation;
    private Quaternion _currentCameraRotation;
    private float _current = 0f;
    private float _endTime = 1f; 
    private bool _cameraIsReturningToDefaultPos;
    [SerializeField] float _cameraReturnSpeed;
    [SerializeField] Slider _zoom;
    //[SerializeField] float _zoomMultiplier = 2f;

    private void Awake() 
    {
        _defaultRotation = transform.rotation;
        Vector3 _defaultCameraPosition = transform.position;
    }


    void Update()
    {
        ZoomCamera();
        
        if (Input.touchCount > 0) // Zjisti jestli nejaky dotek
        {
            Touch touch = Input.GetTouch(0); // reference na obrazovku

            if ( touch.phase == TouchPhase.Began )
            {

                if ( touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2) // detekce v prave casti obrazovky
                {
                    _cameraIsReturningToDefaultPos = false;
                    _isDragging = true;
                    _dragStartPosition = touch.position;
                }
            }
            else if ( touch.phase == TouchPhase.Moved )
            {
                if (_isDragging)
                {
                    float dragDelta = touch.position.x - _dragStartPosition.x;
                    float limitedRotation = Mathf.Clamp(dragDelta, -1f, 1f);
                    float cameraRotation = limitedRotation * _cameraMoveSpeed * Time.deltaTime;
                    Mathf.Clamp(cameraRotation, -5f, 5f);
                    transform.Rotate(Vector3.up, cameraRotation);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                {
                    _currentCameraRotation = transform.rotation;
                    _cameraIsReturningToDefaultPos = true;
                    _isDragging = false;
                }

            }
            
        }
            if (_cameraIsReturningToDefaultPos)
            {
                    if (_current >= _endTime)
                        {
                            _cameraIsReturningToDefaultPos = false;  
                            _current = 0f; 
                        }
                    else
                    {
                        _current = Mathf.MoveTowards(_current, _endTime, _cameraReturnSpeed * Time.deltaTime);
                        transform.rotation = Quaternion.Lerp(_currentCameraRotation, _defaultRotation, _current);
                    }
            }

            

    }

    private void ZoomCamera()
    {
        
    }

    
}
