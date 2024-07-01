using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomTV : MonoBehaviour, IDataPersistence
{
    int _timesToStatic = 0;

    Animator _animator;

    bool _staticPlayed;
    bool _staticPlaying;

    bool _timeSetToStatic;
    float _timeToStatic = 15;
    float _timerToStatic;

    bool _staticTimeSet;
    float _staticTime;
    float _staticTimer;

    AudioSource _audioSource;
        
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_timesToStatic > 0)
            _timesToStatic--;

        SetTextWhileNotPlaying();
                      
    }

    public void SaveData(ref GameData data)
    {
        data.timesToStatic = _timesToStatic;
    }

    public void LoadData(GameData data)
    {
        _timesToStatic = data.timesToStatic;
    }
       

    void Update()
    {
        Debug.Log(_timesToStatic);

        if (LightControl.LightsOut) return;

        PlayStatic();
        StaticTimerWhilePlaying();
        StopStatic();
        StopStaticIfMustHide();

        if (_timesToStatic > 0) return;

        StaticTimerStart();
     

    }



    void StaticTimerStart()
    {
        if (ScenesInGame.Instance.GetSecondParentsRoomScenePlayed() && !_staticPlayed)
        {
            if (!_timeSetToStatic)
            {
                _timeToStatic = Random.Range(2, 4);
                _timeSetToStatic = true;
                Debug.Log(_timeToStatic);

            }

            _timerToStatic += Time.deltaTime;           
        }
    }

    void PlayStatic()
    {
        if (!_staticPlaying)
        {
            if (_timerToStatic >= _timeToStatic)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.Play();

                _animator.SetBool("static", true);
                _staticPlayed = true;
                _staticPlaying = true;

                _timerToStatic = 0;

                _timesToStatic = Random.Range(2, 5);

                SetTextWhilePlaying();
            }                    
        }
    }

    void StaticTimerWhilePlaying()
    {
        if (!_staticPlaying) return;

        if (!_staticTimeSet)
        {
            _staticTime = Random.Range(4, 8);
            _staticTimeSet = true;
        }

        _staticTimer += Time.deltaTime;
    }

    void StopStatic()
    {
        if (!_staticPlaying) return;

        if (_staticTimer >= _staticTime)
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();

            _animator.SetBool("static", false);
            _staticPlaying = false;
                        
            SetTextWhileNotPlaying();
        }
    }

    void StopStaticIfMustHide()
    {
        if (PlayerController.Instance.GetMustHide() && _staticPlaying)
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();

            _animator.SetBool("static", false);
            _staticPlaying = false;

            _timesToStatic = Random.Range(2, 5);

            SetTextWhileNotPlaying();
        }
    }

    void SetTextWhileNotPlaying()
    {
        if (LanguageManager.Instance.Language == "es")
        {
            GetComponent<ClickableObject>().TurnOnText = "Ahora no.";
            GetComponent<ClickableObject>().TurnOffText = "Ya se encuentra apagado.";
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            GetComponent<ClickableObject>().TurnOnText = "Not now.";
            GetComponent<ClickableObject>().TurnOffText = "It's already off.";
        }
    }

    void SetTextWhilePlaying()
    {
        if (LanguageManager.Instance.Language == "es")
        {
            GetComponent<ClickableObject>().TurnOnText = "Ya está encendido.";
            GetComponent<ClickableObject>().TurnOffText = "No puedo apagarlo.";
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            GetComponent<ClickableObject>().TurnOnText = "It's already on.";
            GetComponent<ClickableObject>().TurnOffText = "I can't turn it off.";
        }
    }
}
