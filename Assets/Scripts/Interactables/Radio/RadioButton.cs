using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadioButton : MonoBehaviour, IDataPersistence
{
    [SerializeField] Button _dial1Button;
    [SerializeField] Button _dial3Button;

    [SerializeField] Button _backButton;

    bool _buttonPressed;

    [SerializeField] Image _redButton;
    [SerializeField] Sprite _redButtonUnpressed;
    [SerializeField] Sprite _redButtonPressed;

    [SerializeField] GameObject _dial1;
    [SerializeField] GameObject _dial2;
    [SerializeField] GameObject _dial3;

    int _dial1Value;
    int _dial2Value;
    int _dial3Value;

    Vector3 _dial1Rotation;
    Vector3 _dial2Rotation;
    Vector3 _dial3Rotation;

    int _dial1CorrectValue = 7;
    int _dial2CorrectValue = 1;
    int _dial3CorrectValue = 3;

    [SerializeField] AudioClip _staticNoise;
    [SerializeField] AudioClip _pianoMusic;
    [SerializeField] AudioClip _buttonPress;

    [SerializeField] TextMeshProUGUI _text;

    public void LoadData(GameData data)
    {        
        _dial1Rotation = data.dial1Rotation;
        _dial2Rotation= data.dial2Rotation;
        _dial3Rotation = data.dial3Rotation;
    }

    public void SaveData(ref GameData data)
    {        
        data.dial1Rotation = _dial1Rotation;
        data.dial2Rotation = _dial2Rotation;
        data.dial3Rotation = _dial3Rotation;
    }

    private void Start()
    {
        _dial1.transform.eulerAngles = _dial1Rotation;
        _dial2.transform.eulerAngles = _dial2Rotation;
        _dial3.transform.eulerAngles = _dial3Rotation;

        if (ScenesInGame.Instance.GetIsFlashback())
            GetComponent<ClickableObject>().CanBeUsed = false;
        else
            GetComponent<ClickableObject>().CanBeUsed = true;
    }

    private void Update()
    {
        DialValues();
    }

    public void OnButtonPressed()
    {
        if(Pause.Instance.IsPaused) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;
        if (PlayerController.Instance.GetMustHide()) return;
        if (ScenesInGame.Instance.GetIsFlashback()) return;

        if (_buttonPressed) return;
        StartCoroutine(ButtonPressed());

        Debug.Log(_dial1.transform.rotation);
    }

    void DialValues()
    {
        Dial1();
        Dial2();
        Dial3();
    }

    void Dial1()
    {
        _dial1Rotation = _dial1.transform.eulerAngles;

        if (_dial1.transform.eulerAngles.z >= 0 && _dial1.transform.eulerAngles.z <= 1)
            _dial1Value = 0;
        else if (_dial1.transform.eulerAngles.z >= 323 && _dial1.transform.eulerAngles.z <= 325)
            _dial1Value = 1;
        else if (_dial1.transform.eulerAngles.z >= 287 && _dial1.transform.eulerAngles.z <= 289)
            _dial1Value = 2;
        else if (_dial1.transform.eulerAngles.z >= 251 && _dial1.transform.eulerAngles.z <= 253)
            _dial1Value = 3;
        else if (_dial1.transform.eulerAngles.z >= 215 && _dial1.transform.eulerAngles.z <= 217)
            _dial1Value = 4;
        else if (_dial1.transform.eulerAngles.z >= 179 && _dial1.transform.eulerAngles.z <= 181)
            _dial1Value = 5;
        else if (_dial1.transform.eulerAngles.z >= 143 && _dial1.transform.eulerAngles.z <= 145)
            _dial1Value = 6;
        else if (_dial1.transform.eulerAngles.z >= 107 && _dial1.transform.eulerAngles.z <= 109)
            _dial1Value = 7;
        else if (_dial1.transform.eulerAngles.z >= 71 && _dial1.transform.eulerAngles.z <= 73)
            _dial1Value = 8;
        else if (_dial1.transform.eulerAngles.z >= 36 && _dial1.transform.eulerAngles.z <= 37)
            _dial1Value = 9;
    }

    void Dial2()
    {
        _dial2Rotation = _dial2.transform.eulerAngles;

        if (_dial2.transform.eulerAngles.z >= 0 && _dial2.transform.eulerAngles.z <= 1)
            _dial2Value = 0;
        else if (_dial2.transform.eulerAngles.z >= 323 && _dial2.transform.eulerAngles.z <= 325)
            _dial2Value = 1;
        else if (_dial2.transform.eulerAngles.z >= 287 && _dial2.transform.eulerAngles.z <= 289)
            _dial2Value = 2;
        else if (_dial2.transform.eulerAngles.z >= 252 && _dial2.transform.eulerAngles.z <= 253)
            _dial2Value = 3;
        else if (_dial2.transform.eulerAngles.z >= 215 && _dial2.transform.eulerAngles.z <= 217)
            _dial2Value = 4;
        else if (_dial2.transform.eulerAngles.z >= 179 && _dial2.transform.eulerAngles.z <= 181)
            _dial2Value = 5;
        else if (_dial2.transform.eulerAngles.z >= 143 && _dial2.transform.eulerAngles.z <= 145)
            _dial2Value = 6;
        else if (_dial2.transform.eulerAngles.z >= 107 && _dial2.transform.eulerAngles.z <= 109)
            _dial2Value = 7;
        else if (_dial2.transform.eulerAngles.z >= 71 && _dial2.transform.eulerAngles.z <= 73)
            _dial2Value = 8;
        else if (_dial2.transform.eulerAngles.z >= 36 && _dial2.transform.eulerAngles.z <= 37)
            _dial2Value = 9;
    }

    void Dial3()
    {
        _dial3Rotation = _dial3.transform.eulerAngles;

        if (_dial3.transform.eulerAngles.z >= 0 && _dial3.transform.eulerAngles.z <= 1)
            _dial3Value = 0;
        else if (_dial3.transform.eulerAngles.z >= 323 && _dial3.transform.eulerAngles.z <= 325)
            _dial3Value = 1;
        else if (_dial3.transform.eulerAngles.z >= 287 && _dial3.transform.eulerAngles.z <= 289)
            _dial3Value = 2;
        else if (_dial3.transform.eulerAngles.z >= 252 && _dial3.transform.eulerAngles.z <= 253)
            _dial3Value = 3;
        else if (_dial3.transform.eulerAngles.z >= 215 && _dial3.transform.eulerAngles.z <= 217)
            _dial3Value = 4;
        else if (_dial3.transform.eulerAngles.z >= 179 && _dial3.transform.eulerAngles.z <= 181)
            _dial3Value = 5;
        else if (_dial3.transform.eulerAngles.z >= 143 && _dial3.transform.eulerAngles.z <= 145)
            _dial3Value = 6;
        else if (_dial3.transform.eulerAngles.z >= 107 && _dial3.transform.eulerAngles.z <= 109)
            _dial3Value = 7;
        else if (_dial3.transform.eulerAngles.z >= 71 && _dial3.transform.eulerAngles.z <= 73)
            _dial3Value = 8;
        else if (_dial3.transform.eulerAngles.z >= 36 && _dial3.transform.eulerAngles.z <= 37)
            _dial3Value = 9;
    }

    IEnumerator ButtonPressed()
    {
        _buttonPressed = true;
        _redButton.sprite = _redButtonPressed;

        AudioSource.PlayClipAtPoint(_buttonPress, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(1f);

        if (_dial1Value != _dial1CorrectValue || _dial2Value != _dial2CorrectValue || _dial3Value != _dial3CorrectValue)
        {
            AudioSource.PlayClipAtPoint(_staticNoise, PlayerController.Instance.transform.position);
            _backButton.enabled = false;
            _dial1Button.enabled = false;           
            _dial3Button.enabled = false;
            yield return new WaitForSeconds(2f);
            
            _redButton.sprite = _redButtonUnpressed;
            AudioSource.PlayClipAtPoint(_buttonPress, PlayerController.Instance.transform.position);
            yield return new WaitForSeconds(0.2f);

            _backButton.enabled = true;
            _backButton.enabled = true;
            _dial1Button.enabled = true;
            _dial3Button.enabled = true;
            _buttonPressed = false;

        }
        else if(_dial1Value == _dial1CorrectValue && _dial2Value == _dial2CorrectValue && _dial3Value == _dial3CorrectValue)
        {
            AudioSource.PlayClipAtPoint(_pianoMusic, PlayerController.Instance.transform.position, 0.28f);
            _backButton.enabled = false;
            _dial1Button.enabled = false;
            _dial3Button.enabled = false;
            yield return new WaitForSeconds(8f);

            _redButton.sprite = _redButtonUnpressed;
            AudioSource.PlayClipAtPoint(_buttonPress, PlayerController.Instance.transform.position);

            if (LanguageManager.Instance.Language == "en")
            {
                _text.SetText("That song...");
            }
            else if(LanguageManager.Instance.Language == "es")
            {
                _text.SetText("Esa canción...");
            }

            yield return new WaitForSeconds(2.5f);

            _text.SetText("");
            yield return new WaitForSeconds(1.3f);

            if(LanguageManager.Instance.Language == "en")
            {
                _text.SetText("It reminds me of something...");
            }
            else if (LanguageManager.Instance.Language == "es")
            {
                _text.SetText("Me recuerda a algo...");
            }

            yield return new WaitForSeconds(3.5f);

            _text.SetText("");
            yield return new WaitForSeconds(0.2f);

            _backButton.enabled = true;
            _dial1Button.enabled = true;
            _dial3Button.enabled = true;
            _buttonPressed = false;
        }
               
    }
    
}
