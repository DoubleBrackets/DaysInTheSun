using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;

public class DialogueService : MonoBehaviour
{
    public static DialogueService Instance { get; private set; }
    
    [SerializeField]
    private DialogueRunner _dialogueRunner;
    
    private Queue<string> _dialogueQueue = new Queue<string>();
    
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
        
        StartDialogueFromQueue().Forget();
    }

    private async UniTaskVoid StartDialogueFromQueue()
    {
        while (true)
        {
            await UniTask.Yield();

            if (!_dialogueRunner.IsDialogueRunning && _dialogueQueue.Count > 0)
            {
                StartDialogue(_dialogueQueue.Dequeue());
            }
        }
    }

    public bool StartDialogue(string dialogueName)
    {
        if (_dialogueRunner.IsDialogueRunning)
        {
            Debug.Log("Queueing dialogue: " + dialogueName);
            _dialogueQueue.Enqueue(dialogueName);
            return true;
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
