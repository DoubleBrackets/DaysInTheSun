using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class DialogueService : MonoBehaviour
{
    public static DialogueService Instance { get; private set; }
    
    [SerializeField]
    private DialogueRunner _dialogueRunner;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
    
    public bool StartDialogue(string dialogueName)
    {
        if (_dialogueRunner.IsDialogueRunning)
        {
            return false;
        }
        _dialogueRunner.StartDialogue(dialogueName);
        return true;
    }

    public void AddView(DialogueViewBase view)
    {
        var views = _dialogueRunner.dialogueViews.ToList();
        views.Add(view);
        _dialogueRunner.SetDialogueViews(views.ToArray());
    }
}
