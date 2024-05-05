using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BootupConfigSO", menuName = "BootupConfigSO", order = 51)]
public class BootupConfigSO : ScriptableObject
{
    [field: SerializeField]
    public LocationSO InitialLocation { get; private set; }
}
