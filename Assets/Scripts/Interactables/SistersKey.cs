using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SistersKey : MonoBehaviour
{
    [SerializeField] GameObject _sisterKey;
    [SerializeField] Light2D _light;
    [SerializeField] GameObject _keyImage;
    bool _keyShown;

    private void Start()
    {
        if (/*!ScenesInGame.Instance.GetFirstEntranceFlashbackScenePlayed() ||*/ ScenesInGame.Instance.GetIsFlashback() || PlayerController.Instance.GetMustHide())      
            _sisterKey.SetActive(false);
        else
            _sisterKey.SetActive(true);

    }

    private void Update()
    {
        if (/*!ScenesInGame.Instance.GetFirstEntranceFlashbackScenePlayed() ||*/ ScenesInGame.Instance.GetIsFlashback() || PlayerController.Instance.GetMustHide()) return;        

        if (!_light.enabled && !_keyShown)
        {
            _keyShown = true;
            StartCoroutine(ShowKey());
        }
        else if (_light.enabled)
        {
            _keyShown = false;
        }
    }

    IEnumerator ShowKey()
    {
        _keyImage.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _keyImage.SetActive(false);
    }
}
