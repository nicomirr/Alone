using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LibraryPuzzle : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    static bool _isUsingLibrary;

    bool _photoAlbumShown;

    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _newspaper;
    [SerializeField] TextMeshProUGUI _symbols;
    [SerializeField] TextMeshProUGUI _rome;
    [SerializeField] TextMeshProUGUI _familyAlbum;
    [SerializeField] TextMeshProUGUI _panelBackButton;

    [SerializeField] GameObject _textSelectionPanel;

    [SerializeField] GameObject _newspaperSpanish;
    [SerializeField] GameObject _newspaperEnglish;
    [SerializeField] GameObject _newspaperBackButton;

    [SerializeField] GameObject _symbologyBookSpanish;
    [SerializeField] GameObject _symbologyBookEnglish;
    [SerializeField] GameObject _symbologyBookBackButton;

    [SerializeField] GameObject _familyPhotoAlbum;
    [SerializeField] Sprite _familyPhotoOriginal;
    [SerializeField] Sprite _familyPhoto1;
    [SerializeField] Sprite _familyPhoto2;
    [SerializeField] Sprite _familyPhoto3;
    [SerializeField] Sprite _familyPhoto4;
    [SerializeField] Sprite _familyPhoto5;
    [SerializeField] AudioClip _glitchSound;
    [SerializeField] GameObject _familyPhotoAlbumBackButton;

    [SerializeField] GameObject _romeBookEnglish;
    [SerializeField] GameObject _romeBookSpanish;
    [SerializeField] GameObject _romeBookBackButton;

    [SerializeField] AudioClip _pickUpText;
    [SerializeField] AudioClip _openBook;

    bool _notClickable;
    RoomLightStatus _roomLightStatus;


    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    public void LoadData(GameData data)
    {
        _photoAlbumShown = data.photoAlbumShown;
    }

    public void SaveData(ref GameData data)
    {
        data.photoAlbumShown = _photoAlbumShown;    
    }
    public static bool IsUsingLibrary { get => _isUsingLibrary; set => _isUsingLibrary = value; }

    private void Update()
    {
        Language();

        if(ScenesInGame.Instance.GetIsFlashback())        
            GetComponent<ClickableObject>().CanBeRead = false;        
        else
            GetComponent<ClickableObject>().CanBeRead = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Pause.Instance.IsPaused) return;
        if (!_roomLightStatus.GetRoomHasLight() && !PlayerInventory.Instance.IsUsingFlashlight) return;
        if (ScenesInGame.Instance.GetIsFlashback()) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        if(ButtonsGrid.Instance.GetCurrentAction() == "Read" || ButtonsGrid.Instance.GetCurrentAction() == "Leer")
        {
            _isUsingLibrary = true;

            PlayerController.Instance.SetIsInteractingWithEnviroment(true);
            _textSelectionPanel.SetActive(true);
        }
    }

    void Language()
    {
        if (LanguageManager.Instance.Language == "en")
        {
            if (_newspaperBackButton == null) return;

            _title.text = "WHAT SHOULD I READ?";
            _newspaper.text = "NEWSPAPER ARTICLE";
            _symbols.text = "SYMBOLS AND THEIR MEANINGS";
            _rome.text = "ROMAN CULTURE";
            _familyAlbum.text = "FAMILY PHOTO ALBUM";
            _panelBackButton.text = "BACK";
            _newspaperBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
            _symbologyBookBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
            _romeBookBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
            _familyPhotoAlbumBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        }
        else if(LanguageManager.Instance.Language == "es")
        {
            if (_newspaperBackButton == null) return;

            _title.text = "¿QUE DEBERIA LEER?";
            _newspaper.text = "ARTICULO PERIODISTICO";
            _symbols.text = "SIMBOLOS Y SUS SIGNIFICADOS";
            _rome.text = "CULTURA ROMANA";
            _familyAlbum.text = "ALBUM FAMILIAR";
            _panelBackButton.text = "ATRAS";
            _newspaperBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
            _symbologyBookBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
            _romeBookBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
            _familyPhotoAlbumBackButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
        }
        //CAMBIAR LENGUAJE BOTONES DE ATRAS
    }

    public void OnNewspaperButtonPressed()
    {
        AudioSource.PlayClipAtPoint(_pickUpText, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

        if (LanguageManager.Instance.Language == "es")
        {
            _newspaperSpanish.SetActive(true);
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            _newspaperEnglish.SetActive(true);
        }

        _newspaperBackButton.SetActive(true);
    }

    public void OnNewspaperBackButtonPressed()
    {
        if (LanguageManager.Instance.Language == "es")
        {
            _newspaperSpanish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            _newspaperEnglish.SetActive(false);
        }

        _newspaperBackButton.SetActive(false);
    }
       
    public void OnSymbologyBookButtonPressed()
    {
        AudioSource.PlayClipAtPoint(_openBook, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

        if (LanguageManager.Instance.Language == "es")
        {
            _symbologyBookSpanish.SetActive(true);
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            _symbologyBookEnglish.SetActive(true);
        }

        _symbologyBookBackButton.SetActive(true);
    }

    public void OnSymbologyBookBackButtonPressed()
    {
        if (LanguageManager.Instance.Language == "es")
        {
            _symbologyBookSpanish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            _symbologyBookEnglish.SetActive(false);
        }

        _symbologyBookBackButton.SetActive(false);
    }

    public void OnRomeTextButtonPressed()
    {
        AudioSource.PlayClipAtPoint(_openBook, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

        if (LanguageManager.Instance.Language == "es")
        {
            _romeBookSpanish.SetActive(true);
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            _romeBookEnglish.SetActive(true);
        }

        _romeBookBackButton.SetActive(true);
    }

    public void OnRomeTextBackButtonPressed()
    {
        if (LanguageManager.Instance.Language == "es")
        {
            _romeBookSpanish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "en")
        {
            _romeBookEnglish.SetActive(false);
        }

        _romeBookBackButton.SetActive(false);
    }

    public void OnFamilyPhotoButtonPressed()
    {
        if(_photoAlbumShown)
            AudioSource.PlayClipAtPoint(_openBook, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

        _familyPhotoAlbum.SetActive(true);
        _familyPhotoAlbumBackButton.SetActive(true);

        if(!_photoAlbumShown)
        {
            _photoAlbumShown = true;
            StartCoroutine(PhotoAlbumGlitch());
        }
    }

    public void OnFamilyPhotoBackButtonPressed() 
    {
        _familyPhotoAlbum.SetActive(false);
        _familyPhotoAlbumBackButton.SetActive(false);
    }

    public void OnBackButtonPressed()
    {
        _isUsingLibrary = false;

        PlayerController.Instance.SetIsInteractingWithEnviroment(false);
        _textSelectionPanel.SetActive(false);
    }

    IEnumerator PhotoAlbumGlitch()
    {
        _familyPhotoAlbumBackButton.GetComponentInChildren<Button>().enabled = false;
        
        Image familyPhotoAlbumImage = _familyPhotoAlbum.GetComponent<Image>();

        AudioSource.PlayClipAtPoint(_glitchSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

        familyPhotoAlbumImage.sprite = _familyPhoto1;
        yield return new WaitForSeconds(0.04f);

        familyPhotoAlbumImage.sprite = _familyPhoto2;
        yield return new WaitForSeconds(0.04f);

        familyPhotoAlbumImage.sprite = _familyPhoto3;
        yield return new WaitForSeconds(0.04f);

        familyPhotoAlbumImage.sprite = _familyPhoto4;
        yield return new WaitForSeconds(0.04f);

        familyPhotoAlbumImage.sprite = _familyPhoto5;
        yield return new WaitForSeconds(0.04f);

        familyPhotoAlbumImage.sprite = _familyPhotoOriginal;
        yield return new WaitForSeconds(0.04f);

        _familyPhotoAlbumBackButton.GetComponentInChildren<Button>().enabled = true;
    }

}
