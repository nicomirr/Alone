using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respiration : MonoBehaviour, IDataPersistence
{
    bool _start;

    int _direction = -1;
    [SerializeField] float _speed;

    [SerializeField] Rigidbody2D _respiratorRb;
    [SerializeField] float _respiratorForce;

    [SerializeField] GameObject _tutorialEnglish;
    [SerializeField] GameObject _tutorialSpanish;
        
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

    void Update()
    {       
        RespirationState();
        RespirationTutorial();
    }

    void RespirationTutorial()
    {
        if(!_tutorialShown)
        {
            if(LanguageManager.Instance.Language == "en")
            {                
                _tutorialEnglish.SetActive(true);                
            }
            else if (LanguageManager.Instance.Language == "es")
            {
                _tutorialSpanish.SetActive(true);                
            }
        }
    }

    void RespirationState()
    {
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
        if (LanguageManager.Instance.Language == "en")
        {            
            _tutorialEnglish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _tutorialSpanish.SetActive(false);
        }
       
        _tutorialShown = true;
    }

    public void OnPressTryButton()
    {

    }

   
}
