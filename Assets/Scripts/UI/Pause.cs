using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause Instance;

    bool isPaused;

    [Header("Pause Y Pos")]
    [SerializeField] float _yPos;
    [SerializeField] GameObject _continueButton;
    [SerializeField] GameObject _instructionsButton;
    [SerializeField] GameObject _exitButton;

    [SerializeField] GameObject _instructions;
    [SerializeField] GameObject _instructionsEnglishText;
    [SerializeField] GameObject _instructionsSpanishText;

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
    
    public void Instructions()
    {
        _instructions.SetActive(true);

        if (LanguageManager.Instance.Language == "en")
        {
            _instructionsEnglishText.SetActive(true);
            _instructionsSpanishText.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _instructionsEnglishText.SetActive(false);
            _instructionsSpanishText.SetActive(true);
        }
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
