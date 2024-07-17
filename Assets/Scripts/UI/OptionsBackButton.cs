using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBackButton : MonoBehaviour
{
    public static OptionsBackButton Instance;

    [SerializeField] Image _optionsBackground;
    [SerializeField] Image _volumeImage;
    [SerializeField] GameObject _plus;
    [SerializeField] GameObject _minus;
    [SerializeField] GameObject _optionsEnglish;
    [SerializeField] GameObject _optionsSpanish;

    private void Awake()
    {
        Instance = this;
    }

    public void OptionsBack()
    {
        _optionsBackground.enabled = false;
        _volumeImage.enabled = false;
        _plus.SetActive(false);
        _minus.SetActive(false);
        _optionsEnglish.SetActive(false);
        _optionsSpanish.SetActive(false);
    }
}
