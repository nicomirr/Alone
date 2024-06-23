using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LibraryPuzzle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject _textSelectionPanel;

    [SerializeField] GameObject _newspaperSpanish;
    [SerializeField] GameObject _newspaperEnglish;
    [SerializeField] GameObject _newspaperBackButton;

    [SerializeField] GameObject _symbologyBookSpanish;
    [SerializeField] GameObject _symbologyBookEnglish;
    [SerializeField] GameObject _symbologyBookBackButton;

    [SerializeField] AudioClip _pickUpText;

    bool _notClickable;
    RoomLightStatus _roomLightStatus;

    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    private void Update()
    {
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
            PlayerController.Instance.SetIsInteractingWithEnviroment(true);
            _textSelectionPanel.SetActive(true);
        }
    }

    public void OnNewspaperButtonPressed()
    {
        AudioSource.PlayClipAtPoint(_pickUpText, PlayerController.Instance.transform.position);

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

    public void OnBackButtonPressed()
    {
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);
        _textSelectionPanel.SetActive(false);
    }

    public void OnSymbologyBookButtonPressed()
    {
        AudioSource.PlayClipAtPoint(_pickUpText, PlayerController.Instance.transform.position);

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

}
