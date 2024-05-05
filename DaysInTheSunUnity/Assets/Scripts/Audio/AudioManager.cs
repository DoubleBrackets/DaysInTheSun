using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Footstep,
        FriendChirp,
        ProtagChirp
    }
    
    [System.Serializable]
    public struct SFX
    {
        public SoundType soundType;
        public List<AudioClip> clips;
    }
    
    [SerializeField]
    private List<SFX> _sfxList;

    public static AudioManager Instance { get; set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(SoundType soundType, Vector3 pos)
    {
        var sfx = _sfxList.Find(s => s.soundType == soundType);

        if (sfx.clips == null || sfx.clips.Count == 0)
        {
            return;
        }
        var clip = sfx.clips[Random.Range(0, sfx.clips.Count)];
        
        if (clip)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }
}
