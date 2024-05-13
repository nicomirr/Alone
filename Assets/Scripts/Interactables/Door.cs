using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IDataPersistence, IPointerClickHandler
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    [Header("Key")]
    [SerializeField] string _doorKeyName;

    [Header("Sounds")]
    [SerializeField] AudioClip _doorOpenSound;
    [SerializeField] AudioClip _doorCloseSound;
    [SerializeField] AudioClip _stairsSound;

    [Header("Scene")]
    [SerializeField] string _newSceneName;

    [Header("Player")]
    [SerializeField] Vector2 _playerPosition;
    [SerializeField] float _playerScale;
    [SerializeField] bool _playerIsFront;
    [SerializeField] bool _playerIsBack;
    TextMeshPro _playerText;


    [Header("Door Type")]
    [SerializeField] string _doorType;  
    [SerializeField] bool _isStairs;         

    [Header("Door Status")]
    bool _isOpeningDoor = false;
    bool _firstIsLockedStatusCheck;
    [SerializeField] bool _isLockedInitialStatus;
    [SerializeField] bool _isLocked;
    [SerializeField] AudioClip _doorUnlocked;

    [Header("Cinematics")]
    [SerializeField] bool _firstDinningRoomScene;
    [SerializeField] bool _firstKitchenScene;
    [SerializeField] bool _firstParentsRoomScene;

    [Header("English")]
    [SerializeField] List<string> _lockedTexts;
    [SerializeField] List<string> _hideTexts;
    [SerializeField] List<string> _mustEscapeTexts;

    [Header("Spanish")]
    [SerializeField] List<string> _spanishLockedTexts;
    [SerializeField] List<string> _spanishHideTexts;
    [SerializeField] List<string> _spanishMustEscapeTexts;


    Animator _fadeAnimator;

    [SerializeField] string _continueAtTheseScene;

    [SerializeField] AudioSource searchingSound;
    bool textAdded;

    AudioSource _audioSource;


    private void Awake()
    {
        _playerText = GameObject.Find("PlayerText").GetComponent<TextMeshPro>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _fadeAnimator = GameObject.Find("BlackImage").GetComponent<Animator>();        
    }
   
    public void SaveData(ref GameData data)
    {
        if (data.isLocked.ContainsKey(_id))
        {
            data.isLocked.Remove(_id);
        }
        data.isLocked.Add(_id, _isLocked);

        if (data.firstIsLockedStatusCheck.ContainsKey(_id))
        {
            data.firstIsLockedStatusCheck.Remove(_id);
        }
        data.firstIsLockedStatusCheck.Add(_id, _firstIsLockedStatusCheck);
                
    }

    public void LoadData(GameData data)
    {
        data.isLocked.TryGetValue(_id, out _isLocked);
        data.firstIsLockedStatusCheck.TryGetValue(_id, out _firstIsLockedStatusCheck);
    }
        
    public bool GetIsOpeningDoor() { return _isOpeningDoor; }
    public string GetNewSceneName() { return _newSceneName; }

    private void Update()
    {
        FirstIsLockedStatus();
        Cinematics();                
    }

    void FirstIsLockedStatus()
    {
        if (!_firstIsLockedStatusCheck)
        {
            _isLocked = _isLockedInitialStatus;
            _firstIsLockedStatusCheck = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        UnlockDoor();
    }

    public void OpenDoor()
    {         
        if (_doorType == "sideDoor")
        {
            if(!PlayerController.Instance.GetLookingFront() && !PlayerController.Instance.GetLookingBack()) 
            {
                if (PlayerController.Instance.GetMustEscape())
                {
                    if (_newSceneName != "Stairs2ndFloor" && _newSceneName != "EntranceAndStairs")
                    {
                        if (LanguageManager.Instance.Language == "en")
                            StartCoroutine(CheckDoor(_mustEscapeTexts));
                        else if (LanguageManager.Instance.Language == "es")
                            StartCoroutine(CheckDoor(_spanishMustEscapeTexts));

                        return;
                    }
                }

                if (PlayerController.Instance.GetMustHide())
                {
                    if (LanguageManager.Instance.Language == "en")
                        StartCoroutine(CheckDoor(_hideTexts));
                    else if (LanguageManager.Instance.Language == "es")
                        StartCoroutine(CheckDoor(_spanishHideTexts));

                    return;
                }

                if (!_isLocked)
                {
                    _isOpeningDoor = true;
                    StartCoroutine(ChangeRoom());
                }
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        StartCoroutine(CheckDoor(_lockedTexts));
                    else if (LanguageManager.Instance.Language == "es")
                        StartCoroutine(CheckDoor(_spanishLockedTexts));
                }
            }
                       
        }
        else if (_doorType == "frontDoor")
        {
            if (PlayerController.Instance.GetLookingFront())
            {
                if(PlayerController.Instance.GetMustEscape())
                {
                    if(_newSceneName != "Stairs2ndFloor" && _newSceneName != "EntranceAndStairs")
                    {
                        if (LanguageManager.Instance.Language == "en")
                            StartCoroutine(CheckDoor(_mustEscapeTexts));
                        else if (LanguageManager.Instance.Language == "es")
                            StartCoroutine(CheckDoor(_spanishMustEscapeTexts));

                        return;
                    }                                       
                }

                if(PlayerController.Instance.GetMustHide())
                {
                    if (LanguageManager.Instance.Language == "en")
                        StartCoroutine(CheckDoor(_hideTexts));
                    else if (LanguageManager.Instance.Language == "es")
                        StartCoroutine(CheckDoor(_spanishHideTexts));

                    return;
                }                               

                if(!_isLocked)
                {
                    _isOpeningDoor = true;
                    StartCoroutine(ChangeRoom());
                }
                else
                {
                    if (LanguageManager.Instance.Language == "en")
                        StartCoroutine(CheckDoor(_lockedTexts));
                    else if (LanguageManager.Instance.Language == "es")
                        StartCoroutine(CheckDoor(_spanishLockedTexts));
                }
                    
            }
        }
        else if (_doorType == "backDoor")
        {
            if (PlayerController.Instance.GetLookingBack())
            {
                if (PlayerController.Instance.GetMustEscape())
                {
                    if (_newSceneName != "Stairs2ndFloor" && _newSceneName != "EntranceAndStairs")
                    {
                        if (LanguageManager.Instance.Language == "en")
                            StartCoroutine(CheckDoor(_mustEscapeTexts));
                        else if (LanguageManager.Instance.Language == "es")
                            StartCoroutine(CheckDoor(_spanishMustEscapeTexts));

                        return;
                    }
                }

                if (PlayerController.Instance.GetMustHide())
                {
                    if (LanguageManager.Instance.Language == "en")
                        StartCoroutine(CheckDoor(_hideTexts));
                    else if (LanguageManager.Instance.Language == "es")
                        StartCoroutine(CheckDoor(_spanishHideTexts));

                    return;
                }

                if (!_isLocked)
                {
                    _isOpeningDoor = true;
                    StartCoroutine(ChangeRoom());
                }
                else
                {
                    if(LanguageManager.Instance.Language == "en")
                        StartCoroutine(CheckDoor(_lockedTexts));
                    else if (LanguageManager.Instance.Language == "es")
                        StartCoroutine(CheckDoor(_spanishLockedTexts));
                }

            }
        }
    }
    public string NewSceneName { get => _newSceneName; set => _newSceneName = value; }
    public bool IsLocked { get => _isLocked; set => _isLocked = value; }

    void Cinematics()
    {
        if(_firstDinningRoomScene)
        {
            ScenesInGame.Instance.SetFirstDinningRoomScene(true);
        }
        else if(_firstKitchenScene)
        {
            ScenesInGame.Instance.SetFirstKitchenScene(true);
        }
        else if(_firstParentsRoomScene) 
        { 
            ScenesInGame.Instance.SetFirstParentsRoomScene(true);
        }
    }

    void UnlockDoor()
    {
        if (PlayerInventory.Instance.GetItemListLenght() <= 0) return;

        if(_isLocked && PlayerInventory.Instance.CurrentItemName() == _doorKeyName && PlayerInventory.Instance.IsUsingItemMouse)
        {
            _audioSource.PlayOneShot(_doorUnlocked, 0.7f);
            _isLocked = false;

            if(LanguageManager.Instance.Language == "en")
                TextBox.Instance.ShowText("I unlocked it.");
            else if (LanguageManager.Instance.Language == "es")
                TextBox.Instance.ShowText("La desbloqueé.");

            int itemToDestroy = PlayerInventory.Instance.ItemSelected;

            PlayerInventory.Instance.ItemSelected = 0;
            PlayerInventory.Instance.IsUsingItemMouse = false;
            PlayerInventory.Instance.IsUsingParentsKey = false;
            PlayerInventory.Instance.RemoveItem(itemToDestroy);
        }
    }
       
    IEnumerator ChangeRoom()
    {
        PlayerController.Instance.SetPlayerHasSideMovement(false);
        _fadeAnimator.SetTrigger("StartTransition");
        if (!_isStairs) { _audioSource.PlayOneShot(_doorOpenSound, 0.9f); }
        else { _audioSource.PlayOneShot(_stairsSound, 0.55f); }
        yield return new WaitForSecondsRealtime(1.6f);
        if (!_isStairs) { _audioSource.PlayOneShot(_doorCloseSound, 0.9f); }

        PlayerController.Instance.transform.position = _playerPosition;
        PlayerController.Instance.transform.localScale = new Vector3(_playerScale, PlayerController.Instance.transform.localScale.y);
        PlayerController.Instance.SetLookingFront(_playerIsFront);
        PlayerController.Instance.SetLookingBack(_playerIsBack);

        yield return new WaitForSecondsRealtime(0.3f);                
        SceneManager.LoadScene(_newSceneName);
    }

    void LockedDoors()
    {
        if (_newSceneName == "ParentsRoom" && ScenesInGame.Instance.GetFirstKitchenScenePlayed())
        {
            if (!IsLocked) return;

            if (!textAdded)
            {
                if (LanguageManager.Instance.Language == "en")
                {
                    _lockedTexts.Add("I can hear something inside.");
                    _lockedTexts.Add("The key must be somewhere around the house.");
                    _lockedTexts.Add("Maybe I can find something on my dads desk or computer in the living room.");
                }
                if (LanguageManager.Instance.Language == "es")
                {
                    _spanishLockedTexts.Add("Puedo escuchar algo dentro.");
                    _spanishLockedTexts.Add("La llave debe estar en algún lugar de la casa.");
                    _spanishLockedTexts.Add("Tal vez encuentre algo en el escritorio de mi papá o en el ordenador del living.");

                }

                textAdded = true;
            }

            searchingSound.Play();
        }

    }

    void FinishLockedDoorSounds()
    {
        if (_newSceneName == "ParentsRoom" && ScenesInGame.Instance.GetFirstKitchenScenePlayed())
        {            
            searchingSound.Stop();
        }
    }

    IEnumerator CheckDoor(List<string> texts)
    {     
        PlayerController.Instance.SetOnLockedDoor(true);

        LockedDoors();

        if(LanguageManager.Instance.Language == "en")
        {
            for (int i = 0; i < texts.Count; i++)
            {                
                _playerText.text = texts[i];                

                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if(LanguageManager.Instance.Language == "es")
        {
            for (int i = 0; i < texts.Count; i++)
            { 
                _playerText.text = texts[i];

                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitForSeconds(0.1f);
            }
        }

        FinishLockedDoorSounds();

        _playerText.text = "";                             
        PlayerController.Instance.SetOnLockedDoor(false);

    }
    
}
