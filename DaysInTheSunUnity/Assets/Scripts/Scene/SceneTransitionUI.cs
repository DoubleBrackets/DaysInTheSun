using System.Collections;
using System.Collections.Generic;
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
}
