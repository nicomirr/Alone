using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respiration : MonoBehaviour, IDataPersistence
{

    [SerializeField] bool _isTutorial;
    bool _tryPressed;
    bool _start;

    int _direction = -1;
    [SerializeField] float _speed;

    [SerializeField] Rigidbody2D _respiratorRb;
    [SerializeField] float _respiratorForce;
    [SerializeField] GameObject _respiratorTutorial;

    [SerializeField] GameObject _tutorialEnglish;
    [SerializeField] GameObject _tutorialSpanish;
    [SerializeField] GameObject _tutorialOkButton;
    [SerializeField] GameObject _tryEnglishButton;
    [SerializeField] GameObject _trySpanishButton;
    [SerializeField] GameObject _resetEnglishButton;
    [SerializeField] GameObject _resetSpanishButton;
        
    bool _tutorialShown;
    bool _screenSet;
        
    public void LoadData(GameData data)
    {
        _tutorialShown = data.respirationTutorialShown;
    }

    public void SaveData(ref GameData data)
    {
        data.respirationTutorialShown = _tutorialShown;
    }

    public bool Start { get => _start; set => _start = value; }
    public bool TutorialShown { get => _tutorialShown; set => _tutorialShown = value; }
    public bool IsTutorial { get => _isTutorial; set => _isTutorial = value; }

    void Update()
    {       
        RespirationState();
        RespirationTutorial();
    }

    void RespirationTutorial()
    {
        if (_isTutorial) return;

        if(!_tutorialShown)
        {
            if(LanguageManager.Instance.Language == "en")
            {                
                _tutorialEnglish.SetActive(true);     
                _tryEnglishButton.SetActive(true);
                _resetEnglishButton.SetActive(true);                

            }
            else if (LanguageManager.Instance.Language == "es")
            {
                _tutorialSpanish.SetActive(true);     
                _trySpanishButton.SetActive(true);
                _resetSpanishButton.SetActive(true);

            }

            if (_screenSet) return;
            // 7.43
            //105
            if (Screen.width <= 1280)
            {
                _tutorialEnglish.transform.position = _tutorialEnglish.transform.parent.TransformPoint(10.99f, -0.3221f, _tutorialEnglish.transform.position.z);
                _tutorialSpanish.transform.position = _tutorialSpanish.transform.parent.TransformPoint(10.99f, -0.3221f, _tutorialSpanish.transform.position.z);
                _respiratorTutorial.transform.position = _respiratorTutorial.transform.parent.TransformPoint(7.8f, -1.676414f, _respiratorTutorial.transform.position.z);
                _tutorialOkButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(103.8f, -28.8f);
            }
            else if (Screen.width == 1600 && Screen.height == 1200)
            {
                _tutorialEnglish.transform.position = _tutorialEnglish.transform.parent.TransformPoint(10.99f, -0.3221f, _tutorialEnglish.transform.position.z);
                _tutorialSpanish.transform.position = _tutorialSpanish.transform.parent.TransformPoint(10.99f, -0.3221f, _tutorialSpanish.transform.position.z);
                _respiratorTutorial.transform.position = _respiratorTutorial.transform.parent.TransformPoint(7.8f, -1.676414f, _respiratorTutorial.transform.position.z);
                _tutorialOkButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, -28.8f);
            }
            else if (Screen.width == 1680 && Screen.height == 1050)
            {
                _tutorialEnglish.transform.position = _tutorialEnglish.transform.parent.TransformPoint(10.99f, -0.3221f, _tutorialEnglish.transform.position.z);
                _tutorialSpanish.transform.position = _tutorialSpanish.transform.parent.TransformPoint(10.99f, -0.3221f, _tutorialSpanish.transform.position.z);
                _respiratorTutorial.transform.position = _respiratorTutorial.transform.parent.TransformPoint(7.43f, -1.676414f, _respiratorTutorial.transform.position.z);
                _tutorialOkButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(105f, -28.8f);
            }
            else if (Screen.width == 1920 && Screen.height == 1200)
            {
                _tutorialEnglish.transform.position = _tutorialEnglish.transform.parent.TransformPoint(11.27f, -0.3221f, _tutorialEnglish.transform.position.z);
                _tutorialSpanish.transform.position = _tutorialSpanish.transform.parent.TransformPoint(11.27f, -0.3221f, _tutorialSpanish.transform.position.z);
                _respiratorTutorial.transform.position = _respiratorTutorial.transform.parent.TransformPoint(7.8f, _respiratorTutorial.transform.position.y, _respiratorTutorial.transform.position.z);
                _tutorialOkButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, -28.8f);
            }

            _respiratorTutorial.SetActive(true);
            _tutorialOkButton.SetActive(true);
            _screenSet = true;
        }
    }

    void RespirationState()
    {
        if(_isTutorial && _tryPressed)
        {
            _respiratorTutorial.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            _tryPressed = false;
        }

        if(_isTutorial && Input.GetMouseButtonDown(0) && _respiratorTutorial.GetComponent<Rigidbody2D>().gravityScale != 0.0f)
        {
            if(_respiratorTutorial.activeInHierarchy)
                _respiratorTutorial.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _respiratorForce));
        }

        if (_isTutorial) return;

        if (!_tutorialShown) return;
        if (!_start) return;

        if (_respiratorRb != null)
            _respiratorRb.gravityScale = 0.3f;

        if (Input.GetMouseButtonDown(0))
        {
            if (_respiratorRb != null)
                _respiratorRb.AddForce(new Vector2(0, _respiratorForce));
        }
    }

    public void OnPressOktutorialButton()
    {
        if(_isTutorial) return;

        if (LanguageManager.Instance.Language == "en")
        {            
            _tutorialEnglish.SetActive(false);
            _tryEnglishButton.SetActive(false);
            _resetEnglishButton.SetActive(false);           
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _tutorialSpanish.SetActive(false);
            _trySpanishButton.SetActive(false);
            _resetSpanishButton.SetActive(false);
        }

        _respiratorTutorial.SetActive(false);
        _tutorialShown = true;               
        _tutorialOkButton.SetActive(false);
    }

    public void OnPressTryButton()
    {
        _tryPressed = true;
    }

    public void OnPressResetButton()
    {
        if(_isTutorial)
        {
            _respiratorTutorial.gameObject.SetActive(true);
            _respiratorTutorial.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            _respiratorTutorial.gameObject.transform.position = new Vector2(_respiratorTutorial.gameObject.transform.position.x, -1.676414f);
            _tryPressed = false;
        }
    }

   
}
