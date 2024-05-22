using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AspectRatioFix : MonoBehaviour
{
    GameObject _lookAtPos;
    [SerializeField] TextMeshPro _width;

    private void Awake()
    {
        _lookAtPos = GameObject.Find("LookAtPos");
    }

    private void Start()
    {
        if (_lookAtPos == null) return;

        if (Screen.width <= 1280)
            _lookAtPos.transform.position = _lookAtPos.transform.parent.TransformPoint(2.5f, _lookAtPos.transform.position.y, _lookAtPos.transform.position.z);

        else if(Screen.width == 1600 &&  Screen.height == 1200)
            _lookAtPos.transform.position = _lookAtPos.transform.parent.TransformPoint(2.5f, _lookAtPos.transform.position.y, _lookAtPos.transform.position.z);

        else if(Screen.width == 1680 && Screen.height == 1050)
            _lookAtPos.transform.position = _lookAtPos.transform.parent.TransformPoint(2.5f, _lookAtPos.transform.position.y, _lookAtPos.transform.position.z);

        else if (Screen.width == 1920 && Screen.height == 1200)
            _lookAtPos.transform.position = _lookAtPos.transform.parent.TransformPoint(2.5f, _lookAtPos.transform.position.y, _lookAtPos.transform.position.z);

        else if (Screen.width == 1440 && Screen.height == 900)
            _lookAtPos.transform.position = _lookAtPos.transform.parent.TransformPoint(2.5f, _lookAtPos.transform.position.y, _lookAtPos.transform.position.z);

        else
            _lookAtPos.transform.position = _lookAtPos.transform.parent.TransformPoint(6.72f, _lookAtPos.transform.position.y, _lookAtPos.transform.position.z);

    }

    
}
