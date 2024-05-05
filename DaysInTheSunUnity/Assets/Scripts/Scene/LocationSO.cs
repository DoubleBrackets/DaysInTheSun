using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationSO", menuName = "LocationSO", order = 51)]
public class LocationSO : ScriptableObject
{
    [field: SerializeField]
    public string SceneName { get; private set; }
    
    [field: SerializeField, TextArea]
    public string TitleText { get; private set; }
    
    [field: SerializeField]
    public AudioClip BackgroundMusic { get; private set; }
    
    [field: SerializeField]
    public float BackgroundMusicVolume { get; private set; }
    
    [field: SerializeField]
    public AudioClip AmbientSound { get; private set; }
    
    [field: SerializeField]
    public float AmbientSoundVolume { get; private set; }
}
