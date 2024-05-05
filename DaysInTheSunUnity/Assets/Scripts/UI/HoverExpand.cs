using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HoverExpand : MonoBehaviour
{
    public void Expand()
    {
        transform.DOScale(1.1f, 0.1f);
    }
    
    public void Shrink()
    {
        transform.DOScale(1f, 0.1f);
    }
}
