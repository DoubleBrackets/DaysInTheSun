using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    
    [SerializeField]
    private SceneTransitionUI _sceneTransitionUI;
    
    [SerializeField]
    private AudioSource _backgroundMusicAudioSource;
    
    [SerializeField]
    private AudioSource _ambientSoundAudioSource;
    
    private string currentLevel = String.Empty;

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
#if UNITY_EDITOR
        if(SceneManager.sceneCount > 1) 
            currentLevel = SceneManager.GetSceneAt(1).name;
#endif
    }

    public async UniTask LoadLevel(LocationSO location)
    {
        DialogueService.Instance.EndDialogue();
        
        await _sceneTransitionUI.FadeOut();
        
        string sceneName = location.SceneName;
        
        if (SceneManager.GetSceneByName(currentLevel).isLoaded)
        {
            await SceneManager.UnloadSceneAsync(currentLevel);
        }
        
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentLevel = sceneName;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        
        _sceneTransitionUI.SetTitleText(location.TitleText);
        
        if (location.BackgroundMusic != null)
        {
            _backgroundMusicAudioSource.clip = location.BackgroundMusic;
            _backgroundMusicAudioSource.Play();
        }
        else
        {
            _backgroundMusicAudioSource.Stop();
        
        }
        
        if (location.AmbientSound != null)
        {
            _ambientSoundAudioSource.clip = location.AmbientSound;
            _ambientSoundAudioSource.Play();
        }
        else
        {
            _ambientSoundAudioSource.Stop();
        }
        
        await _sceneTransitionUI.FadeIn();
    }
}