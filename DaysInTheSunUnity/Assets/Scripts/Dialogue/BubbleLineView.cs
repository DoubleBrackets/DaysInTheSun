using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Yarn.Markup;
using Yarn.Unity;

public class BubbleLineView : DialogueViewBase
{
    [SerializeField]
    private string _characterName;
    
    [SerializeField]
    private AudioManager.SoundType _voiceType;
    
    [SerializeField]
    private TMP_Text _tmpText;
    
    [SerializeField]
    private Button _nextButton;
    
    [SerializeField]
    private float _holdTimePerWord;
    
    [SerializeField]
    private float _minHoldTime;

    [SerializeField]
    private float _fadeTime;
    
    [SerializeField]
    private CanvasGroup _bubbleCanvasGroup;
    
    private Action _onDialogueLineFinished;
    
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    
    private Tween _bubbleTween;

    private void Start()
    {
        Hide();
        DialogueService.Instance.AddView(this);
        _bubbleCanvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        DialogueService.Instance.RemoveView(this);
    }

    public override void DialogueStarted()
    {
        // Debug.Log("Dialogue Started");
    }

    public override void DialogueComplete()
    {
        // Debug.Log("Dialogue Complete");
    }

    public override void DismissLine(Action onDismissalComplete)
    {
        // Debug.Log("Dismiss Line");
        onDismissalComplete();
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        // Debug.Log("Interrupt Line");
        _onDialogueLineFinished = onDialogueLineFinished;
        _onDialogueLineFinished();
        Hide();
        _cancellationTokenSource.Cancel();
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        // Debug.Log("Run Line");

        string speakerName = dialogueLine.CharacterName;

        if (!speakerName.Equals(_characterName))
        {
            onDialogueLineFinished();
            return;
        }
        
        MarkupParseResult text = dialogueLine.TextWithoutCharacterName;

        string displayText = LineView.AddLineBreaks(text);
        _tmpText.text = displayText;

        Show();
        
        _onDialogueLineFinished = onDialogueLineFinished;
        
        HoldText(displayText, _cancellationTokenSource.Token).Forget();
        
        _nextButton.onClick.AddListener(OnNextButtonClicked);
        
        AudioManager.Instance.PlaySound(_voiceType, transform.position);
    }

    private async UniTaskVoid HoldText(string text, CancellationToken token)
    {
        try
        {
            int words = text.Split(' ').Length;
            float holdTime = words * _holdTimePerWord;
            holdTime = Mathf.Max(holdTime, _minHoldTime);
            _bubbleCanvasGroup.DOFade(0f, _fadeTime).SetDelay(holdTime - _fadeTime);
            
            await UniTask.Delay(TimeSpan.FromSeconds(holdTime) , cancellationToken: token);
            
            token.ThrowIfCancellationRequested();
            
            Hide();
            _onDialogueLineFinished();
        }
        catch (Exception e)
        {
            return;
        }
    }

    private void OnNextButtonClicked()
    {
        Hide();
        _nextButton.onClick.RemoveListener(OnNextButtonClicked);
        _cancellationTokenSource.Cancel();
        _onDialogueLineFinished();
    }

    private void Hide()
    {
        _bubbleCanvasGroup.DOFade(0f, _fadeTime);
        _bubbleCanvasGroup.interactable = false;
    }

    private void Show()
    {
        _bubbleCanvasGroup.DOFade(1f, _fadeTime);
        _bubbleCanvasGroup.interactable = true;
    }
}
