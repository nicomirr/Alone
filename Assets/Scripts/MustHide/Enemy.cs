using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    [SerializeField] Vector2 initialPos;
    [SerializeField] TextMeshPro _playerText;

    [SerializeField] int _direction;
    AudioSource _audioSource;
    [SerializeField] AudioClip _footstep;

    [SerializeField] GameObject _sceneLight;
    [SerializeField] GameObject _globalLight;
    [SerializeField] Color32 _lightsOn;
    [SerializeField] GameObject _playerHiddingLight;
    [SerializeField] AudioClip _doorClosed;
    [SerializeField] GameObject _respiration;

    [SerializeField] GameObject _playerHidding;
    [SerializeField] GameObject _blackScreen;

    [SerializeField] AudioClip _doorSlam;
    [SerializeField] AudioClip _doorSlamOpen;

    SpriteRenderer _spriteRenderer;

    [SerializeField] Sprite _idle;
    [SerializeField] Sprite _walk1;
    [SerializeField] Sprite _walk2;

    [SerializeField] GameObject _gameOver;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator EnemyMovement()
    {
        Color colorEnemy = this.gameObject.GetComponent<SpriteRenderer>().color;
        colorEnemy.a = 1f;
        this.gameObject.GetComponent<SpriteRenderer>().color = colorEnemy;

        CinemachineTransposer transposer = FindObjectOfType<CinemachineTransposer>();
        transposer.m_XDamping = 0;
        //PlayerController.Instance.transform.position = new Vector2(7.065f, PlayerController.Instance.transform.position.y);

        _respiration.SetActive(true);
        yield return new WaitUntil(() => _respiration.GetComponent<Respiration>().TutorialShown == true);

        transposer.m_XDamping = 5;

        yield return new WaitForSeconds(3f);

        AudioSource.PlayClipAtPoint(_doorSlam, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());                
        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_doorSlam, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());        
        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_doorSlamOpen, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
        yield return new WaitForSeconds(0.5f);
                            
        ScenesInGame.Instance.SetSceneIsPlaying(true);      

        ScenesInGame.Instance.SetSceneIsPlaying(false);

        _respiration.GetComponent<Respiration>().Start = true;

        float time;

        int pos = 0;

        while (PlayerController.Instance.GetIsHidding())
        {            
            time = Random.Range(0.6f, 2f);

            if(pos == 0)
                _spriteRenderer.sprite = _walk1;
            else if(pos == 1)
                _spriteRenderer.sprite = _walk2;

            transform.position = new Vector2(this.transform.position.x + _direction, this.transform.position.y);
            _audioSource.PlayOneShot(_footstep, 1f * GameVolume.Instance.CurrentVolume());

            yield return new WaitForSeconds(0.23f);
            _spriteRenderer.sprite = _idle;

            if (pos == 0)
                pos = 1;
            else if (pos == 1)
                pos = 0;

            yield return new WaitForSeconds(time);
        }

        _respiration.SetActive(false);
        _gameOver.SetActive(false);
        UnityEngine.Cursor.visible = false;

        yield return new WaitForSeconds(2);

        AudioSource.PlayClipAtPoint(_doorClosed, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
        yield return new WaitForSeconds(2);

        colorEnemy = this.gameObject.GetComponent<SpriteRenderer>().color;
        colorEnemy.a = 0f;
        this.gameObject.GetComponent<SpriteRenderer>().color = colorEnemy;

        _globalLight.GetComponent<Light2D>().color = _lightsOn;
        _playerHiddingLight.SetActive(false);
        yield return new WaitForSeconds(2);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(2);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I think it's safe to leave now.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Creo que es seguro salir.";

        yield return new WaitForSeconds(2.4f);

        _playerText.text = "";

        yield return new WaitForSeconds(1.3f);

        _blackScreen.SetActive(true);
        yield return new WaitForSeconds(2f);

        if (GameObject.Find("AudioSourceHorror"))
        {
            GameObject.Find("AudioSourceHorror").GetComponent<AudioSource>().Stop();
            GameObject.Find("AudioSourceHorror").GetComponent<AudioSource>().GetComponent<AudioSourceHorror>().IsPlaying = false;
        }

        Color color = PlayerController.Instance.GetComponent<SpriteRenderer>().color;
        color.a = 1;
        PlayerController.Instance.GetComponent<SpriteRenderer>().color = color;
        _playerHidding.SetActive(false);
        ScenesInGame.Instance.SetSceneIsPlaying(false);

        UnityEngine.Cursor.visible = true;

        yield return new WaitForSeconds(2f);

        this.transform.position = initialPos;
        this.gameObject.SetActive(false);

    }
       
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "EnemyFinish")
        {
            ScenesInGame.Instance.SetSceneIsPlaying(true);
            PlayerController.Instance.SetIsHidding(false);
        }

        if (collision.name == "EnemyRotate")
        {
            _direction *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    
}
