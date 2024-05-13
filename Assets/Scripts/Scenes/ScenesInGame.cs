using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class ScenesInGame : MonoBehaviour, IDataPersistence
{
    public static ScenesInGame Instance;

    bool _firstPlayerRoomScene;
    bool _firstPlayerRoomScenePlayed;
    [SerializeField] GameObject _sceneLight;

    bool _firstDinningRoomScene;
    bool _firstDinningRoomScenePlayed;

    bool _firstKitchenScene;
    bool _firstKitchenScenePlayed;

    bool _firstParentsRoomScene;
    
    bool _firstParentsRoomScenePlayed;
    bool _secondParentsRoomScenePlayed;
    bool _secondParentsRoomSceneFix;

    bool _firstEntranceScenePlayed;

    bool _firstLivingroomScenePlayed;

    bool _sceneIsPlaying;

    [SerializeField] Animator _fadeAnimator;
    [SerializeField] UnityEngine.UI.Image _blackImage;

    float textBoxOffset = 1.5f;

    TextMeshPro _playerText;
    TextMeshPro _generalText;

    [Header("Door sounds")]
    [SerializeField] AudioClip _doorOpenSound;
    [SerializeField] AudioClip _doorCloseSound;

    [Header("Kitchen/Dinning room scene sounds")]
    [SerializeField] GameObject _kettle;
    [SerializeField] AudioSource _kettleAudioSource;
    [SerializeField] GameObject _flashback;
    [SerializeField] AudioClip _jumpScareSound;
    [SerializeField] AudioClip _windowBreak;

    [Header("PlayersRoom scene sounds")]
    [SerializeField] AudioClip _laser;
    [SerializeField] AudioClip _explosion;

    [Header("ParentsRoom scene sounds")]
    [SerializeField] AudioSource _searchingSound;
    [SerializeField] AudioClip _doorSlam;
    [SerializeField] AudioClip _doorSlamOpen;
    [SerializeField] AudioClip _footstep;

    [Header("ParentsRoomSecondScene")]
    [SerializeField] GameObject _wardrobeZoomScene;
    [SerializeField] GameObject _parentsBathroomDoor;
    [SerializeField] Sprite _zoom0;
    [SerializeField] Sprite _zoom1;
    [SerializeField] Sprite _zoom2;
    [SerializeField] Sprite _zoom3;
    [SerializeField] Sprite _zoom4;
    [SerializeField] Sprite _zoom5;
    [SerializeField] Sprite _zoom6;
    [SerializeField] Sprite _zoom7;
    [SerializeField] Sprite _zoom8;
    [SerializeField] Sprite _zoom9;
    [SerializeField] Sprite _zoom10;
    [SerializeField] Sprite _zoom11;
    [SerializeField] Sprite _zoom12;
    [SerializeField] Sprite _zoom13;
    [SerializeField] GameObject _goneText;
    [SerializeField] GameObject _parentsBlackScreen;
    [SerializeField] GameObject _wardrobe;
    [SerializeField] Sprite _wardrobeOpen;
    [SerializeField] AudioSource _horrorSoundAfterWardrobe;

    [Header("EntranceAndStairsFirstScene")]
    [SerializeField] AudioClip _tryingToOpenDoor;
    [SerializeField] GameObject _frontDoor;

    [Header("LivingroomFirstScene")]
    [SerializeField] AudioClip _phonePickup;
    [SerializeField] AudioClip _phoneDial;
    [SerializeField] AudioClip _phoneHangUp;
    [SerializeField] GameObject _hideTutorialEnglish;
    [SerializeField] GameObject _hideTutorialSpanish;

    GameObject _generalTextBackground;

    [Header("Demo Ending")]
    [SerializeField] AudioClip _endingScreenSound;
    [SerializeField] GameObject _endingScreen;
    [SerializeField] GameObject _thanksForPlayingText;
        

    private void Awake()
    {
        Instance = this;

        _playerText = GameObject.Find("PlayerText").GetComponent<TextMeshPro>();
        _generalText = GameObject.Find("GeneralText").GetComponent<TextMeshPro>();

        _generalTextBackground = GameObject.Find("GeneralTextBackground");

    }

    private void Start()
    {
        _firstPlayerRoomScene = true;
    }

    public void SaveData(ref GameData data)
    {
        data.firstPlayerRoomScenePlayed = _firstPlayerRoomScenePlayed;
        data.firstDinningRoomScenePlayed = _firstDinningRoomScenePlayed;
        data.firstDinningRoomScene = _firstDinningRoomScene;
        data.firstKitchenScenePlayed = _firstKitchenScenePlayed;
        data.firstKitchenScene = _firstKitchenScene;
        data.firstParentsRoomScenePlayed = _firstParentsRoomScenePlayed;
        data.firstParentsRoomScene = _firstParentsRoomScene;
        data.secondParentsRoomScenePlayed = _secondParentsRoomScenePlayed;
        data.secondParentsRoomSceneFix = _secondParentsRoomSceneFix;
        data.firstEntranceScenePlayed = _firstEntranceScenePlayed;
        data.firstLivingroomScenePlayed = _firstLivingroomScenePlayed;

    }

    public void LoadData(GameData data)
    {
        _firstPlayerRoomScenePlayed = data.firstPlayerRoomScenePlayed;
        _firstDinningRoomScenePlayed = data.firstDinningRoomScenePlayed;
        _firstDinningRoomScene = data.firstDinningRoomScene;
        _firstKitchenScenePlayed = data.firstKitchenScenePlayed;
        _firstKitchenScene = data.firstKitchenScene;
        _firstParentsRoomScenePlayed = data.firstParentsRoomScenePlayed;
        _firstParentsRoomScene = data.firstParentsRoomScene;
        _secondParentsRoomScenePlayed = data.secondParentsRoomScenePlayed;
        _secondParentsRoomSceneFix = data.secondParentsRoomSceneFix;
        _firstEntranceScenePlayed = data.firstEntranceScenePlayed;
        _firstLivingroomScenePlayed = data.firstLivingroomScenePlayed;
    }

    public bool GetSceneIsPlaying() { return _sceneIsPlaying; }
    public void SetSceneIsPlaying(bool value) { _sceneIsPlaying = value; }
    public bool GetFirstKitchenScenePlayed() { return _firstKitchenScenePlayed; }
    public bool GetFirstDinningRoomScene() { return _firstDinningRoomScene; }
    public void SetFirstDinningRoomScene(bool value) { _firstDinningRoomScene = value; }
    public bool GetFirstKitchenScene() { return _firstKitchenScene; }
    public void SetFirstKitchenScene(bool value) { _firstKitchenScene = value; }
    public bool GetFirstParentsRoomScene() { return _firstParentsRoomScene; }
    public void SetFirstParentsRoomScene(bool value) { _firstParentsRoomScene = value; }
    public bool GetSecondParentsRoomScene() { return _secondParentsRoomScenePlayed; }
    public void SetSecondParentsRoomScene(bool value) { _secondParentsRoomScenePlayed = value; }


    private void Update()
    {
        SetTextPos();
        PlayScene();
    }

    void SetTextPos()
    {
        _playerText.transform.position = new Vector3(PlayerController.Instance.transform.position.x + textBoxOffset, _playerText.transform.position.y);
        _generalText.transform.position = new Vector3(PlayerController.Instance.transform.position.x + textBoxOffset, _generalText.transform.position.y);
        _generalTextBackground.transform.position = new Vector3(PlayerController.Instance.transform.position.x, _generalTextBackground.transform.position.y);
    }

    void PlayScene()
    {
        if (_firstPlayerRoomScene && !_firstPlayerRoomScenePlayed)
        {
            if (SceneManager.GetActiveScene().name == "PlayersRoom")
            {
                _sceneIsPlaying = true;

                StartCoroutine(FirstPlayerRoomScene());
                _firstPlayerRoomScenePlayed = true;
            }
        }

        if (_firstDinningRoomScene && !_firstDinningRoomScenePlayed)
        {
            if (SceneManager.GetActiveScene().name == "DinningRoom")
            {
                _sceneIsPlaying = true;

                StartCoroutine(FirstDinningRoomScene());
                _firstDinningRoomScenePlayed = true;
            }
        }

        if (_firstKitchenScene && !_firstKitchenScenePlayed)
        {
            if (SceneManager.GetActiveScene().name == "Kitchen")
            {
                _sceneIsPlaying = true;

                StartCoroutine(FirstKitchenScene());
                _firstKitchenScenePlayed = true;
            }
        }

        if (_firstParentsRoomScene && !_firstParentsRoomScenePlayed)
        {
            if (SceneManager.GetActiveScene().name == "ParentsRoom")
            {
                _sceneIsPlaying = true;

                StartCoroutine(FirstParentsRoomScene());
                _firstParentsRoomScenePlayed = true;
            }
        }

        if (PlayerController.Instance.GetIsHidding() && !_secondParentsRoomScenePlayed)
        {
            if (SceneManager.GetActiveScene().name == "ParentsRoom")
            {
                _sceneIsPlaying = true;

                StartCoroutine(SecondParentsRoomScene());
                _secondParentsRoomScenePlayed = true;
            }
        }

        if (_secondParentsRoomSceneFix)
        {
            if (SceneManager.GetActiveScene().name == "ParentsRoom")
            {
                _sceneIsPlaying = true;

                StartCoroutine(SecondParentsRoomSceneFix());               
            }
        }


        if (PlayerController.Instance.GetMustEscape() && !_firstEntranceScenePlayed)
        {
            if(SceneManager.GetActiveScene().name == "EntranceAndStairs")
            {
                _sceneIsPlaying = true;

                StartCoroutine(FirstEntranceScene());
                _firstEntranceScenePlayed = true;
            }
        }

        if (PlayerController.Instance.GetMustEscape() && !_firstLivingroomScenePlayed)
        {
            if (SceneManager.GetActiveScene().name == "LivingRoom")
            {
                _sceneIsPlaying = true;

                StartCoroutine(FirstLivingroomScene());
                _firstLivingroomScenePlayed = true;
            }
        }

    }

    IEnumerator FirstPlayerRoomScene()
    {
        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 0;

        GameObject.Find("TV").GetComponentInChildren<Animator>().SetBool("playing", true);

        Color generalTextBackgroundColorInvisible = new Color(0, 0, 0, 0);
        Color generalTextBackgroundColorVisible = new Color(0.4f, 0.4f, 0.4f, 1);

        GameObject.Find("FollowCamera").GetComponent<CinemachineVirtualCamera>().GetComponent<CinemachineTransposer>();
        GameObject.Find("LookAtPos").transform.position = new Vector3(0, GameObject.Find("LookAtPos").transform.position.y);
        PlayerController.Instance.GetComponent<Animator>().SetBool("isPlaying", true);

        yield return new WaitForSeconds(0.5f);

        AudioSource.PlayClipAtPoint(_laser, PlayerController.Instance.transform.position, 0.1f);

        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_laser, PlayerController.Instance.transform.position, 0.1f);

        yield return new WaitForSeconds(1.1f);

        AudioSource.PlayClipAtPoint(_laser, PlayerController.Instance.transform.position, 0.1f);

        yield return new WaitForSeconds(0.9f);

        GameObject.Find("TV").GetComponentInChildren<Animator>().speed = 0;
        AudioSource.PlayClipAtPoint(_explosion, PlayerController.Instance.transform.position, 0.1f);

        yield return new WaitForSeconds(1.4f);
                
        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "Oh no. Lost again. This game is really hard.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Perdí otra vez. Este juego es muy difícil.";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(2.5f);

        _generalTextBackground.GetComponent<SpriteRenderer>().color = generalTextBackgroundColorVisible;

        if(LanguageManager.Instance.Language == "en")
            _generalText.text = "Voice: Son, it's dinner time. Come downstairs.";
        else if(LanguageManager.Instance.Language == "es")
            _generalText.text = "Voz: Hijo, la cena está lista. Ven para abajo.";

        //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        yield return new WaitForSeconds(3.5f);

        _generalTextBackground.GetComponent<SpriteRenderer>().color = generalTextBackgroundColorInvisible;
        _generalText.text = "";

        yield return new WaitForSeconds(1.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "I shouldn't keep mom waiting. Time to get going.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Mejor no dejar a mi mamá esperando. Debería ir yendo.";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        _fadeAnimator.SetTrigger("StartTransition");
        transposer.m_XDamping = 5;

        yield return new WaitForSeconds(2);

        _fadeAnimator.SetTrigger("EndTransition");
        GameObject.Find("LookAtPos").transform.position = new Vector3(6.72f, GameObject.Find("LookAtPos").transform.position.y);
        Color color = new Color(0, 0, 0, 0);
        _blackImage.color = color;
        PlayerController.Instance.GetComponent<Animator>().SetBool("isPlaying", false);
        _sceneIsPlaying = false;

        GameObject.Find("TV").GetComponentInChildren<Animator>().SetBool("playing", false);
    }

    IEnumerator FirstDinningRoomScene()
    {        
        _kettleAudioSource.volume = 0.08f;
        _kettleAudioSource.Play();
        yield return new WaitForSeconds(2);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "Where's everyone?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Dónde están todos?";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "That's weird. They \n should be here.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Que extraño. Deberían \n estar aquí.";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "What's that sound?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué es ese sonido?";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "It's coming from the kitchen.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Viene desde la cocina.";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";

        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 33));
        yield return new WaitForSeconds(6.6f);

        StartCoroutine(FirstDinningRoomSceneChangeToKitchen());

    }

    IEnumerator FirstDinningRoomSceneChangeToKitchen()
    {
        PlayerController.Instance.SetSceneToLoad("Kitchen");
        _fadeAnimator.SetTrigger("StartTransition");
        AudioSource.PlayClipAtPoint(_doorOpenSound, PlayerController.Instance.transform.position, 0.7f);
        yield return new WaitForSecondsRealtime(1.7f);
        PlayerController.Instance.transform.position = new Vector3(15.13f, -1.27f);
        AudioSource.PlayClipAtPoint(_doorCloseSound, PlayerController.Instance.transform.position, 0.7f);
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene("Kitchen");
    }

    IEnumerator FirstKitchenScene()
    {
        yield return new WaitForSeconds(2);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "That's weird.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Que extraño.";

        yield return new WaitForSeconds(3.0f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "The sound was \n coming  from here.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "El sonido venía de aquí.";

        yield return new WaitForSeconds(3.0f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 13));
        yield return new WaitForSeconds(2.9f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("isLookingBack", true);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "That sound was coming from the kettle. I am sure.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Ese sonido venía de la pava. Estoy seguro.";

        yield return new WaitForSeconds(3.2f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        _kettleAudioSource.volume = 0.8f;
        _kettleAudioSource.volume = 0.4f;
        _kettleAudioSource.Play();
        _kettle.GetComponentInChildren<Animator>().SetBool("kettleIsOn", true);
        yield return new WaitForSeconds(1.0f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "What's happenning...?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué está sucediendo...?";

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(false);
        _playerText.text = "";
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);


        _sceneLight.SetActive(true);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "I'm feeling dizzy...";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Me estoy sintiendo mareado...";

        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        _playerText.text = "";
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        AudioSource.PlayClipAtPoint(_jumpScareSound, PlayerController.Instance.transform.position, 0.4f);
        _sceneLight.SetActive(true);
        _kettleAudioSource.Stop();
        _kettle.GetComponentInChildren<Animator>().SetBool("kettleIsOn", false);
        _flashback.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _flashback.SetActive(false);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "What was that?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué fue eso?";

        yield return new WaitForSeconds(1.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.0f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "Something's not wright.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Algo no anda bien.";

        yield return new WaitForSeconds(1.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.0f);

        AudioSource.PlayClipAtPoint(_windowBreak, PlayerController.Instance.transform.position, 0.28f);
        yield return new WaitForSeconds(0.4f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("isLookingBack", false);
        PlayerController.Instance.transform.localScale = new Vector3(1, 1);
        yield return new WaitForSeconds(1.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "What was that noise?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué fue ese ruido?";

        yield return new WaitForSeconds(1.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.0f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "It came from my \n parents bedroom.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Vino desde la habitación \n de mis padres.";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.0f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "I should check it out.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Debería ir y revisar.";

        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";

        _sceneIsPlaying = false;

    }

    IEnumerator FirstParentsRoomScene()
    {               
        yield return new WaitForSeconds(1);
         
        StartCoroutine(PlayerController.Instance.MoveOnScene(1, 1, 2));
        yield return new WaitForSeconds(0.4f);
        
        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "What happened here?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué sucedió aquí?";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "It's all a mess.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Está todo revuelto \n y desordenado.";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 4));
        yield return new WaitForSeconds(1f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "The window is broken.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "La ventana está rota.";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "That must have been the sound I heard.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Debe haber sido ese el ruido que escuché.";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(1, 1, 26));
        yield return new WaitForSeconds(5.2f);

        _searchingSound.Play();
        yield return new WaitForSeconds(0.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "What's that sound?";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué es ese ruido?";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "It's coming from my parents bathroom.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Viene desde el baño de mis padres.";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        if(LanguageManager.Instance.Language == "en")
            _playerText.text = "I must hide.";
        else if(LanguageManager.Instance.Language == "es")
            _playerText.text = "Debo esconderme.";

        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(0.5f);

        PlayerController.Instance.SetMustHide(true);
        _sceneIsPlaying = false;

    }

    IEnumerator SecondParentsRoomScene()
    {
        PlayerController.Instance.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(1.5f);

        _searchingSound.Stop();
        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_doorSlam, PlayerController.Instance.transform.position, 0.5f);

        _parentsBathroomDoor.transform.position = new Vector3(8.2f, _parentsBathroomDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        _parentsBathroomDoor.transform.position = new Vector3(8.3f, _parentsBathroomDoor.transform.position.y);
        yield return new WaitForSeconds(1.3f);

        AudioSource.PlayClipAtPoint(_doorSlam, PlayerController.Instance.transform.position, 0.5f);

        _parentsBathroomDoor.transform.position = new Vector3(8.2f, _parentsBathroomDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        _parentsBathroomDoor.transform.position = new Vector3(8.3f, _parentsBathroomDoor.transform.position.y);
        yield return new WaitForSeconds(1.3f);

        AudioSource.PlayClipAtPoint(_doorSlamOpen, PlayerController.Instance.transform.position, 0.5f);
        _wardrobeZoomScene.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(0.5f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom1;
        yield return new WaitForSeconds(0.8f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(0.3f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom0;
        yield return new WaitForSeconds(0.5f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(0.5f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom1;
        yield return new WaitForSeconds(1f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom2;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(1.7f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom3;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(1.3f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom4;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(2f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom5;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(1.5f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom6;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(2f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom7;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(4f); //2.3


        //DEMO//

        //AudioSource.PlayClipAtPoint(_endingScreenSound, PlayerController.Instance.transform.position, 0.5f);
        //_endingScreen.SetActive(true);
        //yield return new WaitForSeconds(6.5f);

        //_thanksForPlayingText.SetActive(true);

        //yield return new WaitForSeconds(9f);

        //System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        //Application.Quit();

        //DEMO

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom8;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(2.0f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom9;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(0.6f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom10;
        yield return new WaitForSeconds(1.1f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom11;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(1.4f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom12;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(1.6f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom13;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(1.4f);

        _wardrobeZoomScene.GetComponent<SpriteRenderer>().sprite = _zoom1;
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position, 0.6f);
        yield return new WaitForSeconds(3f);

        AudioSource.PlayClipAtPoint(_doorCloseSound, PlayerController.Instance.transform.position, 0.7f);
        yield return new WaitForSeconds(2.3f);

        if (LanguageManager.Instance.Language == "en")
            _goneText.GetComponent<TextMeshPro>().text = "I think it's gone.";
        else if (LanguageManager.Instance.Language == "es")
            _goneText.GetComponent<TextMeshPro>().text = "Creo que se fue.";
        _goneText.SetActive(true);
        yield return new WaitForSeconds(2f);

        _goneText.SetActive(false);
        yield return new WaitForSeconds(2f);

        _parentsBlackScreen.SetActive(true);
        PlayerController.Instance.SetSceneToLoad("ParentsRoom");
        yield return new WaitForSeconds(3f);

        _wardrobeZoomScene.SetActive(false);

        Color color = PlayerController.Instance.GetComponent<SpriteRenderer>().color;
        color.a = 1;
        PlayerController.Instance.GetComponent<SpriteRenderer>().color = color;
        PlayerController.Instance.SetIsHidding(false);
        PlayerController.Instance.SetMustHide(false);
        _wardrobe.GetComponent<SpriteRenderer>().sprite = _wardrobeOpen;
        _horrorSoundAfterWardrobe.Play();
        _horrorSoundAfterWardrobe.GetComponent<AudioSourceHorror>().IsPlaying = true;
        _secondParentsRoomSceneFix = true;
        PlayerController.Instance.transform.position = new Vector2(1.520912f, PlayerController.Instance.transform.position.y);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SecondParentsRoomSceneFix()
    {
        PlayerController.Instance.SetMustEscape(true);

        yield return new WaitForSeconds(2f);

        _secondParentsRoomSceneFix = false;
        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I must get out of the house as soon as possible.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Debo irme de la casa lo más pronto posible.";
        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";


        _sceneIsPlaying = false;
    }

    IEnumerator FirstEntranceScene()
    {
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.transform.position = new Vector2(-2.2f, PlayerController.Instance.transform.position.y);
        PlayerController.Instance.SetWalkingSpeedMod(4);

        StartCoroutine(PlayerController.Instance.MoveOnScene(1, 1, 24));
        yield return new WaitForSeconds(5f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpen", true);
        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        AudioSource.PlayClipAtPoint(_tryingToOpenDoor, PlayerController.Instance.transform.position, 0.25f);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.9f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", true);
        _frontDoor.transform.position = new Vector2(14.85f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpenSecond", false);
        _frontDoor.transform.position = new Vector2(14.92f, _frontDoor.transform.position.y);
        yield return new WaitForSeconds(0.4f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("doorOpen", false);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I can't open the door.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "No puedo abrir la puerta.";
        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(1f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 10));
        yield return new WaitForSeconds(3.1f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(1, 1, 3));
        yield return new WaitForSeconds(1.7f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 4));
        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "What should I do?";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Que hago?";
        yield return new WaitForSeconds(2.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I know.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Ya sé.";
        yield return new WaitForSeconds(2.0f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "The phone.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "El teléfono.";
        yield return new WaitForSeconds(2.0f);

        _playerText.text = "";
        yield return new WaitForSeconds(1f);

        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 18));
        yield return new WaitForSeconds(3.75f);

        StartCoroutine(FirstEntranceSceneChangeToLivingRoom());
    }

    IEnumerator FirstEntranceSceneChangeToLivingRoom()
    {
        PlayerController.Instance.SetSceneToLoad("LivingRoom");
        _fadeAnimator.SetTrigger("StartTransition");
        AudioSource.PlayClipAtPoint(_doorOpenSound, PlayerController.Instance.transform.position, 0.7f);
        yield return new WaitForSecondsRealtime(1.7f);
        PlayerController.Instance.transform.position = new Vector3(15.1f, -1.27f);
        AudioSource.PlayClipAtPoint(_doorCloseSound, PlayerController.Instance.transform.position, 0.7f);
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene("LivingRoom");
    }

    IEnumerator FirstLivingroomScene()
    {
        PlayerController.Instance.transform.position = new Vector3(15.1f, -1.27f);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.SetWalkingSpeedMod(4);
        StartCoroutine(PlayerController.Instance.MoveOnScene(-1, -1, 27));
        yield return new WaitForSeconds(6f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("isLookingBack", true);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.transform.localScale = Vector3.one;
        PlayerController.Instance.GetComponent<Animator>().SetBool("telephonePickedup", true);
        AudioSource.PlayClipAtPoint(_phonePickup, PlayerController.Instance.transform.position, 0.7f);
        yield return new WaitForSeconds(0.2f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephoneDialing", true);
        AudioSource.PlayClipAtPoint(_phoneDial, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.7f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephoneDialing", false);        
        yield return new WaitForSeconds(1f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephoneDialing", true);
        AudioSource.PlayClipAtPoint(_phoneDial, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.7f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephoneDialing", false);
        yield return new WaitForSeconds(1f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephoneDialing", true);
        AudioSource.PlayClipAtPoint(_phoneDial, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.7f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephoneDialing", false);
        yield return new WaitForSeconds(3.5f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("telephonePickedup", false);
        AudioSource.PlayClipAtPoint(_phoneHangUp, PlayerController.Instance.transform.position);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "It's not working...";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "No funciona...";
        yield return new WaitForSeconds(3.0f);

        _playerText.text = "";

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(false);       
        yield return new WaitForSeconds(0.4f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "What's happening?";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué está pasando?";

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);       
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        _playerText.text = "";
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        _searchingSound.Play();

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "What's that sound?";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué es ese sonido?";

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _playerText.text = "";
        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.7f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I think that \n something is coming.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Creo que algo se acerca.";

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _playerText.text = "";

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I must hide.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Debo esconderme.";

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(0.6f);

        _playerText.text = "";

        if (LanguageManager.Instance.Language == "en")
        {
            _hideTutorialEnglish.SetActive(true);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _hideTutorialSpanish.SetActive(true);
        }

        PlayerController.Instance.SetMustEscape(false);     
        PlayerController.Instance.SetMustHide(true);
                    
    }

    public void OnOkHideTutorialButtonPressed()
    {        
        if (LanguageManager.Instance.Language == "en")
        {
            _hideTutorialEnglish.SetActive(false);
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            _hideTutorialSpanish.SetActive(false);
        }

        _sceneIsPlaying = false;
    }
}
