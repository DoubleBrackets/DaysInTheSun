using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTarget;
    
    [SerializeField]
    private GameObject _inputProviderContainer;
    
    [SerializeField]
    private Vector2 _sensitivity;

    [SerializeField]
    private float _lerpFactor;
    
    [SerializeField]
    private Vector2 _lookLimits;
    
    private float _xRotation;
    private float _yRotation;
    
    private float _currentXRotation;
    private float _currentYRotation;
    
    private UserInputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = UserInputProvider.Instance;
        
    }

    private void LateUpdate()
    {
        var input = _inputProvider.GetMouseDelta();

        if (!_inputProvider.GetCameraMove())
        {
            Cursor.lockState = CursorLockMode.None;
            input = Vector2.zero;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        _xRotation -= input.y * _sensitivity.y;
        _yRotation += input.x * _sensitivity.x;
        
        _xRotation = Mathf.Clamp(_xRotation, _lookLimits.x, _lookLimits.y);
        
        float t = 1 - Mathf.Pow(1 - _lerpFactor, Time.deltaTime * 10);
        _currentXRotation = Mathf.Lerp(_currentXRotation, _xRotation, t);
        _currentYRotation = Mathf.Lerp(_currentYRotation, _yRotation, t);
        
        _cameraTarget.localRotation = Quaternion.Euler(_currentXRotation, _currentYRotation, 0);
    }
}
