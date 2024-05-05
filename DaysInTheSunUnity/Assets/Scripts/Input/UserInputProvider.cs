using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UserInputProvider : MonoBehaviour, IInputProvider
{
    
    public static UserInputProvider Instance { get; private set; }
    
    private bool _jumpPressed;
    private Vector2 _movementInput;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            _jumpPressed = true;
        }
        else
        {
            _jumpPressed = false;
        }
        
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        _movementInput = new Vector2(horizontal, vertical);
        
    }

    public Vector3 GetLookDirection()
    {
        return Camera.main.transform.forward;
    }

    public Vector2 GetMovementInput()
    {
        return _movementInput;
    }

    public bool GetCameraMove()
    {
        return Input.GetMouseButton(1);
    }

    public bool GetJumpInput()
    {
        return _jumpPressed;
    }
    
    public Vector2 GetMouseDelta()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
