using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour, IDataPersistence
{
    public static LanguageManager Instance;

    [SerializeField] Button languageButton;
    string _language = "en";
    string _languageDisplay;

    static bool _languageChanged;

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


    public void SaveData(ref GameData data)
    {
        data.language = _language;
    }

    public void LoadData(GameData data)
    {
        _language = data.language;
    }

    private void Update()
    {
        
        if(_language == "en") { _languageDisplay = "ESPAÑOL"; }
        else if(_language == "es") { _languageDisplay = "ENGLISH"; }

        if (languageButton != null) { languageButton.GetComponentInChildren<TextMeshProUGUI>().text = _languageDisplay; }

    }

    public void ChangeLanguage()
    {
        if(_language == "en") { Language = "es"; }
        else if(_language == "es") { Language = "en"; }        

        _languageChanged = true;
    }

    public string Language { get => _language; set => _language = value; }
    public bool LanguageChanged { get => _languageChanged; set => _languageChanged = value; }
}
