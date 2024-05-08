using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour, IPointerClickHandler, IDataPersistence
{

    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    bool _firstLightStatusCheck;
    [SerializeField] bool _firstTimeStatus;

    [SerializeField] GameObject _lightSwitch;
    [SerializeField] GameObject _rooflight;

    [SerializeField] Sprite _rooflightOn;
    [SerializeField] Sprite _rooflightOff;

    [SerializeField] Sprite _lightSwitchUp;
    [SerializeField] Sprite _lightSwitchDown;
    [SerializeField] Sprite _lightSwitchBack;

    [SerializeField] bool _isOn;

    [SerializeField] bool _isRooflight;
    [SerializeField] bool _isFrontSwitch;

    [SerializeField] float _volume;

    [SerializeField] AudioClip _turnOnAudio;
    [SerializeField] AudioClip _turnOffAudio;
    AudioSource _audioSource;

    bool _soundPlayed = true;

    string _currentAction;
       
    Light2D _light;

    [SerializeField] bool _isFan;

    private void Awake()
    {
        _light = GetComponentInChildren<Light2D>();              
        _audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetUpSwitch();  
        FirstTimeStatus();
    }
    public void LoadData(GameData data)
    {        
        data.lightsOn.TryGetValue(_id, out _isOn);

        _light.enabled = _isOn;

        data.firstLightsStatusCheck.TryGetValue(_id, out _firstLightStatusCheck); //trata de conseguir el valor en la id indicada y lo vuelca en la variable indicada en out.
                    
    }

    public void SaveData(ref GameData data)
    {
        if (data.lightsOn.ContainsKey(_id))
        {
            data.lightsOn.Remove(_id);
        }
        data.lightsOn.Add(_id, _isOn);

        if(data.firstLightsStatusCheck.ContainsKey(_id))
        {
            data.firstLightsStatusCheck.Remove(_id);
        }
        data.firstLightsStatusCheck.Add(_id, _firstLightStatusCheck);
    }    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;

        ChangeLightStatus();
        RoofLightAndLightSwitchStatus();
        SwitchSound();
    }

    void SetUpSwitch()
    {
        if(_isRooflight)
        {
            if (_isFrontSwitch)
            {
                if (_isOn) { _lightSwitch.GetComponent<SpriteRenderer>().sprite = _lightSwitchUp; }
                else { _lightSwitch.GetComponent<SpriteRenderer>().sprite = _lightSwitchDown; }
            }
            else
                _lightSwitch.GetComponent<SpriteRenderer>().sprite = _lightSwitchBack;
        }
    }

    void FirstTimeStatus()
    {
        if (!_firstLightStatusCheck)
        {
            _isOn = _firstTimeStatus;
            _light.enabled = _firstTimeStatus;
            _firstLightStatusCheck = true;
        }
    }

    void ChangeLightStatus()
    {
        _currentAction = ButtonsGrid.Instance.GetCurrentAction();

        if (_currentAction == "Turn On" || _currentAction == "Encender")
        {
            if (_isOn)
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already on.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                else if(LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está encendida.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            }
            else
            {
                _isOn = true;
                _soundPlayed = false;
            }
        }
        else if(_currentAction == "Turn Off" || _currentAction == "Apagar")
        {
            if(!_isOn)
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already off.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está apagada.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            }
            else
            {
                _isOn = false;
                _soundPlayed = false;
            }
        }

        _light.enabled = _isOn;
    }

    void SwitchSound()
    {
        if (_soundPlayed) return;

        if (_isOn)
        {
            _audioSource.PlayOneShot(_turnOnAudio, _volume);
            _soundPlayed = true;
        }
        else if (!_isOn)
        {
            _audioSource.PlayOneShot(_turnOffAudio, _volume);
            _soundPlayed = true;
        }
    }

    void RoofLightAndLightSwitchStatus()
    {
        if (_isRooflight)
        {
            if (_isFrontSwitch)
            {
                if (_isOn) { _lightSwitch.GetComponent<SpriteRenderer>().sprite = _lightSwitchUp; }
                else { _lightSwitch.GetComponent<SpriteRenderer>().sprite = _lightSwitchDown; }
            }

            if(!_isFan)
            {
                if (_isOn) { _rooflight.GetComponent<SpriteRenderer>().sprite = _rooflightOn; }
                else { _rooflight.GetComponent<SpriteRenderer>().sprite = _rooflightOff; }
            }
           
        }
    }

}
