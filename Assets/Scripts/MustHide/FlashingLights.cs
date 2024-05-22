using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class FlashingLights : MonoBehaviour, IDataPersistence
{
    bool _corroutineStarted;
    [SerializeField] GameObject _sceneLight;
    [SerializeField] GameObject _globalLight;
    [SerializeField] Color32 _lightsOut;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _respiration;
    [SerializeField] GameObject _playerHiddingLight;
    [SerializeField] AudioSource _searchingSound;
    [SerializeField] float _deathTime;
    [SerializeField] float _deathTimer;
    [SerializeField] GameObject _gameOver;


    public void SaveData(ref GameData data)
    {
        data.flashingLightsCoroutineStarted = _corroutineStarted;
    }

    public void LoadData(GameData data)
    {
        _corroutineStarted = data.flashingLightsCoroutineStarted;
    }


    void Update()
    {
        MustHideState();
    }

    void MustHideState()
    {
        if (!ScenesInGame.Instance.GetSecondParentsRoomScene()) return;


        if (PlayerController.Instance.GetMustHide() && !ScenesInGame.Instance.GetSceneIsPlaying())
        {
            if (!Pause.Instance.IsPaused)
                _deathTimer += Time.deltaTime;

            if (_deathTimer >= _deathTime)
            {
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
            time = Random.Range(0.0f, 0.65f);

            _sceneLight.SetActive(false);
            yield return new WaitForSeconds(time);

            time = Random.Range(0.0f, 0.65f);

            _sceneLight.SetActive(true);
            yield return new WaitForSeconds(time);
        }

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
        _corroutineStarted = false;

        _playerHiddingLight.SetActive(true);
        _enemy.SetActive(true);
        StartCoroutine(_enemy.GetComponent<Enemy>().EnemyMovement());
    
    }

}
