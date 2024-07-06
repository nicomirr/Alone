using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour,IDataPersistence
{    
    public static PlayerInventory Instance;

    GameObject _itemHold;
    GameObject _itemHolder;

    List<GameObject> _items = new List<GameObject>();
           
    [SerializeField] GameObject _flashlight;  
    [SerializeField] GameObject _glass;    
    [SerializeField] GameObject _keyParents; 
    [SerializeField] GameObject _dadsNote;
    [SerializeField] GameObject _bathroomDrainStopper;
    [SerializeField] GameObject _keyUnderStairs;
    [SerializeField] GameObject _umbrella;
    [SerializeField] GameObject _umbrellaHandle;
    [SerializeField] GameObject _keySister;
    [SerializeField] GameObject _firePoker;
    [SerializeField] GameObject _keyEntrance;
    [SerializeField] GameObject _musicSheet;

    int _itemSelected;
    ClickableObject _inventoryItem;

    bool _hasPickedUpFlashlight;

    bool _hasPickedUpSistersKey;
    bool _hasUsedSistersKey;
    bool _hasPickedUpFirePoker;

    bool _hasFlashlight;
    bool _hasGlass;
    bool _hasKeyParents;
    bool _hasDadsNote;
    bool _hasBathroomDrainStopper;
    bool _hasKeyUnderStairs;
    bool _hasUmbrella;
    bool _hasUmbrellaHandle;
    bool _hasKeySister;
    bool _hasFirePoker;
    bool _hasKeyEntrance;
    bool _hasMusicSheet;

    bool _isUsingFlashlight;
    bool _isUsingGlass;
    bool _isUsingItemMouse;
    bool _isUsingParentsKey;
    bool _isUsingBathroomDrainStopper;
    bool _isUsingKeyUnderStairs;
    bool _isUsingUmbrella;
    bool _isUsingUmbrellaHandle;
    bool _isUsingKeySister;
    bool _isUsingFirePoker;
    bool _isUsingKeyEntrance;

    [SerializeField] AudioClip _paperSound;

    bool _glassFilled;
    [SerializeField] Sprite _glassEmpty;
    [SerializeField] Sprite _glassWithWater;
    [SerializeField] AudioClip _glassFill;
    bool _fillSoundPlayed;

    [SerializeField] Texture2D _blackPointer;

    private void Awake()
    {        
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _itemHold = GameObject.Find("ItemHold");
        _itemHolder = GameObject.Find("ItemHolder");
    }

    public void SaveData(ref GameData data) 
    {       
        data.isUsingFlashlight = Instance._isUsingFlashlight;
        data.isUsingGlass = Instance._isUsingGlass;
        data.isUsingParentsKey = Instance._isUsingParentsKey;
        data.isUsingBathroomDrainStopper = Instance._isUsingBathroomDrainStopper;
        data.isUsingKeyUnderStairs = Instance._isUsingKeyUnderStairs;
        data.isUsingUmbrella = Instance._isUsingUmbrella;
        data.isUsingUmbrellaHandle = Instance._isUsingUmbrellaHandle;
        data.isUsingKeySister = Instance._isUsingKeySister;
        data.isUsingFirePoker = Instance._isUsingFirePoker;
        data.isUsingKeyEntrance = Instance._isUsingKeyEntrance;

        data.glassFilled = Instance._glassFilled;
        data.fillSoundPlayed = Instance._fillSoundPlayed;       
        data.itemSelected = Instance._itemSelected;
        data.inventoryItem = Instance._inventoryItem;

        data.hasPickedUpFlashlight = Instance._hasPickedUpFlashlight;

        data.hasPickedUpFirePoker = Instance._hasPickedUpFirePoker;

        data.hasPickedUpSistersKey = Instance._hasPickedUpSistersKey;
        data.hasUsedSistersKey = Instance._hasUsedSistersKey;

        data.hasFlashlight = Instance._hasFlashlight;
        data.hasGlass = Instance._hasGlass;
        data.hasKeyParents = Instance._hasKeyParents;
        data.hasDadsNote = Instance._hasDadsNote;
        data.hasBathroomDrainStopper = Instance._hasBathroomDrainStopper;
        data.hasKeyUnderStairs = Instance._hasKeyUnderStairs;
        data.hasUmbrella = Instance._hasUmbrella;
        data.hasUmbrellaHandle = Instance._hasUmbrellaHandle;
        data.hasKeySister = Instance._hasKeySister;
        data.hasFirePoker = Instance._hasFirePoker;
        data.hasKeyEntrance = Instance._hasKeyEntrance;
        data.hasMusicSheet = Instance.HasMusicSheet;
    }
    public void LoadData(GameData data)     
    {
        Instance._isUsingFlashlight = data.isUsingFlashlight;
        Instance._isUsingGlass = data.isUsingGlass;
        Instance._isUsingParentsKey = data.isUsingParentsKey;
        Instance._isUsingBathroomDrainStopper = data.isUsingBathroomDrainStopper;
        Instance._isUsingKeyUnderStairs = data.isUsingKeyUnderStairs;
        Instance._isUsingUmbrella = data.isUsingUmbrella;
        Instance._isUsingUmbrellaHandle = data.isUsingUmbrellaHandle;
        Instance._isUsingKeySister = data.isUsingKeySister;
        Instance._isUsingFirePoker = data.isUsingFirePoker;
        Instance._isUsingKeyEntrance = data.isUsingKeyEntrance;

        Instance._glassFilled = data.glassFilled;
        Instance._fillSoundPlayed = data.fillSoundPlayed;       
        Instance._itemSelected = data.itemSelected;
        Instance._inventoryItem = data.inventoryItem;

        Instance._hasPickedUpFlashlight = data.hasPickedUpFlashlight;

        Instance._hasPickedUpFirePoker = data.hasPickedUpFirePoker;

        Instance._hasPickedUpSistersKey = data.hasPickedUpSistersKey;
        Instance._hasUsedSistersKey = data.hasUsedSistersKey;

        Instance._hasFlashlight = data.hasFlashlight;
        Instance._hasGlass = data.hasGlass;
        Instance._hasKeyParents = data.hasKeyParents;
        Instance._hasDadsNote = data.hasDadsNote;
        Instance._hasBathroomDrainStopper = data.hasBathroomDrainStopper;
        Instance._hasKeyUnderStairs = data.hasKeyUnderStairs;
        Instance._hasUmbrella = data.hasUmbrella;
        Instance._hasUmbrellaHandle = data.hasUmbrellaHandle;
        Instance._hasKeySister = data.hasKeySister;
        Instance._hasFirePoker = data.hasFirePoker;
        Instance._hasKeyEntrance = data.hasKeyEntrance;
        Instance.HasMusicSheet = data.hasMusicSheet;
    }

    private void Start()
    {
        if (_hasFlashlight)
        {
            _items.Add(_flashlight);
        }

        if (_hasGlass)
        {            
            _items.Add(_glass);
        }

        if (_hasKeyParents)
        {
            _items.Add(_keyParents);
        }

        if (_hasDadsNote)
        {
            _items.Add(_dadsNote);
        }

        if(_hasBathroomDrainStopper)
        {
            _items.Add(_bathroomDrainStopper);
        }

        if(_hasKeyUnderStairs)
        {
            _items.Add(_keyUnderStairs);
        }

        if (_hasUmbrella)
        {
            _items.Add(_umbrella);
        }

        if (_hasUmbrellaHandle)
        {
            _items.Add(_umbrellaHandle);
        }

        if(_hasKeySister)
        {
            _items.Add(_keySister);
        }

        if(_hasFirePoker)
        {
            _items.Add(_firePoker);
            _hasPickedUpFirePoker = true;
        }

        if(_hasKeyEntrance)
        {
            _items.Add(_keyEntrance);
        }

        if(HasMusicSheet)
        {
            _items.Add(_musicSheet);
        }
    }

    private void Update()
    {
        if(ScenesInGame.Instance.GetIsEnding())
        {
            EndingStatus();
            return;
        }

        CurrentItemFix();
        FlashbackStatus();
        ClampItems();
        UpdateItems();
        Opacity();
        SetItem();
        ItemInMouseState();
        StopUsingItem();
    }

    public bool IsUsingFlashlight { get => _isUsingFlashlight; set => _isUsingFlashlight = value; }
    public bool IsUsingGlass { get => _isUsingGlass; set => _isUsingGlass = value; }
    public bool IsUsingItemMouse { get => _isUsingItemMouse; set => _isUsingItemMouse = value; }
    public bool GlassFilled { get => _glassFilled; set => _glassFilled = value; }
    public bool IsUsingParentsKey { get => _isUsingParentsKey; set => _isUsingParentsKey = value; }
    public bool IsUsingBathroomDrainStopper { get => _isUsingBathroomDrainStopper; set => _isUsingBathroomDrainStopper = value; }
    public int ItemSelected { get => _itemSelected; set => _itemSelected = value; }
    public bool IsUsingKeyUnderStairs { get => _isUsingKeyUnderStairs; set => _isUsingKeyUnderStairs = value; }
    public bool IsUsingUmbrella { get => _isUsingUmbrella; set => _isUsingUmbrella = value; }
    public bool IsUsingUmbrellaHandle { get => _isUsingUmbrellaHandle; set => _isUsingUmbrellaHandle = value; }
    public bool IsUsingKeySister { get => _isUsingKeySister; set => _isUsingKeySister = value; }
    public bool IsUsingFirePoker { get => _isUsingFirePoker; set => _isUsingFirePoker = value; }
    public bool IsUsingKeyEntrance { get => _isUsingKeyEntrance; set => _isUsingKeyEntrance = value; }
    public bool HasUsedSistersKey { get => _hasUsedSistersKey; set => _hasUsedSistersKey = value; }
    public bool HasPickedUpFirePoker { get => _hasPickedUpFirePoker; set => _hasPickedUpFirePoker = value; }
    public bool HasKeyEntrance { get => _hasKeyEntrance; set => _hasKeyEntrance = value; }
    public bool HasMusicSheet { get => _hasMusicSheet; set => _hasMusicSheet = value; }

    public GameObject GetCurrentItem() { return _items[_itemSelected];}

    public int GetItemListLenght() { return _items.Count; }
       
    public void AddItem(GameObject item) 
    { 
        _items.Add(item);

        while (CurrentItemName() != item.name)
            _itemSelected++;
                

        if (item.name == "FlashlightInventory")
        {
            _hasFlashlight = true;
            _hasPickedUpFlashlight = true;
        }
        else if (item.name == "GlassInventory")
        {
            _hasGlass = true;
        }
        else if (item.name == "KeyParentsInventory")
        {
            _hasKeyParents = true;
        }
        else if (item.name == "DadsNoteInventory")
        {
            _hasDadsNote = true;
        }
        else if (item.name == "DrainStopperInventory")
        {
            _hasBathroomDrainStopper = true;
        }
        else if (item.name == "KeyUnderStairsInventory")
        {
            _hasKeyUnderStairs = true;
        }
        else if (item.name == "UmbrellaInventory")
        {
            _hasUmbrella = true;
        }
        else if (item.name == "UmbrellaHandleInventory")
        {
            _hasUmbrellaHandle = true;
        }
        else if(item.name == "KeySisterInventory")
        {
            _hasKeySister = true;
            _hasPickedUpSistersKey = true;
        }
        else if(item.name == "PokerInventory")
        {
            _hasFirePoker = true;
            _hasPickedUpFirePoker = true;
        }
        else if(item.name == "KeyEntranceInventory")
        {
            _hasKeyEntrance = true;
        }
        else if(item.name == "MusicSheetInventory")
        {
            _hasMusicSheet = true;
        }
    }

    public void RemoveItem(int index) 
    {
        if (_items[index].name == "FlashlightInventory")
        {
            _hasFlashlight = false;
        }
        else if (_items[index].name == "GlassInventory")
        {
            _hasGlass = false;
        }
        else if (_items[index].name == "KeyParentsInventory")
        {
            _hasKeyParents = false;
        }
        else if (_items[index].name == "DadsNoteInventory")
        {
            _hasDadsNote = false;
        }
        else if (_items[index].name == "DrainStopperInventory")
        {
            _hasBathroomDrainStopper = false;
            _isUsingBathroomDrainStopper = false;
            _isUsingBathroomDrainStopper = false;
        }
        else if (_items[index].name == "KeyUnderStairsInventory")
        {
            _hasKeyUnderStairs = false;
            _isUsingKeyUnderStairs = false;
            _isUsingItemMouse = false;
        }
        else if (_items[index].name == "UmbrellaInventory")
        {
            _hasUmbrella = false;
            _isUsingUmbrella = false;
            _isUsingItemMouse = false;
        }
        else if (_items[index].name == "UmbrellaHandleInventory")
        {
            _hasUmbrellaHandle = false;
            _isUsingUmbrellaHandle = false;
            _isUsingItemMouse = false;
        }
        else if (_items[index].name == "KeySisterInventory")
        {
            _hasKeySister = false;
            _isUsingKeySister = false;
            _isUsingItemMouse = false;
        }
        else if (_items[index].name == "PokerInventory")
        {
            _hasFirePoker = false;
            _isUsingFirePoker = false;
            _isUsingItemMouse = false;
        }
        else if (_items[index].name == "KeyEntranceInventory")
        {
            _hasKeyEntrance = false;
            _isUsingKeyEntrance = false;
            _isUsingItemMouse = false;
        }
        else if (_items[index].name == "MusicSheetInventory")
        {
            _hasMusicSheet = false;            
        }

        _items.RemoveAt(index);
       
    }

    void CurrentItemFix()
    {
        if(ItemSelected < 0 || ItemSelected >= _items.Count)
            ItemSelected = 0;
    }

    void StopUsingItem()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {            
            _isUsingGlass = false;
            _isUsingItemMouse = false;
            _isUsingParentsKey = false;
            _isUsingBathroomDrainStopper = false;
            _isUsingKeyUnderStairs = false;
            _isUsingUmbrella = false;
            _isUsingUmbrellaHandle = false;
            _isUsingKeySister = false;
            _isUsingFirePoker = false;
            _isUsingKeyEntrance = false;
        }
    }

    void FlashbackStatus()
    {
        if(ScenesInGame.Instance.GetIsFlashback())
        {      
            _items.Remove(_glass);
            _items.Remove(_dadsNote);      
            _items.Remove(_umbrellaHandle);

            if (_hasPickedUpFlashlight)
                _items.Remove(_flashlight);

            if (_hasPickedUpSistersKey && !_hasUsedSistersKey)
                _items.Remove(_keySister);
        }
    }

    void EndingStatus()
    {
        if(ScenesInGame.Instance.GetLoopEnding())
            _itemHolder.SetActive(false);
        else if(ScenesInGame.Instance.GetTruthEnding() && SceneManager.GetActiveScene().name != "Attic")
            _itemHolder.SetActive(false);
    }

    public void AfterFlashbackStatus()
    {
        _items.Add(_umbrellaHandle);
        _items.Add(_glass);
        _items.Add(_dadsNote);

        if (_hasPickedUpFlashlight)
            _items.Add(_flashlight);

        if (_hasPickedUpSistersKey && !_hasUsedSistersKey)
            _items.Add(_keySister);
    }

    public void DestroyCurrentItem()
    {
        int itemToDestroy = PlayerInventory.Instance.ItemSelected;

        PlayerInventory.Instance.ItemSelected = 0;
        PlayerInventory.Instance.IsUsingItemMouse = false;
        PlayerInventory.Instance.IsUsingParentsKey = false;
        PlayerInventory.Instance.RemoveItem(itemToDestroy);
    }

    public void NextItem()
    {
        if (Pause.Instance.IsPaused) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;
        if (PlayerController.Instance.GetIsReading()) return;
        if (PlayerController.Instance.GetMustHide()) return;
                
        _isUsingGlass = false;
        _isUsingItemMouse = false;
        _isUsingParentsKey = false;
        _isUsingBathroomDrainStopper = false;
        _isUsingKeyUnderStairs = false;
        _isUsingUmbrella = false;
        _isUsingUmbrellaHandle = false;
        _isUsingKeySister = false;
        _isUsingFirePoker = false;
        _isUsingKeyEntrance = false;

        if (_itemSelected < _items.Count - 1)
            _itemSelected++;
        else
            _itemSelected = 0;     

    }

    public void PreviousItem()
    {
        if (Pause.Instance.IsPaused) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;
        if (PlayerController.Instance.GetIsReading()) return;
        if (PlayerController.Instance.GetMustHide()) return;

        _isUsingGlass = false;
        _isUsingItemMouse = false;
        _isUsingParentsKey = false;
        _isUsingBathroomDrainStopper = false;
        _isUsingKeyUnderStairs= false;
        _isUsingUmbrella = false;
        _isUsingUmbrellaHandle = false;
        _isUsingKeySister = false;
        _isUsingFirePoker = false;
        _isUsingKeyEntrance = false;

        if (_itemSelected > 0)
            _itemSelected--;
        else
            _itemSelected = _items.Count - 1;
    }

    public string CurrentItemName() { return _items[_itemSelected].name; }

    void UpdateItems()
    {
        if (_items.Count != 0)
        {
            if (CurrentItemName() == "GlassInventory")
            {                
                if (_glassFilled)
                {

                    if(LanguageManager.Instance.Language == "en")
                        GetCurrentItem().GetComponent<ClickableObject>().LookAtText = "It's filled with water.";
                    else if (LanguageManager.Instance.Language == "es")                      
                        GetCurrentItem().GetComponent<ClickableObject>().LookAtTextSpanish = "Está lleno de agua.";
                    
                    GetCurrentItem().GetComponent<SpriteRenderer>().sprite = _glassWithWater;

                    Debug.Log(GetCurrentItem().GetComponent<ClickableObject>().LookAtText);

                    if(!_fillSoundPlayed)
                    {
                        AudioSource.PlayClipAtPoint(_glassFill, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
                        _fillSoundPlayed = true;
                    }                    
                }
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        GetCurrentItem().GetComponent<ClickableObject>().LookAtText = "It's an empty glass.";
                    else if (LanguageManager.Instance.Language == "es")
                        GetCurrentItem().GetComponent<ClickableObject>().LookAtTextSpanish = "Es un vaso vacío.";

                    GetCurrentItem().GetComponent<SpriteRenderer>().sprite = _glassEmpty;

                    _fillSoundPlayed = false;
                }
            }

        }
    }

    void ClampItems()
    {       
        if(_itemSelected < 0) { _itemSelected = 0; }
    }
    private void Opacity()
    {
        if (_itemHold == null) { return; }

        Color color = _itemHold.GetComponent<UnityEngine.UI.Image>().color;

        if (_items.Count == 0)
            color.a = 0f;
        else
            color.a = 1f;

        _itemHold.GetComponent<UnityEngine.UI.Image>().color = color;
    }

    private void SetItem()
    {
        if (_items.Count == 0) { return; }

        _itemHold.GetComponent<UnityEngine.UI.Image>().sprite = _items[_itemSelected].GetComponent<SpriteRenderer>().sprite;

        _inventoryItem = _items[_itemSelected].GetComponent<ClickableObject>();
        _itemHold.GetComponent<ClickableObject>().SetClickableObject(_inventoryItem);

    }

    private void ItemInMouseState()
    {
        if (ButtonsGrid.Instance.GetCurrentAction() != "Use" && ButtonsGrid.Instance.GetCurrentAction() != "Usar")
        {
            _isUsingGlass = false;
            _isUsingParentsKey = false;
            _isUsingBathroomDrainStopper = false;
            _isUsingKeyUnderStairs = false;
            _isUsingUmbrella = false;
            _isUsingUmbrellaHandle = false;
            _isUsingKeySister = false;
            _isUsingKeyEntrance = false;
            _isUsingItemMouse = false;
        }
               
    }
          
    public void InteractWithItem()
    {
        if (Pause.Instance.IsPaused) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;
        if (GetItemListLenght() == 0) return;
        if (PlayerController.Instance.GetMustHide()) return;

        if (CurrentItemName() == "FlashlightInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;

            if (PlayerController.Instance.GetIsInteractingWithEnviroment()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {         
                _isUsingFlashlight = !_isUsingFlashlight;
                PlayerController.Instance.FlashlightSound();
            }
            else if (ButtonsGrid.Instance.GetCurrentAction() == "Turn On" || ButtonsGrid.Instance.GetCurrentAction() == "Encender")
            {

                if (_isUsingFlashlight)
                {
                    if(LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText("It's already on.", true, _inventoryItem);
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText("Ya está encendida.", true, _inventoryItem);
                }
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText("I will turn it on when I am using it.", true, _inventoryItem);
                    else if(LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText("La encenderé cuando la esté utilizando.", true, _inventoryItem);
                }
            }
            else if (ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar")
            {
                if (!_isUsingFlashlight)
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText("It's already off.", true, _inventoryItem);
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText("Ya está apagada.", true, _inventoryItem);
                }                    
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText("I will turn it off when I am not using it.", true, _inventoryItem);
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText("La apagaré cuando no la esté utilizando.", true, _inventoryItem);
                }
                    
            }

        }
        else if(CurrentItemName() == "GlassInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;
                        
            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {                
                _isUsingGlass = !_isUsingGlass;
                _isUsingItemMouse = _isUsingGlass;                                                                      
            }
        }
        else if(CurrentItemName() == "KeyParentsInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingParentsKey = !_isUsingParentsKey;
                _isUsingItemMouse = _isUsingParentsKey;
            }
        }
        else if(CurrentItemName() == "DadsNoteInventory")
        {
            if (PlayerController.Instance.GetIsInteractingWithEnviroment()) return;
                        
            if (ButtonsGrid.Instance.GetCurrentAction() == "Read" || ButtonsGrid.Instance.GetCurrentAction() == "Leer")
            {
                AudioSource.PlayClipAtPoint(_paperSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

                PlayerController.Instance.SetIsReading(true);                                          
                GameObject.Find("DadsNote").transform.GetChild(0).gameObject.SetActive(true);                              
            }
                        
        }
        else if(CurrentItemName() == "DrainStopperInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingBathroomDrainStopper = !_isUsingBathroomDrainStopper;
                _isUsingItemMouse = _isUsingBathroomDrainStopper;
            }
        }
        else if (CurrentItemName() == "KeyUnderStairsInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingKeyUnderStairs = !_isUsingKeyUnderStairs;
                _isUsingItemMouse = _isUsingKeyUnderStairs;
            }
        }
        else if (CurrentItemName() == "UmbrellaInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingUmbrella = !_isUsingUmbrella;
                _isUsingItemMouse = _isUsingUmbrella;
            }
        }
        else if (CurrentItemName() == "UmbrellaHandleInventory")
        {
            if (PlayerController.Instance.GetIsReading()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingUmbrellaHandle = !_isUsingUmbrellaHandle;
                _isUsingItemMouse = _isUsingUmbrellaHandle;
            }
        }
        else if(CurrentItemName() == "KeySisterInventory")
        {
            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingKeySister = !_isUsingKeySister;
                _isUsingItemMouse = _isUsingKeySister;
            }
        }
        else if (CurrentItemName() == "PokerInventory")
        {
            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingFirePoker = !_isUsingFirePoker;
                _isUsingItemMouse = _isUsingFirePoker;
            }
        }
        else if (CurrentItemName() == "KeyEntranceInventory")
        {
            if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
            {
                _isUsingKeyEntrance = !_isUsingKeyEntrance;
                _isUsingItemMouse = _isUsingKeyEntrance;
            }
        }
        else if (CurrentItemName() == "MusicSheetInventory")
        {
            if (PlayerController.Instance.GetIsInteractingWithEnviroment()) return;

            if (ButtonsGrid.Instance.GetCurrentAction() == "Read" || ButtonsGrid.Instance.GetCurrentAction() == "Leer")
            {
                AudioSource.PlayClipAtPoint(_paperSound, PlayerController.Instance.transform.position);

                PlayerController.Instance.SetIsReading(true);
                GameObject.Find("MusicSheet").transform.GetChild(0).gameObject.SetActive(true);
            }

        }
    }
   
}


//CAMBIAR TODO EL SISTEMA DE INVENTARIO. QUE CADA OBJETO TENGA SU SCRIPT, Y EJECUTAR SUS ACCIONES DESDE ESTOS.
//NO NECESARIAMENTE TIENE QUE SER UN BOTON EL OBJETO DEL INVENTARIO.


//CHEQUEAR SI AGARRO LA LLAVE DE LA HERMANA. SI LA USO. Y SI NO LA USO SACARLA Y AGREGARLA EN FLASHBACK.