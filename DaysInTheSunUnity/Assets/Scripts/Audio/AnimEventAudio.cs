using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventAudio : MonoBehaviour
{
    public void PlaySound(int sfx)
    {
        AudioManager.Instance.PlaySound((AudioManager.SoundType)sfx, transform.position);
    }
}
