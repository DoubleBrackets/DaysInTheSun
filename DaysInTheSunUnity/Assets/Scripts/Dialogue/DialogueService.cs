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

            if (!_dialogueRunner.IsDialogueRunning && _dialogueQueue.Count > 0 && _dialogueRunner.dialogueViews.Length > 0)
            {
                Debug.Log("Starting queued dialogue: " + _dialogueQueue.Peek());
                _dialogueRunner.StartDialogue(_dialogueQueue.Dequeue());
            }
        }
    }

    public bool StartDialogue(string dialogueName)
    {
        Debug.Log("Queueing dialogue: " + dialogueName);
        _dialogueQueue.Enqueue(dialogueName);
        return true;
    }

    public void AddView(DialogueViewBase view)
    {
        var views = _dialogueRunner.dialogueViews.ToList();
        views.Add(view);
        _dialogueRunner.SetDialogueViews(views.ToArray());
    }
    
    public void RemoveView(DialogueViewBase view)
    {
        var views = _dialogueRunner.dialogueViews.ToList();
        views.Remove(view);
        _dialogueRunner.SetDialogueViews(views.ToArray());
    }
    
    public void EndDialogue()
    {
        _dialogueRunner.Stop();
    }
}
