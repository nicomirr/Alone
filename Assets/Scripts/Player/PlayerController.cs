using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    public static PlayerController Instance;

    [Header("Flashlight animations")]
    [SerializeField] GameObject _sideFlashlight;
    [SerializeField] GameObject _sideLight;
    [SerializeField] GameObject _frontFlashlight;
    [SerializeField] GameObject _frontBackLight;

    [Header("Flashlight audio")]
    [SerializeField] AudioClip _flashlightOn;
    [SerializeField] AudioClip _flashlightOff;

    Rigidbody2D _playerRb;
    Animator _playerAnimator;
    BoxCollider2D _playerBodyCollider;

    Vector2 _rawInput;

    [Header("Movement")]
    float _walkSpeed = 3;
    float _walkSpeedMod = 2.7f;
    float _normalSpeed = 2.7f;
    float _fastSpeed = 4f;

    [Header("Pause Position")]
    [SerializeField] float _pauseYPos;

    Door _door;
    string _sceneToLoad;
    
    bool _sideMove = true;
    bool _lookingFront = false;
    bool _lookingBack = false;
    bool _playerHasSideMovement;

    bool _isTalking = false;
    bool _isInteractingWithEnviroment = false;
    bool _isReading = false;

    bool _onScene;

    bool _onLockedDoor;

    bool _mustHide;
    bool _isHidding;

    bool _mustEscape;

    bool _gameOver;


    public void Awake()
    {         
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);

        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();
        _playerBodyCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        WearingPajamas();
    }

    public void LoadData(GameData data)
    {
        Instance.transform.position = data.playerPos;
        Instance.transform.localScale = data.playerScale;
        Instance._lookingFront = data.lookingFront;
        Instance._lookingBack = data.lookingBack;
        Instance._sideMove = data.sideMove;
        Instance._mustHide = data.mustHide;
        Instance._mustEscape = data.mustEscape;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPos = Instance.transform.position;
        data.playerScale = Instance.transform.localScale;
        data.lookingFront = Instance._lookingFront;
        data.lookingBack = Instance._lookingBack;
        data.sideMove = Instance._sideMove;     
        data.mustHide = Instance._mustHide;
        data.mustEscape = Instance._mustEscape;
    }
   

    private void Update()
    {
        WearingPajamas();
        StopPlayer();
        if (Pause.Instance.IsPaused) return;
        if (_onLockedDoor) return;
        if (_isHidding) return;
        if(_gameOver) return;
        if (_door != null)
        {
           if (_door.GetIsOpeningDoor()) { return; }            
        }           

        Flashlight();

        _onScene = ScenesInGame.Instance.GetSceneIsPlaying();
        if (_onScene)
        {
            PlayerInventory.Instance.IsUsingFlashlight = false;
            return;
        }

        if (_isTalking) return;
        if (_isInteractingWithEnviroment) return;
        if (_isReading) return;

        FastMovement();
        Move();
        MoveFrontBack();
        FlipSprite();
        
    }

    public bool GetGameOver() { return _gameOver; }
    public void SetGameOver(bool value) {  _gameOver = value; }
    public bool GetMustEscape() { return _mustEscape; }
    public void SetMustEscape(bool value) { _mustEscape = value; }
    public bool GetIsHidding() { return _isHidding; }
    public void SetIsHidding(bool value) { _isHidding = value; }
    public bool GetMustHide() { return _mustHide; }
    public void SetMustHide(bool value) { _mustHide = value; }
    public string GetSceneToLoad() { return _sceneToLoad; }
    public void SetSceneToLoad(string text) { _sceneToLoad = text; }    
    public bool GetSideMove() { return _sideMove; }
    public bool GetLookingFront() { return _lookingFront; }
    public void SetLookingFront(bool value) { _lookingFront = value; }
    public bool GetLookingBack() { return _lookingBack; }
    public void SetLookingBack(bool value) { _lookingBack = value; }
    public bool GetPlayerHasSideMovement() { return _playerHasSideMovement; }
    public void SetOnLockedDoor(bool value) { _onLockedDoor = value; } 
    public bool GetOnLockedDoor() { return _onLockedDoor; } 
    public void SetIsInteractingWithEnviroment(bool value) { _isInteractingWithEnviroment = value; }
    public bool GetIsReading() { return _isReading; }
    public void SetIsReading(bool value) { _isReading = value; }
    public bool GetIsInteractingWithEnviroment() { return _isInteractingWithEnviroment; }
    public bool GetIsSearching() { return _isInteractingWithEnviroment; }
    public void SetPlayerHasSideMovement(bool value) { _playerHasSideMovement = value; }               
    public void SetIsTalking(bool value) { _isTalking = value; }
    public void SetWalkingSpeedMod(float num) { _walkSpeedMod = num; }

    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }

    void FastMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _walkSpeedMod = _fastSpeed;
        else
            _walkSpeedMod = _normalSpeed;
    }

    void OnInteract(InputValue value)
    {              
        if (_door != null)
            if (_door.GetIsOpeningDoor()) { return; }

        if (_isTalking) return;
        if (_onLockedDoor) return;
              
        if (_playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Doors")))
        {            
            if (!_door.IsLocked)
            {
                _playerAnimator.SetBool("isWalking", false);

                if (!ScenesInGame.Instance.GetSceneIsPlaying()) 
                {
                    _sceneToLoad = _door.GetNewSceneName();
                    _playerRb.velocity = new Vector2(0, 0);
                }
            }

            _door.OpenDoor();
        }

    }

    void OnPause(InputValue value)
    {
        if(_isHidding) return;  
        if (_onScene) return;
        if (_isReading) return;      
        if(_gameOver) return;

        if (!Pause.Instance.IsPaused)
        {
            Pause.Instance.IsPaused = true;
            GameObject.Find("PauseScreen").transform.position = new Vector2(Pause.Instance.transform.position.x, Screen.height / 2);
        }
        else
        {
            Pause.Instance.Continue();
        }
    }

    void WearingPajamas()
    {
        if (ScenesInGame.Instance.GetIsFlashback())
            _playerAnimator.SetBool("isWearingPajamas", true);
        else
            _playerAnimator.SetBool("isWearingPajamas", false);
    }

    void Move()
    {
        if (_door != null)
            if (_door.GetIsOpeningDoor()) { return; }

        if (_sideMove)
        {
            Vector2 playerVelocity = new Vector2(_rawInput.x * _walkSpeed, _playerRb.velocity.y).normalized;
            _playerRb.velocity = playerVelocity * _walkSpeedMod;

            _playerHasSideMovement = Mathf.Abs(_rawInput.x) > Mathf.Epsilon;

            _playerAnimator.SetBool("isWalking", _playerHasSideMovement);
        }
    }       

    void MoveFrontBack()
    {
        if (_door != null)
            if (_door.GetIsOpeningDoor()) { return; }

        if (_rawInput.y < 0 && _rawInput.x == 0)
        {
            _sideMove = false;
            _lookingFront = true;
            _lookingBack = false;
                       
        }
        else if (_rawInput.y > 0 && _rawInput.x == 0)
        {            
            _sideMove = false;
            _lookingFront = false;
            _lookingBack = true;
        }
        else if (_rawInput.x != 0)
        {           
            _sideMove = true;
            _lookingFront = false;
            _lookingBack = false;
        }

        _playerAnimator.SetBool("isLookingFront", _lookingFront);
        _playerAnimator.SetBool("isLookingBack", _lookingBack);

    }

    void StopPlayer()
    {
        if(_isTalking || Pause.Instance.IsPaused || _onLockedDoor || _isInteractingWithEnviroment || _isHidding)
        {
            _playerHasSideMovement = false;
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);
            _playerAnimator.SetBool("isWalking", _playerHasSideMovement);
        }
    }

    public void FlashlightSideMove(float value) { _sideFlashlight.transform.position = new Vector2(_sideFlashlight.transform.position.x, value); }
    public void FlashlightFrontMove(float value) { _frontFlashlight.transform.position = new Vector2(_frontFlashlight.transform.position.x, value); }

    public void FlashlightSound()
    {
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();

        if (PlayerInventory.Instance.IsUsingFlashlight)
            audioSource.PlayOneShot(_flashlightOn, 0.75f);
        else
            audioSource.PlayOneShot(_flashlightOff, 0.75f);
    }

    void Flashlight()
    {
        if (PlayerInventory.Instance.IsUsingFlashlight)
        {
            _playerAnimator.SetBool("hasFlashlight", true);

            if (_sideMove)
            {
                _sideFlashlight.SetActive(true);
                _sideLight.GetComponent<Light2D>().enabled = true;
                _frontFlashlight.SetActive(false);
                _frontBackLight.GetComponent<Light2D>().enabled = false;

            }
            else if (_lookingFront)
            {
                _sideFlashlight.SetActive(false);
                _sideLight.GetComponent<Light2D>().enabled = false;
                _frontFlashlight.SetActive(true);
                _frontBackLight.GetComponent<Light2D>().enabled = true;
            }
            else if (_lookingBack)
            {
                _sideFlashlight.SetActive(false);
                _sideLight.GetComponent<Light2D>().enabled = false;
                _frontFlashlight.SetActive(false);
                _frontBackLight.GetComponent<Light2D>().enabled = true;
            }
        }
        else
        {
            _playerAnimator.SetBool("hasFlashlight", false);
            _sideFlashlight.SetActive(false);
            _sideLight.GetComponent<Light2D>().enabled = false;
            _frontFlashlight.SetActive(false);
            _frontBackLight.GetComponent<Light2D>().enabled = false;
        }

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(_playerRb.velocity.x), transform.localScale.y);

        if (_lookingFront || _lookingBack)
            transform.localScale = new Vector2(1, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
           _door = collision.gameObject.GetComponent<Door>();
        }
    }

    public IEnumerator MoveOnScene(int playerXScale, int moveDirection, int steps)
    {
        _playerAnimator.SetBool("isLookingFront", false);
        _playerAnimator.SetBool("isLookingBack", false);

        _lookingFront = false;
        _lookingBack = false;

        this.transform.localScale = new Vector2(playerXScale, 1);

        Vector2 playerVelocity = new Vector2(moveDirection * _walkSpeed, _playerRb.velocity.y).normalized;

        _playerHasSideMovement = true;    
        _playerAnimator.SetBool("isWalking", true);

        for (int i = 0; i < steps; i++)
        {
            _playerRb.velocity = playerVelocity * _walkSpeedMod;
            yield return new WaitForSeconds(0.2f);
        }

        _playerHasSideMovement = false;
        _playerAnimator.SetBool("isWalking", false);
        _playerRb.velocity = new Vector2(0, 0);

    }
}
