using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputProvider
{
    public Vector3 GetLookDirection();
    
    public Vector2 GetMovementInput();

    public bool GetCameraMove();
    
    public bool GetJumpInput();
}
