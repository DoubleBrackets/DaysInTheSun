using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneChangeZone : MonoBehaviour
{
    [SerializeField]
    private LocationSO _locationSO;

    private BoxCollider _boxCollider;
    
    private void OnValidate()
    {
#if UNITY_EDITOR
        _boxCollider = GetComponent<BoxCollider>();
#endif
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader.Instance.LoadLevel(_locationSO);
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_boxCollider.center, _boxCollider.bounds.size);
            Gizmos.matrix = Matrix4x4.identity;
#if UNITY_EDITOR
            Handles.Label( transform.position + Vector3.up * 2, _locationSO.SceneName);
#endif
        }
    }
}
