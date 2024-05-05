using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleText;

    [SerializeField]
    private CanvasGroup _fadeOutCanvasGroup;
    
    [SerializeField]
    private CanvasGroup _fadeInCanvasGroup;
    
    public void SetTitleText(string text)
    {
        _titleText.text = text;
    }
    
    public async UniTask FadeOut()
    {
        _fadeOutCanvasGroup.DOFade(1, 2f);
        await UniTask.Delay(2000);
        _fadeOutCanvasGroup.alpha = 1;
    }
    
    public async UniTask FadeIn()
    {
        _fadeInCanvasGroup.alpha = 1;
        _fadeOutCanvasGroup.alpha = 0;
        await UniTask.Delay(3000);

        _fadeInCanvasGroup.DOFade(0, 2f);
        await UniTask.Delay(2000);
        _fadeInCanvasGroup.alpha = 0;
    }
}
