using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class BubbleOptionsView : DialogueViewBase
{
    [SerializeField]
    private OptionsBubble _optionsBubblePrefab;

    [SerializeField]
    private Transform _optionsContainer;

    [SerializeField]
    private float _responseTime;
    
    private List<OptionsBubble> _optionsBubbles = new List<OptionsBubble>();
    
    private Action<int> _onOptionSelected;
    
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private void Update()
    {
        // log currently selected game object

        Debug.Log(EventSystem.current.IsPointerOverGameObject());
    }

    private void Start()
    {
        DialogueService.Instance.AddView(this);
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        base.InterruptLine(dialogueLine, onDialogueLineFinished);
    }

    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        for(int i = 1; i < dialogueOptions.Length; i++)
        {
            var option = dialogueOptions[i];
            var optionsBubble = Instantiate(_optionsBubblePrefab, _optionsContainer);
            
            var text = option.Line.Text;
            
            optionsBubble.Setup(i, LineView.AddLineBreaks(text), _responseTime);
            
            OptionsTimer(_cancellationTokenSource.Token).Forget();
            
            _optionsBubbles.Add(optionsBubble);
            
            optionsBubble.OnSelected += OnOptionsSelected;
            _onOptionSelected = onOptionSelected;
        }
    }
    
    private void OnOptionsSelected(int index)
    {
        if(_onOptionSelected == null)
        {
            return;
        }
        
        foreach(var optionsBubble in _optionsBubbles)
        {
            optionsBubble.Dispose();
        }

        
        _onOptionSelected?.Invoke(index);
        
        _onOptionSelected = null;
        
        _optionsBubbles.Clear();
        
        _cancellationTokenSource.Cancel();
    }

    private async UniTaskVoid OptionsTimer(CancellationToken cancellationToken)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_responseTime), cancellationToken: cancellationToken);
            
            cancellationToken.ThrowIfCancellationRequested();
        
            OnOptionsSelected(0);
        }
        catch (Exception e)
        {
            throw;
        }

    }
}
