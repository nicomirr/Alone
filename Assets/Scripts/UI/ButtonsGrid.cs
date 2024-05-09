using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.Examples.ObjectSpin;

public class ButtonsGrid : MonoBehaviour
{
    public static ButtonsGrid Instance;

    Color _selectedButtonColor = Color.grey;
    Color _unselectedButtonColor = Color.white;

    [SerializeField] List<Button> _buttons = new List<Button>();

    string _currentAction;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(LanguageManager.Instance.Language == "en")
            _currentAction = "Look At";
        else if (LanguageManager.Instance.Language == "es")
            _currentAction = "Mirar";
    }

    void Update()
    {
        SetCurrentButton();
        DisableButtons();
        MustHideStatus();
        Language();
    }

    public void SetCurrentActionEnglish(string text)     
    { 
        if(LanguageManager.Instance.Language == "en")
            _currentAction = text;
    }

    public void SetCurrentActionSpanish(string text)
    {
        if (LanguageManager.Instance.Language == "es")
            _currentAction = text;
    }

    public string GetCurrentAction() { return _currentAction; }

    void SetCurrentButton()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_currentAction == _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text)
                _buttons[i].GetComponent<Image>().color = _selectedButtonColor;
            else
                _buttons[i].GetComponent<Image>().color = _unselectedButtonColor;

        }
    }

    void DisableButtons()
    {
        if (Pause.Instance.IsPaused || ScenesInGame.Instance.GetSceneIsPlaying() || PlayerController.Instance.GetMustHide())
        {
            for(int i = 0; i < _buttons.Count; ++i)
            {
                _buttons[i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < _buttons.Count; ++i)
            {
                _buttons[i].enabled = true;
            }
        }
    }

    void MustHideStatus()
    {
        if(PlayerController.Instance.GetMustHide())
        {            
            if (LanguageManager.Instance.Language == "en")
                _currentAction = "Look At";
            else if (LanguageManager.Instance.Language == "es")
                _currentAction = "Mirar";
        }
        
    }

    void Language()
    {
        if(LanguageManager.Instance.Language == "en")
        {
            _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Look At";
            _buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Turn On";
            _buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off";
            _buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = "Pick Up";
            _buttons[4].GetComponentInChildren<TextMeshProUGUI>().text = "Search";
            _buttons[5].GetComponentInChildren<TextMeshProUGUI>().text = "Read";
            _buttons[6].GetComponentInChildren<TextMeshProUGUI>().text = "Open";
            _buttons[7].GetComponentInChildren<TextMeshProUGUI>().text = "Close";
            _buttons[8].GetComponentInChildren<TextMeshProUGUI>().text = "Use";            
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Mirar";
            _buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Encender";
            _buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Apagar";
            _buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = "Agarrar";
            _buttons[4].GetComponentInChildren<TextMeshProUGUI>().text = "Buscar";
            _buttons[5].GetComponentInChildren<TextMeshProUGUI>().text = "Leer";
            _buttons[6].GetComponentInChildren<TextMeshProUGUI>().text = "Abrir";
            _buttons[7].GetComponentInChildren<TextMeshProUGUI>().text = "Cerrar";
            _buttons[8].GetComponentInChildren<TextMeshProUGUI>().text = "Usar";
        }

    }
}
