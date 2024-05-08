using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomLightStatus : MonoBehaviour
{
    [SerializeField] Light2D[] _lights;
    bool _roomHasLight;

    private void Update()
    {
        CheckLights();
    }

    public bool GetRoomHasLight() { return _roomHasLight; }

    void CheckLights()
    {
        if (_lights.Length == 0) return;

        for (int i = 0; i < _lights.Length; i++)
        {
            if (_lights[i].enabled)
            {
                _roomHasLight = true;
                return;
            }
            else
                _roomHasLight = false;
        }
    }
}
