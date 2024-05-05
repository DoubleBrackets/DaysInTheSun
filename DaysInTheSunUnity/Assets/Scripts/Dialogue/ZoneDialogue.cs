using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDialogue : MonoBehaviour
{
    [SerializeField]
    private string _dialogueNodeName;
    
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
}
