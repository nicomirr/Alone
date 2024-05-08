using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    GameObject _lookAtPos;
    CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _lookAtPos = GameObject.Find("LookAtPos");
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _camera.Follow = _lookAtPos.transform;
    }
}
