using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    private void Start()
    {
        SceneLoader.Instance.LoadLevel(_sceneName).Forget();
    }
}
