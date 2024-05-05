using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [SerializeField]
    private BootupConfigSO _bootupConfigSO;

    private void Start()
    {
        SceneLoader.Instance.LoadLevel(_bootupConfigSO.InitialLocation).Forget();
    }
}
