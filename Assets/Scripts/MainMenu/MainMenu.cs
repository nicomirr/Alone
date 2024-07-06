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
    [SerializeField] Button _optionsButton;
    [SerializeField] Button _exitButton;
    [SerializeField] Button _instructionsButton;
    [SerializeField] Button _languageButton;

    [SerializeField] GameObject _gameTitle;       
    [SerializeField] AudioSource _audioSource;

    [SerializeField] Image _optionsBackground;
    [SerializeField] Image _volumeImage;
    [SerializeField] GameObject _plus;
    [SerializeField] GameObject _minus;
    [SerializeField] GameObject _optionsSpanish;
    [SerializeField] GameObject _optionsEnglish;

    [SerializeField] GameObject _instructions;
    [SerializeField] GameObject _instructionsEnglishText;
    [SerializeField] GameObject _instructionsSpanishText;
    [SerializeField] GameObject _instructions2EnglishText;
    [SerializeField] GameObject _instructions2SpanishText;

    [SerializeField] GameObject _tutorial;

    [SerializeField] GameObject _initialInstructions;
    [SerializeField] GameObject _initialInstructionsExplainEnglish;
    [SerializeField] GameObject _initialInstructionsExploreEnglish;
    [SerializeField] GameObject _initialInstructionsExplainSpanish;
    [SerializeField] GameObject _initialInstructionsExploreSpanish;
    [SerializeField] GameObject _instructions1;
    [SerializeField] GameObject _instructions2;

    [SerializeField] GameObject _inventoryEnglishExplanation;
    [SerializeField] GameObject _inventorySpanishExplanation;




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
        Volume();
    }

    public void OnNewGameClicked()
    {
        UnityEngine.Cursor.visible = false;
        DisableMenuButtons();
        StartCoroutine(Tutorial());

    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadScene(SceneControl.Instance.SceneToLoad);
    }

    public void OnOptionsClicked()
    {
        _optionsBackground.enabled = true;
        _volumeImage.enabled = true;
        _plus.SetActive(true);
        _minus.SetActive(true);

        if(LanguageManager.Instance.Language == "en")
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

    public void OnOptionsBackButtonPressed()
    {
        _optionsBackground.enabled = false;
        _volumeImage.enabled = false;
        _plus.SetActive(false);
        _minus.SetActive(false);
        _optionsEnglish.SetActive(false);
        _optionsSpanish.SetActive(false);
    }

    public void OnInstructionsClicked()
    {
        _instructions.SetActive(true);

        if(LanguageManager.Instance.Language == "en")
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
        _optionsButton.gameObject.SetActive(false);
        _exitButton.gameObject.SetActive(false);
        _languageButton.gameObject.SetActive(false);
        _instructionsButton.gameObject.SetActive(false);
    }

    private void EnableMenuButtons()
    {
        _newGameButton.gameObject.SetActive(true);
        _continueGameButton.gameObject.SetActive(true);
        _optionsButton.gameObject.SetActive(true);
        _exitButton.gameObject.SetActive(true);
        _languageButton.gameObject.SetActive(true);
    }

    void Volume()
    {
        if(_audioSource != null)
            _audioSource.volume = 1f * GameVolume.Instance.CurrentVolume();
    }

    void Language()
    {
        if(LanguageManager.Instance.Language == "en")
        {
            _newGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "NEW GAME";
            _continueGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUE";
            _optionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "OPTIONS";
            _instructionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "INSTRUCTIONS";
            _exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "EXIT";
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _newGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "NUEVA PARTIDA";
            _continueGameButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTINUAR";
            _optionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "OPCIONES";
            _instructionsButton.GetComponentInChildren<TextMeshProUGUI>().text = "INSTRUCCIONES";
            _exitButton.GetComponentInChildren<TextMeshProUGUI>().text = "SALIR";
        }
    }

    IEnumerator Tutorial()
    {
        _gameTitle.SetActive(false);
        _audioSource.Stop();
                
        yield return new WaitForSeconds(2);

        _initialInstructions.SetActive(true);

        if(LanguageManager.Instance.Language == "en")
        {
            _initialInstructionsExplainEnglish.SetActive(true);
            _initialInstructionsExploreEnglish.SetActive(true);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _initialInstructionsExplainSpanish.SetActive(true);
            _initialInstructionsExploreSpanish.SetActive(true);
        }

        yield return new WaitForSeconds(12);

        _initialInstructions.SetActive(false);

        if (LanguageManager.Instance.Language == "en")
        {
            _initialInstructionsExplainEnglish.SetActive(false);
            _initialInstructionsExploreEnglish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _initialInstructionsExplainSpanish.SetActive(false);
            _initialInstructionsExploreSpanish.SetActive(false);
        }

        _tutorial.SetActive(true);

        yield return new WaitForSeconds(4.5f);

        if (LanguageManager.Instance.Language == "en")
        {
            _inventoryEnglishExplanation.SetActive(true);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _inventorySpanishExplanation.SetActive(true);
        }

        yield return new WaitForSeconds(4.5f);

        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadScene("PlayersRoom");
    }

}
