using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZoneDialogue : MonoBehaviour
{
    [SerializeField]
    private string _dialogueNodeName;

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
            bool didStart = DialogueService.Instance.StartDialogue(_dialogueNodeName);
            if (didStart)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_boxCollider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_boxCollider.center, _boxCollider.bounds.size);
            Gizmos.matrix = Matrix4x4.identity;
            #if UNITY_EDITOR
            Handles.Label( transform.position + Vector3.up * 2, _dialogueNodeName);
            #endif
        }
    }
}
