using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Computer : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    Animator _computerAnimator;
    CanBeTurnedOnOff _canBeTurnedOnOff;
    bool _notClickable;

    RoomLightStatus _roomLightStatus;

    Vector3 _characterPosNearComputer;
    [SerializeField] Vector3 _characterPosZoomInComputer;

    [SerializeField] GameObject _computerZoom;
    [SerializeField] GameObject _computerLockedInterface;
    [SerializeField] GameObject _computerUnlockedInterface;
    [SerializeField] GameObject _inputField;

    [Header("Objects to deactivate")]
    [SerializeField] GameObject _desk;
    [SerializeField] GameObject _chair;
    [SerializeField] GameObject _shelf;
    [SerializeField] GameObject _sofa;
    [SerializeField] GameObject _tv;

    [SerializeField] Texture2D _blackPointer;

    [SerializeField] GameObject _notepad;
    [SerializeField] GameObject _notepadText;

    [SerializeField] GameObject computerText;
    [SerializeField] GameObject backButtonA;
    [SerializeField] GameObject backButtonB;
    [SerializeField] GameObject loginButton;
    [SerializeField] GameObject forgotPasswordButton;

    string _password = "5947";   //5947
    string _typedPassword;
    bool _correctPasswordEntered;

    bool _zoomIn;

    private void Awake()
    {
        _computerAnimator = GetComponentInChildren<Animator>();
        _canBeTurnedOnOff = GetComponent<CanBeTurnedOnOff>();
        _roomLightStatus = GetComponent<RoomLightStatus>();
    }

    public void SaveData(ref GameData data)
    {
        data.correctPasswordEntered = _correctPasswordEntered;
    }

    public void LoadData(GameData data)
    {
        _correctPasswordEntered = data.correctPasswordEntered;
    }

    public bool CorrectPasswordEntered { get => _correctPasswordEntered; set => _correctPasswordEntered = value; }
    public bool ZoomIn { get => _zoomIn; set => _zoomIn = value; }

    private void Update()
    {
        PauseStatus();
        Language();
        ChangeStatus();

        if (_inputField == null) return;
        _typedPassword = _inputField.GetComponent<TMP_InputField>().text;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ZoomInComputer();
    }

    void ChangeStatus()
    {
        if (_canBeTurnedOnOff.IsTurnedOn)
        {
            _computerAnimator.SetBool("computerIsOn", true);
            GetComponent<ClickableObject>().CanBeUsed = true;

            if (_correctPasswordEntered)
            {
                _computerAnimator.SetBool("unlocked", true);
                _notepad.GetComponent<BoxCollider2D>().enabled = true;

                if (_notepad.GetComponent<Notepad>().NotepadBeenOpened)
                {
                    _computerAnimator.SetBool("notepadBeenOpened", true);
                }
            }
        }
        else
        {
            _computerAnimator.SetBool("computerIsOn", false);
            GetComponent<ClickableObject>().CanBeUsed = false;
        }

    }

    void ZoomInComputer()
    {
        if (ButtonsGrid.Instance.GetCurrentAction() != "Use" && ButtonsGrid.Instance.GetCurrentAction() != "Usar") return;

        if (!ScenesInGame.Instance.GetFirstKitchenScenePlayed() && _canBeTurnedOnOff.IsTurnedOn)
        {
            if (LanguageManager.Instance.Language == "en")
                TextBox.Instance.ShowText("I shouldn't keep them waiting.");
            else if (LanguageManager.Instance.Language == "es")
                TextBox.Instance.ShowText("No debería dejarlos esperando.");

            return;
        }

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        if (!_canBeTurnedOnOff.IsTurnedOn) return;

        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 0;

        _zoomIn = true;

        PlayerController.Instance.SetLookingBack(false);
        PlayerController.Instance.SetLookingFront(false);
        PlayerController.Instance.SetIsInteractingWithEnviroment(true);

        _characterPosNearComputer = PlayerController.Instance.transform.position;
        PlayerController.Instance.transform.position = _characterPosZoomInComputer;
        PlayerController.Instance.transform.localScale = new Vector3(1, 1);

        this.GetComponent<BoxCollider2D>().enabled = false;
        _desk.SetActive(false);
        _chair.SetActive(false);
        _shelf.SetActive(false);
        _sofa.SetActive(false);
        _tv.SetActive(false);

        Color color = _computerZoom.GetComponent<Image>().color;
        color.a = 1;

        _computerZoom.GetComponent<Image>().color = color;
        //_computerZoom.GetComponent<BoxCollider2D>().enabled = true;

        if (!_correctPasswordEntered)
        {
            _computerLockedInterface.SetActive(true);

        }
        else
        {
            _computerLockedInterface.SetActive(false);
            _computerUnlockedInterface.SetActive(true);
            _notepad.GetComponent<BoxCollider2D>().enabled = true;

            if (_notepad.GetComponent<Notepad>().NotepadBeenOpened)
            {
                _notepad.GetComponent<BoxCollider2D>().enabled = false;
                _notepadText.SetActive(true);
            }
        }

        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);
    }

    public void ForgotPasswordButton()
    {
        GameObject.Find("ComputerZoomInterfaceLocked").transform.GetChild(4).gameObject.SetActive(true);

    }

    public void LogInButton()
    {
        if (_typedPassword == _password)
        {
            _correctPasswordEntered = true;
            _computerLockedInterface.SetActive(false);
            _computerUnlockedInterface.SetActive(true);
        }
        else
        {
            _inputField.GetComponent<TMP_InputField>().text = "";
            
        }

    }

    public void BackButton()
    {
        _zoomIn = false;

        PlayerController.Instance.transform.position = _characterPosNearComputer;
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);

        this.GetComponent<BoxCollider2D>().enabled = true;
        _desk.SetActive(true);
        _chair.SetActive(true);
        _shelf.SetActive(true);
        _sofa.SetActive(true);
        _tv.SetActive(true);

        Color color = _computerZoom.GetComponent<Image>().color;
        color.a = 0;

        _computerZoom.GetComponent<Image>().color = color;
        //_computerZoom.GetComponent<BoxCollider2D>().enabled = false;
        _computerLockedInterface.SetActive(false);
        _computerUnlockedInterface.SetActive(false);
        _notepad.GetComponent<BoxCollider2D>().enabled = false;
        _notepadText.SetActive(false);

        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 5;

        _computerZoom.GetComponent<Image>().color = color;
    }
    
    void Language()
    {
        if (LanguageManager.Instance.Language == "en")
        {
            if (computerText == null) return;

            computerText.GetComponent<TextMeshProUGUI>().text = "Read the note. Months are numbers.";
            backButtonA.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
            backButtonB.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
            loginButton.GetComponentInChildren<TextMeshProUGUI>().text = "Log in";
            forgotPasswordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Forgot password?";

        }
        else if (LanguageManager.Instance.Language == "es")
        {
            if (computerText == null) return;

            computerText.GetComponent<TextMeshProUGUI>().text = "Leer nota. Los meses son números.";
            backButtonA.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
            backButtonB.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
            loginButton.GetComponentInChildren<TextMeshProUGUI>().text = "Iniciar";
            forgotPasswordButton.GetComponentInChildren<TextMeshProUGUI>().text = "Olvidé contraseña";
        }

    }

    void PauseStatus()
    {
        if (_inputField == null) return;

        if(Pause.Instance.IsPaused)
        {
            _inputField.SetActive(false);
        }
        else
        {
            _inputField.SetActive(true);
        }
    }

}
