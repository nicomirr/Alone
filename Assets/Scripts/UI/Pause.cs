using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static Pause Instance;

    bool isPaused;

    [Header("Pause Y Pos")]
    [SerializeField] float _yPos;
    [SerializeField] GameObject _continueButton;
    [SerializeField] GameObject _instructionsButton;
    [SerializeField] GameObject _exitButton;

    [SerializeField] Image _optionsBackground;
    [SerializeField] Image _volumeImage;
    [SerializeField] GameObject _plus;
    [SerializeField] GameObject _minus;
    [SerializeField] GameObject _optionsEnglish;
    [SerializeField] GameObject _optionsSpanish;

    [SerializeField] GameObject _instructions;
    [SerializeField] GameObject _instructionsEnglishText;
    [SerializeField] GameObject _instructions2EnglishText;
    [SerializeField] GameObject _instructionsSpanishText;
    [SerializeField] GameObject _instructions2SpanishText;

    [SerializeField] GameObject _instructions1;
    [SerializeField] GameObject _instructions2;

    private void Awake()
    {           
        Instance = this;
    }

    public bool IsPaused { get => isPaused; set => isPaused = value; }

    private void Update()
    {
        Language();      
    }
        
    public void Continue()
    {
        isPaused = false;
        GameObject.Find("PauseScreen").transform.position = new Vector3(this.transform.position.x, _yPos);
    }
    
    public void Options()
    {
        _optionsBackground.enabled = true;
        _volumeImage.enabled = true;
        _plus.SetActive(true);
        _minus.SetActive(true);

        if (LanguageManager.Instance.Language == "en")
        {
            _optionsEnglish.SetActive(true);
            _optionsSpanish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _optionsEnglish.SetActive(false);
            _optionsSpanish.SetActive(true);
        }
    }   

    public void Instructions()
    {
        _instructions.SetActive(true);

        if (LanguageManager.Instance.Language == "en")
        {
            _instructionsEnglishText.SetActive(true);
            _instructions2EnglishText.SetActive(true);
            _instructionsSpanishText.SetActive(false);
            _instructions2SpanishText.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _instructionsEnglishText.SetActive(false);
            _instructions2EnglishText.SetActive(false);
            _instructionsSpanishText.SetActive(true);
            _instructions2SpanishText.SetActive(true);
        }
    }

    public void OnInstructionNextArrowClicked()
    {
        _instructions1.SetActive(false);
        _instructions2.SetActive(true);
    }

    public void OnInstructionBackArrowClicked()
    {
        _instructions1.SetActive(true);
        _instructions2.SetActive(false);
    }

    public void InstructionsBack()
    {
        _instructions.SetActive(false);
    }

    public void Exit()
    {
        //System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); 
        //Application.Quit(); 
        Application.Quit();
    }

    void Language()
    {
        if(LanguageManager.Instance.Language == "en")
        {
            _continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            _instructionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Instructions";
            _exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exit";
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
            _instructionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Instrucciones";
            _exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Salir";
        }
    }
}
