using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBinder : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;
    
    void Start()
    {
        _canvas.worldCamera = Camera.main;
    }
}
