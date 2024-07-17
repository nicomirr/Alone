using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class FlashingLights : MonoBehaviour
{
    bool _corroutineStarted;
    [SerializeField] GameObject _sceneLight;
    [SerializeField] GameObject _globalLight;
    [SerializeField] Color32 _lightsOut;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _respiration;
    [SerializeField] GameObject _playerHiddingLight;
    [SerializeField] AudioSource _searchingSound;
    float _deathTime = 30;
    [SerializeField] float _deathTimer;
    [SerializeField] GameObject _gameOver;

        
    void Update()
    {
        if (ScenesInGame.Instance.GetIsEnding()) return;
        if (ScenesInGame.Instance.GetIsFlashback()) return;

        MustHideState();
    }

    void MustHideState()
    {
        _searchingSound.volume = 1f * GameVolume.Instance.CurrentVolume();

        if (!ScenesInGame.Instance.GetSecondParentsRoomScenePlayed()) return;

        if (PlayerController.Instance.GetMustHide() && !ScenesInGame.Instance.GetSceneIsPlaying())
        {
            if (!Pause.Instance.IsPaused)
                _deathTimer += Time.deltaTime;

            if (_deathTimer >= _deathTime)
            {
                PlayerController.Instance.SetGameOver(true);
                _gameOver.SetActive(true);
            }
        }

        if (!_corroutineStarted && PlayerController.Instance.GetMustHide())
        {
            _corroutineStarted = true;
            StartCoroutine(LightFlashing());
        }
    }

    IEnumerator LightFlashing()
    {
        float time;

        while (PlayerController.Instance.GetMustHide())
        {
            if (!AudioSourceHorror.Instance.GetComponent<AudioSource>().isPlaying)
                AudioSourceHorror.Instance.GetComponent<AudioSource>().Play();

            if (ScenesInGame.Instance.GetSceneIsPlaying() && PlayerController.Instance.GetMustHide() && !ScenesInGame.Instance.GetFirstLivingRoomSceneIsPlaying())
            {
                if(_sceneLight != null)
                    _sceneLight.SetActive(true);

                StopCoroutine(LightFlashing());
                MustHideCounter.timer = 0;
                PlayerController.Instance.SetMustHide(false);
            }           
            
            time = Random.Range(0.0f, 0.65f);

            if (_sceneLight != null)
                _sceneLight.SetActive(false);

            yield return new WaitForSeconds(time);

            time = Random.Range(0.0f, 0.65f);

            if (_sceneLight != null)
                _sceneLight.SetActive(true);

            yield return new WaitForSeconds(time);
                      
        }

        PlayerInventory.Instance.IsUsingFlashlight = false;

        time = Random.Range(0.0f, 0.65f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(time);

        time = Random.Range(0.0f, 0.65f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(time);

        time = Random.Range(0.0f, 0.65f);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(time);

        time = Random.Range(0.0f, 0.65f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(time);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(time);

        time = Random.Range(0.0f, 0.65f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(time);

        _sceneLight.SetActive(false);
        yield return new WaitForSeconds(time);

        time = Random.Range(0.0f, 0.65f);

        _sceneLight.SetActive(true);
        yield return new WaitForSeconds(time);

        _sceneLight.SetActive(false);
        _globalLight.GetComponent<Light2D>().color = _lightsOut;
        _searchingSound.Stop();

        PlayerController.Instance.SetMustHide(false);
        _deathTimer = 0;
        _corroutineStarted = false;

        _playerHiddingLight.SetActive(true);
        _enemy.SetActive(true);
        StartCoroutine(_enemy.GetComponent<Enemy>().EnemyMovement());
    
    }

}
