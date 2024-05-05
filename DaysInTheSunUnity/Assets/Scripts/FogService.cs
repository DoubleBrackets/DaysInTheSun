using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogService : MonoBehaviour
{
    public static FogService Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        RenderSettings.fog = true;
    }
    
    public void SetFogColor(Color color)
    {
        RenderSettings.fogColor = color;
    }
    
    public void SetFogDensity(float density)
    {
        RenderSettings.fogDensity = density;
    }
}
