using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class ClickableObject : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    bool _textFirstSetup;

    [Header("English")]
    [SerializeField] string _firstLookAtText;
    [SerializeField] string _firstTurnOnText;
    [SerializeField] string _firstTurnOffText;
    [SerializeField] string _firstPickupText;
    [SerializeField] string _firstSearchText;
    [SerializeField] string _firstReadText;
    [SerializeField] string _firstOpenText;
    [SerializeField] string _firstCloseText;
    [SerializeField] string _firstUseText;
    [SerializeField] string _firstHasObjectText;
    [SerializeField] string _firstHideText;

    [Header("Spanish")]
    [SerializeField] string _firstLookAtTextSpanish;
    [SerializeField] string _firstTurnOnTextSpanish;
    [SerializeField] string _firstTurnOffTextSpanish;
    [SerializeField] string _firstPickupTextSpanish;
    [SerializeField] string _firstSearchTextSpanish;
    [SerializeField] string _firstReadTextSpanish;
    [SerializeField] string _firstOpenTextSpanish;
    [SerializeField] string _firstCloseTextSpanish;
    [SerializeField] string _firstUseTextSpanish;
    [SerializeField] string _firstHasObjectTextSpanish;
    [SerializeField] string _firstHideTextSpanish;
    [SerializeField] string _hasObjectTextSpanish;
    [SerializeField] string _hideTextSpanish;
    string _noLightTextSpanish = "Está muy oscuro para ver.";

    [Header("Spanish texts inventory")]
    [SerializeField] string _lookAtTextSpanish;
    [SerializeField] string _turnOnTextSpanish;
    [SerializeField] string _turnOffTextSpanish;
    [SerializeField] string _pickupTextSpanish;
    [SerializeField] string _searchTextSpanish;
    [SerializeField] string _readTextSpanish;
    [SerializeField] string _openTextSpanish;
    [SerializeField] string _closeTextSpanish;
    [SerializeField] string _useTextSpanish; 
   
    [Header("Texts")]
    [SerializeField] string _lookAtText;
    [SerializeField] string _turnOnText;
    [SerializeField] string _turnOffText;
    [SerializeField] string _pickupText;
    [SerializeField] string _searchText;
    [SerializeField] string _readText;
    [SerializeField] string _openText;
    [SerializeField] string _closeText;
    [SerializeField] string _useText;
    [SerializeField] string _hasObjectText;
    [SerializeField] string _hideText;
    string _noLightText = "It's too dark to see.";

    [Header("Type of Object")]
    [SerializeField] bool _isLight;
    [SerializeField] bool _isInventoryObject;    

    [Header("Type of Actions")]
    [SerializeField] bool _canBeUsed;
    [SerializeField] bool _canBeTurnedOnOff;
    [SerializeField] bool _canBeOpened;
    [SerializeField] bool _canBeSearched;
    [SerializeField] bool _canBePickedUp;
    [SerializeField] bool _canBeRead;
    [SerializeField] bool _hiddingSpot;


    [Header("Object")]
    [SerializeField] bool _hasObject;
    [SerializeField] GameObject _item;

    [Header("InitialStatus")]
    [SerializeField] bool _canBeSearchedInitialStatus;
    [SerializeField] bool _hasObjectInitialStatus;
       
    bool _firstCanBeSearchedStatusCheck;
    bool _firstHasObjectStatusCheck;
            
    string _currentAction;

    bool _hasBeenPickedUp;
  
    bool _notClickable;
           
    RoomLightStatus _roomLightStatus;

      
    private void Awake()
    {      
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();       
    }

    public void SaveData(ref GameData data)
    {
        if (data.textFirstSetup.ContainsKey(_id))
        {
            data.textFirstSetup.Remove(_id);
        }
        data.textFirstSetup.Add(_id, _textFirstSetup);

        if (data.lookAtText.ContainsKey(_id))
        {
            data.lookAtText.Remove(_id);
        }
        data.lookAtText.Add(_id, _lookAtText);

        if (data.turnOnText.ContainsKey(_id))
        {
            data.turnOnText.Remove(_id);
        }
        data.turnOnText.Add(_id, _turnOnText);

        if (data.turnOffText.ContainsKey(_id))
        {
            data.turnOffText.Remove(_id);
        }
        data.turnOffText.Add(_id, _turnOffText);

        if (data.pickupText.ContainsKey(_id))
        {
            data.pickupText.Remove(_id);
        }
        data.pickupText.Add(_id, _pickupText);

        if (data.searchText.ContainsKey(_id))
        {
            data.searchText.Remove(_id);
        }
        data.searchText.Add(_id, _searchText);

        if (data.readText.ContainsKey(_id))
        {
            data.readText.Remove(_id);
        }
        data.readText.Add(_id, _readText);

        if (data.openText.ContainsKey(_id))
        {
            data.openText.Remove(_id);
        }
        data.openText.Add(_id, _openText);

        if (data.closeText.ContainsKey(_id))
        {
            data.closeText.Remove(_id);
        }
        data.closeText.Add(_id, _closeText);

        if (data.useText.ContainsKey(_id))
        {
            data.useText.Remove(_id);
        }
        data.useText.Add(_id, _useText);

        if (data.hasObjectText.ContainsKey(_id))
        {
            data.hasObjectText.Remove(_id);
        }
        data.hasObjectText.Add(_id, _hasObjectText);

        if (data.hideText.ContainsKey(_id))
        {
            data.hideText.Remove(_id);
        }
        data.hideText.Add(_id, _hideText);
               
        if (data.hasObject.ContainsKey(_id)) 
        { 
            data.hasObject.Remove(_id);
        }
        data.hasObject.Add(_id, _hasObject);

        if (data.firstHasObjectStatusCheck.ContainsKey(_id)) 
        { 
            data.firstHasObjectStatusCheck.Remove(_id);
        }
        data.firstHasObjectStatusCheck.Add(_id, _firstHasObjectStatusCheck);

        if (data.hasBeenPickedUp.ContainsKey(_id))
        {
            data.hasBeenPickedUp.Remove(_id);
        }
        data.hasBeenPickedUp.Add(_id, _hasBeenPickedUp);

        if (data.canBeSearched.ContainsKey(_id))
        {
            data.canBeSearched.Remove(_id);
        }
        data.canBeSearched.Add(_id, _canBeSearched);

        if (data.canBeUsed.ContainsKey(_id))
        {
            data.canBeUsed.Remove(_id);
        }
        data.canBeUsed.Add(_id, _canBeUsed);

        if (data.firstCanBeSearchedStatusCheck.ContainsKey(_id))
        {
            data.firstCanBeSearchedStatusCheck.Remove(_id);
        }
        data.firstCanBeSearchedStatusCheck.Add(_id, _firstCanBeSearchedStatusCheck);
    }

    public void LoadData(GameData data)
    {
        data.textFirstSetup.TryGetValue(_id, out _textFirstSetup);
        data.lookAtText.TryGetValue(_id, out _lookAtText);
        data.turnOnText.TryGetValue(_id, out _turnOnText);
        data.turnOffText.TryGetValue(_id, out _turnOffText);
        data.pickupText.TryGetValue(_id, out _pickupText);
        data.searchText.TryGetValue(_id, out _searchText);
        data.readText.TryGetValue(_id, out _readText);
        data.openText.TryGetValue(_id, out _openText);
        data.closeText.TryGetValue(_id, out _closeText);
        data.useText.TryGetValue(_id, out _useText);    
        data.hasObjectText.TryGetValue(_id, out _hasObjectText);
        data.hideText.TryGetValue(_id, out _hideText);
        data.hasObject.TryGetValue(_id, out _hasObject);
        data.firstHasObjectStatusCheck.TryGetValue(_id, out _firstHasObjectStatusCheck);
        data.hasBeenPickedUp.TryGetValue(_id, out _hasBeenPickedUp);
        data.canBeSearched.TryGetValue(_id, out _canBeSearched);
        data.canBeUsed.TryGetValue(_id, out _canBeUsed);    
        data.firstCanBeSearchedStatusCheck.TryGetValue(_id, out _firstCanBeSearchedStatusCheck);
    }
    public string LookAtText { get => _lookAtText; set => _lookAtText = value; }
    public string TurnOnText { get => _turnOnText; set => _turnOnText = value; }
    public string TurnOffText { get => _turnOffText; set => _turnOffText = value; }
    public string PickupText { get => _pickupText; set => _pickupText = value; }
    public string SearchText { get => _searchText; set => _searchText = value; }
    public string ReadText { get => _readText; set => _readText = value; }
    public string OpenText { get => _openText; set => _openText = value; }
    public string CloseText { get => _closeText; set => _closeText = value; }
    public string UseText { get => _useText; set => _useText = value; }
    public string HasObjectText { get => _hasObjectText; set => _hasObjectText = value; }
    public bool Light { get => _isLight; set => _isLight = value; }
    public bool InventoryObject { get => _isInventoryObject; set => _isInventoryObject = value; }
    public bool CanBeUsed { get => _canBeUsed; set => _canBeUsed = value; }
    public bool CanBeTurnedOnOff { get => _canBeTurnedOnOff; set => _canBeTurnedOnOff = value; }
    public bool NotClickable { get => _notClickable; set => _notClickable = value; }
    public bool CanBeSearched { get => _canBeSearched; set => _canBeSearched = value; }
    public string LookAtTextSpanish { get => _lookAtTextSpanish; set => _lookAtTextSpanish = value; }
    public string TurnOnTextSpanish { get => _turnOnTextSpanish; set => _turnOnTextSpanish = value; }
    public string TurnOffTextSpanish { get => _turnOffTextSpanish; set => _turnOffTextSpanish = value; }
    public string PickupTextSpanish { get => _pickupTextSpanish; set => _pickupTextSpanish = value; }
    public string SearchTextSpanish { get => _searchTextSpanish; set => _searchTextSpanish = value; }
    public string ReadTextSpanish { get => _readTextSpanish; set => _readTextSpanish = value; }
    public string OpenTextSpanish { get => _openTextSpanish; set => _openTextSpanish = value; }
    public string CloseTextSpanish { get => _closeTextSpanish; set => _closeTextSpanish = value; }
    public string UseTextSpanish { get => _useTextSpanish; set => _useTextSpanish = value; }

    private void Update()
    {
        TextSetup();

        if(_hasBeenPickedUp) 
            this.gameObject.SetActive(false);
                
        UpdateAction();
        FirstHasObjectStatus();
        FirstCanBeSearchedStatus();
    }   

    void TextSetup()
    {
        if(LanguageManager.Instance.LanguageChanged)
        {
            LanguageManager.Instance.LanguageChanged = false;
            _textFirstSetup = false;
        }

        if(!_textFirstSetup)
        {            
            if(LanguageManager.Instance.Language == "en")
            {
                _lookAtText = _firstLookAtText;
                _turnOnText = _firstTurnOnText;
                _turnOffText = _firstTurnOffText;
                _pickupText = _firstPickupText;
                _searchText = _firstSearchText;
                _readText = _firstReadText;
                _openText = _firstOpenText;
                _closeText = _firstCloseText;
                _useText = _firstUseText;
                _hasObjectText = _firstHasObjectText;
                _hideText = _firstHideText;
            }
            else if (LanguageManager.Instance.Language == "es")
            {
                _lookAtText = _firstLookAtTextSpanish;
                _turnOnText = _firstTurnOnTextSpanish;
                _turnOffText = _firstTurnOffTextSpanish;
                _pickupText = _firstPickupTextSpanish;
                _searchText = _firstSearchTextSpanish;
                _readText = _firstReadTextSpanish;
                _openText = _firstOpenTextSpanish;
                _closeText = _firstCloseTextSpanish;
                _useText = _firstUseTextSpanish;
                _hasObjectText = _firstHasObjectTextSpanish;
                _hideText = _firstHideTextSpanish;
            }

            _textFirstSetup = true;
        }
    }
       
    public void SetClickableObject(ClickableObject clickableObject)
    {
        if (LanguageManager.Instance.Language == "en")
        {
            _lookAtText = clickableObject.LookAtText;
            _readText = clickableObject.ReadText;
            _turnOnText = clickableObject.TurnOnText;
            _turnOffText = clickableObject.TurnOffText;
            _pickupText = clickableObject.PickupText;
            _searchText = clickableObject.SearchText;
            _openText = clickableObject.OpenText;
            _closeText = clickableObject.CloseText;
            _useText = clickableObject.UseText;
            _hasObjectText = clickableObject.HasObjectText;
            _canBeUsed = clickableObject.CanBeUsed;
            _canBeTurnedOnOff = clickableObject.CanBeTurnedOnOff;
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _lookAtText = clickableObject.LookAtTextSpanish;
            _readText = clickableObject.ReadTextSpanish;
            _turnOnText = clickableObject.TurnOnTextSpanish;
            _turnOffText = clickableObject.TurnOffTextSpanish;
            _pickupText = clickableObject.PickupTextSpanish;
            _searchText = clickableObject.SearchTextSpanish;
            _openText = clickableObject.OpenTextSpanish;
            _closeText = clickableObject.CloseTextSpanish;
            _useText = clickableObject.UseTextSpanish;
            _hasObjectText = clickableObject.HasObjectText;
            _canBeUsed = clickableObject.CanBeUsed;
            _canBeTurnedOnOff = clickableObject.CanBeTurnedOnOff;
        }
    }

    void UpdateAction()
    {
        _currentAction = ButtonsGrid.Instance.GetCurrentAction();
    }

    void FirstHasObjectStatus()
    {
        if (!_firstHasObjectStatusCheck)
        {
            _hasObject = _hasObjectInitialStatus;
            _firstHasObjectStatusCheck = true;
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

    public void OnPointerClick(PointerEventData eventData)
    {        
        Action();
    }
       
    public void Action()
    {       
        if (Pause.Instance.IsPaused) return;      
        if (PlayerController.Instance.GetIsReading()) return;       
        if (PlayerController.Instance.GetOnLockedDoor()) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;
        if (PlayerController.Instance.GetIsHidding()) return;
        if (_isInventoryObject && PlayerInventory.Instance.GetItemListLenght() == 0) return;
        if (!_isInventoryObject)
            _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable && !_isInventoryObject) return;

        bool roomHasLight = _roomLightStatus.GetRoomHasLight();


        if (PlayerController.Instance.GetMustHide())
        {
            Debug.Log("Entra");

            if (_isInventoryObject) return;
            Debug.Log("Entra");

            if (_hiddingSpot) return;

            Debug.Log("Entra");


            TextBox.Instance.ShowText(_hideText);

            Debug.Log(_hideText);

            return;
        }

        if (PlayerController.Instance.GetMustEscape())
        {
            if (_isInventoryObject) return;

            if (LanguageManager.Instance.Language == "en")
                TextBox.Instance.ShowText("There's no time for that.");
            else if (LanguageManager.Instance.Language == "es")
                TextBox.Instance.ShowText("No hay tiempo para eso.");

            return;
        }

        if (!PlayerInventory.Instance.IsUsingItemMouse)
        {         
            if(_currentAction == "Look At" || _currentAction == "Mirar")
            {                
                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_lookAtText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if(LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Turn On" || _currentAction == "Encender")
            {
                if (_isLight) return;
                if (_canBeTurnedOnOff && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight)) return;
                                
                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_turnOnText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Turn Off" || _currentAction == "Apagar")
            {
                if (_isLight) return;
                if (_canBeTurnedOnOff && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight)) return;
                                
                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_turnOffText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Pick Up" || _currentAction == "Agarrar")
            {
                if (_canBePickedUp && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight))
                {
                    PlayerInventory.Instance.AddItem(_item);
                    _hasBeenPickedUp = true;
                    this.gameObject.SetActive(false);
                    return;
                }
                                
                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_pickupText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Search" || _currentAction == "Buscar")
            {
                if (_canBeSearched && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight)) return;
                                
                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                {
                    if (_hasObject)
                    {
                        TextBox.Instance.ShowText(_hasObjectText, _isInventoryObject, GetComponent<ClickableObject>());
                        PlayerInventory.Instance.AddItem(_item);
                        _hasObject = false;
                    }
                    else
                        TextBox.Instance.ShowText(_searchText, _isInventoryObject, GetComponent<ClickableObject>());
                }
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Read" || _currentAction == "Leer")
            {
                if (_canBeRead && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight)) return;
                                
                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_readText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Open" || _currentAction == "Abrir")
            {
                if (_canBeOpened && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight)) return;

                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_openText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Close" || _currentAction == "Cerrar")
            {
                if (_canBeOpened && (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight)) return;

                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_closeText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }
            }
            else if(_currentAction == "Use" || _currentAction == "Usar")
            {
                if (_canBeUsed) return;

                if (roomHasLight || PlayerInventory.Instance.IsUsingFlashlight || _isInventoryObject)
                    TextBox.Instance.ShowText(_useText, _isInventoryObject, GetComponent<ClickableObject>());
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText(_noLightText, _isInventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText(_noLightTextSpanish, _isInventoryObject, GetComponent<ClickableObject>());
                }

            }            
 
        }
        
    }
        
}
