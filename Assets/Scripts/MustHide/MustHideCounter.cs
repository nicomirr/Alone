using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustHideCounter : MonoBehaviour, IDataPersistence
{    
    public static float timer;
    int _time = 180;
    AudioSource _audioSourceHorror;
    bool _horrorAudioEnabled;
    bool _corroutineStarted;


    private void Awake()
    {       
        _audioSourceHorror = GameObject.Find("AudioSourceHorror").GetComponent<AudioSource>();  
    }

    public void LoadData(GameData data)
    {
        timer = data.timer;
        _horrorAudioEnabled = data.horrorAudioEnabledMustHideCounter;
    }

    public void SaveData(ref GameData data)
    {
        data.timer = timer;
        data.horrorAudioEnabledMustHideCounter = _horrorAudioEnabled;

    }


    private void Update()
    {        
        if (Pause.Instance.IsPaused) return;
        if (ScenesInGame.Instance.GetSecondDinningroomScenePlayed()) return;
        if (!ScenesInGame.Instance.GetFirstLivingroomScenePlayed()) return;
        if (ScenesInGame.Instance.GetIsFlashback()) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;
        if (PlayerController.Instance.GetMustHide()) return;
        if (PlayerController.Instance.GetIsHidding()) return;

        Timer();
               
        if(timer >= _time && !_corroutineStarted)
        {
            StartCoroutine(StartCountdown());
            _corroutineStarted = true;
        }
               
    }

    void Timer()
    {
        if (Pause.Instance.IsPaused || ScenesInGame.Instance.GetSceneIsPlaying() || PlayerController.Instance.GetIsInteractingWithEnviroment()) return;

        if(timer < _time)
            timer += Time.deltaTime;
    }

    IEnumerator StartCountdown()
    {        
        if(!_horrorAudioEnabled) 
        {
            _audioSourceHorror.volume = 1f * GameVolume.Instance.CurrentVolume();
            _audioSourceHorror.Play();
            AudioSourceHorror.Instance.IsPlaying = true;
            _horrorAudioEnabled = true;
        }

        int num = 0;
        
        if(!Pause.Instance.IsPaused && !ScenesInGame.Instance.GetSceneIsPlaying() && !PlayerController.Instance.GetIsInteractingWithEnviroment())
            num = Random.Range(0, 51);

        if (num == 25)
        {            
            _time = Random.Range(180, 241);
            timer = 0;
            PlayerController.Instance.SetMustHide(true);
            _horrorAudioEnabled = false;
            _corroutineStarted = false;
            StopCoroutine(StartCountdown());
        }
        else 
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(StartCountdown());
        }        
    }   
}

