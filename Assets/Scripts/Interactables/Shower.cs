using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shower : MonoBehaviour, IPointerClickHandler
{
    Animator _animator;
    CanBeOpen _canBeOpen;
    CanBeTurnedOnOff _canBeTurnedOnOff;
    AudioSource _audioSource;    
    bool _coroutineRunning;

    RoomLightStatus _roomLightStatus;

    bool _notClickable;

    [Header("Character position")]
    [SerializeField] Vector3 _characterPosZoomInShower;
    Vector3 _characterPosNearShower;

    [Header("Text position")]
    [SerializeField] float _textYPosZoomIn;
    [SerializeField] float _textYPosNormal;

    [Header("Shower Items")]
    [SerializeField] GameObject _showerZoom;
    [SerializeField] GameObject _backButton;

    [Header("Bathroom Items")]
    [SerializeField] GameObject _bathtub;
    [SerializeField] GameObject _mirror;
    [SerializeField] GameObject _sink;
    [SerializeField] GameObject _toillete;

    [SerializeField] Texture2D _blackPointer;
       
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _canBeOpen = GetComponent<CanBeOpen>();   
        _canBeTurnedOnOff = GetComponent<CanBeTurnedOnOff>();
        _audioSource = GetComponent<AudioSource>();
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }
        
    private void Update()
    {
        CanBeTurnedOnOffState();
        ShowerCurtainState();
        ShowerRunningState();        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;

        if (ButtonsGrid.Instance.GetCurrentAction() == "Search" || ButtonsGrid.Instance.GetCurrentAction() == "Buscar")                    
            ZoomInBath();                    
    }

    void CanBeTurnedOnOffState()
    {
        if (!_canBeOpen.IsOpen) _canBeTurnedOnOff.CanBeTurnedOnOrOff = false;
        else _canBeTurnedOnOff.CanBeTurnedOnOrOff = true;
    }

    void ShowerCurtainState()
    {
        if (_canBeOpen.IsOpen)
            _animator.SetBool("curtainIsClosed", false);
        else
            _animator.SetBool("curtainIsClosed", true);
    }

    void ShowerRunningState()
    {
        if (_canBeTurnedOnOff.IsTurnedOn)
        {
            if (!_canBeOpen.IsOpen) return;

            if (!_coroutineRunning)
                StartCoroutine(ShowerRunning());
        }
        else
        {
            if (!_canBeOpen.IsOpen) return;

            _animator.SetBool("opened", false);
            _animator.SetBool("running", false);
            _audioSource.Stop();
            _coroutineRunning = false;
        }
    }

    void ZoomInBath()
    {
        if (!_roomLightStatus.GetRoomHasLight()) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        if (!_canBeOpen.IsOpen)
        {
            if(LanguageManager.Instance.Language == "en")
            {
                TextBox.Instance.ShowText("The curtains are closed.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                return;
            }
            else if(LanguageManager.Instance.Language == "es")
            {
                TextBox.Instance.ShowText("La cortina está cerrada.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                return;
            }
        }
        if (_canBeTurnedOnOff.IsTurnedOn)
        {
            if(LanguageManager.Instance.Language == "en")
            {
                TextBox.Instance.ShowText("I should turn the water off first.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                return;
            }
            else if(LanguageManager.Instance.Language == "es")
            {
                TextBox.Instance.ShowText("Debería cerrar el agua primero.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                return;
            }

            
        }

        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 0;

        _characterPosNearShower = PlayerController.Instance.transform.position;
        PlayerController.Instance.transform.position = _characterPosZoomInShower;
        PlayerController.Instance.SetIsInteractingWithEnviroment(true);

        TextBox.Instance.transform.position = new Vector3(TextBox.Instance.transform.position.x, _textYPosZoomIn);


        _showerZoom.SetActive(true);
        _backButton.SetActive(true);
               
        _bathtub.SetActive(false);
        _mirror.SetActive(false);
        _sink.SetActive(false);
        _toillete.SetActive(false);

        MouseBehaviour.Instance.PlayerMinClickableDistance = 100;
        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);
    }

    public void BackButton()
    {
        PlayerController.Instance.transform.position = _characterPosNearShower;
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        TextBox.Instance.transform.position = new Vector3(TextBox.Instance.transform.position.x, _textYPosNormal);

        _showerZoom.SetActive(false);
        _backButton.SetActive(false);
              
        _bathtub.SetActive(true);
        _mirror.SetActive(true);
        _sink.SetActive(true);
        _toillete.SetActive(true);

        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 5;

        MouseBehaviour.Instance.PlayerMinClickableDistance = 3;
        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);

    }

    IEnumerator ShowerRunning()
    {
        _coroutineRunning = true;

        _audioSource.Play();
        _animator.SetBool("opened", true);
        yield return new WaitForSeconds(0.07f);
        _animator.SetBool("running", true);
    }

    void Language()
    {
        if (LanguageManager.Instance.Language == "en")
        {
            _backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        }
        else if(LanguageManager.Instance.Language == "es")
        {
            _backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
        }
    }
}
