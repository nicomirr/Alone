using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] Button _newGameButton;
    [SerializeField] Button _continueGameButton;
    //[SerializeField] Button _optionsButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _instructionsButton;
    [SerializeField] Button _languageButton;

    [SerializeField] GameObject _gameTitle;       
    [SerializeField] AudioSource _audioSource;
        
    [SerializeField] GameObject _instructions;
    [SerializeField] GameObject _instructionsEnglishText;
    [SerializeField] GameObject _instructionsSpanishText;

    [SerializeField] GameObject _tutorial;


    private void Start()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            _continueGameButton.interactable = false;
        }
    }

    private void Update()
    {
        Language();
    }

    public void OnNewGameClicked()
    {     
        DisableMenuButtons();
        StartCoroutine(Tutorial());

    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadScene(SceneControl.Instance.SceneToLoad);
    }

    public void OnInstructionsClicked()
    {
        _instructions.SetActive(true);

        if(LanguageManager.Instance.Language == "en")
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

    public void OnInstructionsBackButtonClicked()
    {
        _instructions.SetActive(false);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        _newGameButton.gameObject.SetActive(false);
        _continueGameButton.gameObject.SetActive(false);
        //_optionsButton.interactable = false;
        _exitButton.gameObject.SetActive(false);
        _languageButton.gameObject.SetActive(false);
        _instructionsButton.gameObject.SetActive(false);
    }

    private void EnableMenuButtons()
    {
        _newGameButton.gameObject.SetActive(true);
        _continueGameButton.gameObject.SetActive(true);
        //_optionsButton.interactable = false;
        _exitButton.gameObject.SetActive(true);
        _languageButton.gameObject.SetActive(true);
    }


    void Language()
    {
        if(LanguageManager.Instance.Language == "en")
        {
            _newGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEW GAME";
            _continueGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUE";
            _instructionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "INSTRUCTIONS";
            _exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "EXIT";
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _newGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "NUEVA PARTIDA";
            _continueGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUAR";
            _instructionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "INSTRUCCIONES";
            _exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "SALIR";
        }
    }

    IEnumerator Tutorial()
    {
        _gameTitle.SetActive(false);
        _audioSource.Stop();
                
        yield return new WaitForSeconds(2);

        _tutorial.SetActive(true);

        yield return new WaitForSeconds(4);

        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadScene("PlayersRoom");
    }

}
