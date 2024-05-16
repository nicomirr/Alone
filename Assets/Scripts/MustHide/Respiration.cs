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

            _respiratorTutorial.SetActive(true);
            _tutorialOkButton.SetActive(true);
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
