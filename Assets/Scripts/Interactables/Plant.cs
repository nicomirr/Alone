using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plant : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    RoomLightStatus _roomLightStatus;

    bool _notClickable;

    bool _hasBeenWatered;
    [SerializeField] AudioClip _plantWateringSound;

    bool _firstCanBeSearchedStatusCheck;
    [SerializeField] bool _canBeSearchedInitialStatus;
    [SerializeField] bool _canBeSearched;

    [Header("Text position")]
    [SerializeField] float _textYPosZoomIn;
    [SerializeField] float _textYPosNormal;

    [Header("Character position")]
    [SerializeField] Vector3 _characterPosZoomInPlant;
    [SerializeField] Vector3 _playerScale;
    [SerializeField] Vector3 _playerScaleExit;
    Vector3 _characterPosNearPlant;

    [Header("Plant Items")]
    [SerializeField] GameObject _plantZoom;
    [SerializeField] GameObject _backButton;

    [SerializeField] Texture2D _blackPointer;


    [Header("Objects to disable")]
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] GameObject obj3;
    [SerializeField] GameObject obj4;
    [SerializeField] GameObject obj5;
    [SerializeField] GameObject obj6;
    [SerializeField] GameObject obj7;
    [SerializeField] GameObject obj8;
    [SerializeField] GameObject obj9;

    bool _notepadOpened;
    
    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    public void SaveData(ref GameData data)
    {
        if (data.hasBeenWatered.ContainsKey(_id))
        {
            data.hasBeenWatered.Remove(_id);
        }
        data.hasBeenWatered.Add(_id, _hasBeenWatered);

        if(data.firstPlantCanBeSearchedStatusCheck.ContainsKey(_id)) 
        { 
            data.firstPlantCanBeSearchedStatusCheck.Remove(_id);        
        }
        data.firstPlantCanBeSearchedStatusCheck.Add(_id, _firstCanBeSearchedStatusCheck);
    }

    public void LoadData(GameData data)
    {
        data.hasBeenWatered.TryGetValue(_id, out _hasBeenWatered);
        data.firstPlantCanBeSearchedStatusCheck.TryGetValue(_id, out _firstCanBeSearchedStatusCheck);
        _notepadOpened = data.notepadOpened;
    }
    public bool HasBeenWatered { get => _hasBeenWatered; set => _hasBeenWatered = value; }

    private void Update()
    {
        Language();
        CanBeSearchedUpdate();
    }

    void CanBeSearchedUpdate()
    {
        if (HasBeenWatered && _notepadOpened)        
            _canBeSearched = true;
                
        if(_canBeSearched)
            GetComponent<ClickableObject>().CanBeSearched = true;
    }

    public void PlayWateringSound()
    {
        AudioSource.PlayClipAtPoint(_plantWateringSound, PlayerController.Instance.transform.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;
               
        if (_canBeSearched) 
        {
            if (ButtonsGrid.Instance.GetCurrentAction() == "Search" || ButtonsGrid.Instance.GetCurrentAction() == "Buscar")
            {
                if (_notepadOpened)
                    ZoomInPlant();
                else
                {
                    if(LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText("What for?");
                    else if(LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText("¿Para qué?");
                }

            }
        }

    }

    void FirstCanBeSearchedStatus()
    {
        if (!_firstCanBeSearchedStatusCheck)
        {
            _canBeSearched = _canBeSearchedInitialStatus;
            _firstCanBeSearchedStatusCheck = true;
        }
    }

    void DisableObjects()
    {
        if (obj1 != null)
            obj1.SetActive(false);
        if (obj2 != null)
            obj2.SetActive(false);
        if (obj3 != null)
            obj3.SetActive(false);
        if (obj4 != null)
            obj4.SetActive(false);
        if (obj5 != null)
            obj5.SetActive(false);
        if (obj6 != null)
            obj6.SetActive(false);
        if (obj7 != null)
            obj7.SetActive(false);
        if (obj8 != null)
            obj8.SetActive(false);
        if (obj9 != null)
            obj9.SetActive(false);
    }

    void EnableObjects()
    {
        if (obj1 != null)
            obj1.SetActive(true);
        if (obj2 != null)
            obj2.SetActive(true);
        if (obj3 != null)
            obj3.SetActive(true);
        if (obj4 != null)
            obj4.SetActive(true);
        if (obj5 != null)
            obj5.SetActive(true);
        if (obj6 != null)
            obj6.SetActive(true);
        if (obj7 != null)
            obj7.SetActive(true);
        if (obj8 != null)
            obj8.SetActive(true);
        if (obj9 != null)
            obj9.SetActive(true);
    }

    void ZoomInPlant()
    {
        if (!_roomLightStatus.GetRoomHasLight() && !PlayerInventory.Instance.IsUsingFlashlight) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 0;

        PlayerController.Instance.SetLookingBack(false);
        PlayerController.Instance.SetLookingFront(false);
        PlayerController.Instance.transform.localScale = _playerScale;

        _characterPosNearPlant = PlayerController.Instance.transform.position;
        PlayerController.Instance.transform.position = _characterPosZoomInPlant;
        PlayerController.Instance.SetIsInteractingWithEnviroment(true);

        TextBox.Instance.transform.position = new Vector3(TextBox.Instance.transform.position.x, _textYPosZoomIn);

        Color color = _plantZoom.GetComponent<SpriteRenderer>().color;
        color.a = 1;

        DisableObjects();

        _plantZoom.GetComponent<SpriteRenderer>().color = color;
        _plantZoom.GetComponent<BoxCollider2D>().enabled = true;        
        _backButton.SetActive(true);

        MouseBehaviour.Instance.PlayerMinClickableDistance = 100;
        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);

    }

    public void BackButton()
    {
        PlayerController.Instance.transform.position = _characterPosNearPlant;
        PlayerController.Instance.transform.localScale = _playerScaleExit;
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        StartCoroutine(WaitBackButton());

        TextBox.Instance.transform.position = new Vector3(TextBox.Instance.transform.position.x, _textYPosNormal);

        Color color = _plantZoom.GetComponent<SpriteRenderer>().color;
        color.a = 0;

        EnableObjects();

        _plantZoom.GetComponent<SpriteRenderer>().color = color;
        _plantZoom.GetComponent<BoxCollider2D>().enabled = false;
        _backButton.SetActive(false);
               
        MouseBehaviour.Instance.PlayerMinClickableDistance = 3;
        Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);

    }

    IEnumerator WaitBackButton()
    {
        yield return new WaitForSeconds(0.1f);
        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 5;
    }

    void Language()
    {
        if(_backButton != null) 
        {
            if (LanguageManager.Instance.Language == "en")
                _backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
            else if (LanguageManager.Instance.Language == "es")
                _backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
        }
                
    }
}
