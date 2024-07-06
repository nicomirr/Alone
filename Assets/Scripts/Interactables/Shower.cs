using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shower : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    Animator _animator;
    CanBeOpen _canBeOpen;
    CanBeTurnedOnOff _canBeTurnedOnOff;
    AudioSource _audioSource;
    bool showerCoroutineRunning;

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

    bool _hasDrainStopper;
    bool _waterFilled;
    bool _keyPicked;

    TextMeshPro _playerText;

    public bool HasDrainStopper { get => _hasDrainStopper; set => _hasDrainStopper = value; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _canBeOpen = GetComponent<CanBeOpen>();   
        _canBeTurnedOnOff = GetComponent<CanBeTurnedOnOff>();
        _audioSource = GetComponent<AudioSource>();
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
        _playerText = GameObject.Find("PlayerText").GetComponent<TextMeshPro>();
    }

    public void LoadData(GameData data)
    {
        _hasDrainStopper = data.hasDrainStopper;
        _keyPicked = data.showerKeyPicked;
        _waterFilled = data.waterFilled;
    }

    public void SaveData(ref GameData data)
    {
        data.hasDrainStopper = _hasDrainStopper;
        data.showerKeyPicked = _keyPicked;
        data.waterFilled = _waterFilled;
    }

    private void Start()
    {
        if(_waterFilled) 
        {
            _animator.SetBool("bathtubFilled", true);
            return; 
        }

        if(_canBeTurnedOnOff.IsTurnedOn)
            StartCoroutine(ShowerRunning());
    }

    private void Update()
    {               
        Flashback();
        Language();
        CanBeTurnedOnOffState();
        ShowerCurtainState();
        ShowerRunningState();
        KeyPickedStatus();
        WaterFilled();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;

        if (ButtonsGrid.Instance.GetCurrentAction() == "Search" || ButtonsGrid.Instance.GetCurrentAction() == "Buscar")                    
            ZoomInBath();

        KeyPickedStatus();

        if (!ScenesInGame.Instance.GetIsFlashback()) return;
        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        if (ButtonsGrid.Instance.GetCurrentAction() == "Turn On" || ButtonsGrid.Instance.GetCurrentAction() == "Encender")
        {
            if (LanguageManager.Instance.Language == "en")
                TextBox.Instance.ShowText("I don't need to.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            else if (LanguageManager.Instance.Language == "es")
                TextBox.Instance.ShowText("No necesito hacer eso.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
        }
        else if (ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar")
        {
            if (LanguageManager.Instance.Language == "en")
                TextBox.Instance.ShowText("It's off.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            else if (LanguageManager.Instance.Language == "es")
                TextBox.Instance.ShowText("Está apagada.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
        }
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
        if(_waterFilled)
        {
            _canBeTurnedOnOff.CanBeTurnedOnOrOff = false;
            _canBeTurnedOnOff.Disable = true;
            return;
        }

        if (_canBeTurnedOnOff.IsTurnedOn)
        {
            if (!_canBeOpen.IsOpen) return;
                      
            if (!showerCoroutineRunning)
                StartCoroutine(ShowerRunning());
        }
        else
        {
            if (!_canBeOpen.IsOpen) return;

            _animator.SetBool("opened", false);
            _animator.SetBool("running", false);
            _audioSource.Stop();
            showerCoroutineRunning = false;
        }
    }

    void WaterFilled()
    {
        if(_hasDrainStopper && !_waterFilled && showerCoroutineRunning)
        {            
            GetComponent<ClickableObject>().CanBeTurnedOnOff = false;

            StartCoroutine(RugLoose());

            
        }
        else if(_waterFilled && !_keyPicked)
        {
            _animator.SetBool("opened", false);
            _animator.SetBool("running", false);
            _audioSource.Stop();
            showerCoroutineRunning = false;
            _animator.SetBool("bathtubFilled", true);

            GetComponent<ClickableObject>().CanBeSearched = false;
            GetComponent<ClickableObject>().HasObject = true;

        }
    }

    void KeyPickedStatus()
    {
        if (_waterFilled && ButtonsGrid.Instance.GetCurrentAction() == "Search" || ButtonsGrid.Instance.GetCurrentAction() == "Buscar")
            _keyPicked = true;
    }

    void ZoomInBath()
    {
        if (!_roomLightStatus.GetRoomHasLight() && !PlayerInventory.Instance.IsUsingFlashlight) return;
        if (_waterFilled) { return; }

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
               
        _bathtub.GetComponent<BoxCollider2D>().enabled = false;
        _mirror.SetActive(false);
        _sink.SetActive(false);
        _toillete.SetActive(false);

        MouseBehaviour.Instance.PlayerMinClickableDistance = 100;
        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);
    }

    void Flashback()
    {
        if(ScenesInGame.Instance.GetIsFlashback())
            _animator.SetBool("bathtubFilled", false);
                
    }

    public void BackButton()
    {
        PlayerController.Instance.transform.position = _characterPosNearShower;
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        TextBox.Instance.transform.position = new Vector3(TextBox.Instance.transform.position.x, _textYPosNormal);

        _showerZoom.SetActive(false);
        _backButton.SetActive(false);

        _bathtub.GetComponent<BoxCollider2D>().enabled = true;
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
        showerCoroutineRunning = true;

        _audioSource.Play();
        _animator.SetBool("opened", true);
        yield return new WaitForSeconds(0.07f);
        _animator.SetBool("running", true);
    }

    IEnumerator RugLoose()
    {
        ScenesInGame.Instance.SetSceneIsPlaying(true);
        yield return new WaitForSeconds(2.3f);

        _waterFilled = true;

        yield return new WaitForSeconds(1.5f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "The rug got loose. \nI can get the item now.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "La alfombra se aflojó. \nAhora puedo agarrar el objeto.";

        yield return new WaitForSeconds(4f);

        _playerText.text = "";

        ScenesInGame.Instance.SetSceneIsPlaying(false);


    }

    void Volume()
    {
        _audioSource.volume = 1f * GameVolume.Instance.CurrentVolume();
    }

    void Language()
    {
        if (_backButton == null) return;

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
