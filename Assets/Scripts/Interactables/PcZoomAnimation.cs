using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PcZoomAnimation : MonoBehaviour, IDataPersistence
{
    [SerializeField] Sprite _noEnterImage;
    [SerializeField] Sprite _enterImage;
    [SerializeField] Sprite _correctPasswordImage;
    [SerializeField] Sprite _notepadImage;
    [SerializeField] Computer _computer;
    bool _notepadOpened;
    [SerializeField] GameObject _notepadText;
    [SerializeField] GameObject _notepadButton;
    [SerializeField] Notepad _notepad;

    public void LoadData(GameData data)
    {
        _notepadOpened = data.notepadOpened;
    }

    public void SaveData(ref GameData data)
    {
        data.notepadOpened = _notepadOpened;
    }

    void Start()
    {
        StartCoroutine(Animation());
    }

    void Update()
    {
        if(_computer.ZoomIn && _computer.CorrectPasswordEntered)
        {
            if (Screen.width <= 1280 || Screen.width == 1600 && Screen.height == 1200)
                _notepadButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50.9f, 188.9f);

            else if((Screen.width == 1920 && Screen.height == 1200) || (Screen.width == 1680 && Screen.height == 1050) || Screen.width == 1440 && Screen.height == 900)
                _notepadButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50.9f, 159.4f);

            _notepadButton.SetActive(true);
        }

        if (!_computer.ZoomIn) { _notepadButton.SetActive(false); }

        if(_notepadOpened)
        {
            _notepad.NotepadBeenOpened = true;

            GetComponent<UnityEngine.UI.Image>().sprite = _notepadImage;
            NotepadLanguage();
            if(_computer.ZoomIn)
                _notepadText.SetActive(true);
            else
                _notepadText.SetActive(false);
        }
    }

    IEnumerator Animation()
    {
        if (!_computer.CorrectPasswordEntered)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = _enterImage;
            yield return new WaitForSeconds(1);

            GetComponent<UnityEngine.UI.Image>().sprite = _noEnterImage;
            yield return new WaitForSeconds(1);

            StartCoroutine(Animation());
        }       
        else
        {
            GetComponent<UnityEngine.UI.Image>().sprite = _correctPasswordImage;
            _notepadButton.SetActive(true);
        }
    }

    void NotepadLanguage()
    {
        if (LanguageManager.Instance.Language == "en")
            _notepadText.GetComponent<TextMeshProUGUI>().text = "Reminder: If a key is missing, check in plants. I have lost keys while watering them a lot of times.";
        else if (LanguageManager.Instance.Language == "es")
            _notepadText.GetComponent<TextMeshProUGUI>().text = "Recordatorio: Si hay alguna llave perdida, buscar en las plantas. Ya perdí muchas llaves regando.";
    }

    public void OnNotepadButtonPressed()
    {
        _notepadOpened = true;
    }

}
