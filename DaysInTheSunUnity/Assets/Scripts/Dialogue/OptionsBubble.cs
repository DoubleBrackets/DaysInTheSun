using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBubble : MonoBehaviour
{
    [SerializeField]
    private Button _optionButtonPrefab;
    
    [SerializeField]
    private CanvasGroup _canvasGroup;
    
    [SerializeField]
    private TMP_Text _optionText;

    [SerializeField]
    private float _fadeTime;
    
    private int _index;
    
    public event Action<int> OnSelected;
    
    private float _responseTime;

    public void Setup(int index, string text, float responseTime)
    {
        _index = index;
        _responseTime = responseTime;
        _optionButtonPrefab.onClick.AddListener(OnButtonClicked);
        _optionText.text = text;
        _canvasGroup.alpha = 0f;
        _canvasGroup.DOFade(1f, _fadeTime).OnComplete(StartFade);
    }

    private void StartFade()
    {
        _canvasGroup.DOFade(0f, _responseTime - _fadeTime).SetEase(Ease.InQuad);
    }

    private void OnButtonClicked()
    {
        OnSelected?.Invoke(_index);
    }

    public void Dispose()
    {
        _canvasGroup.DOFade(0f, _fadeTime).OnComplete(Destroy);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
