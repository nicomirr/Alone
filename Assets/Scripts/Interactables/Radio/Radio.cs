using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Radio : MonoBehaviour, IPointerClickHandler
{
    bool _notClickable;
    RoomLightStatus _roomLightStatus;

    [SerializeField] GameObject _radio;

    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
        {
            if (Pause.Instance.IsPaused) return;
            EnableRadio();
        }
    }

    void EnableRadio()
    {
        if (!_roomLightStatus.GetRoomHasLight() && !PlayerInventory.Instance.IsUsingFlashlight) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        PlayerController.Instance.SetIsInteractingWithEnviroment(true);

        _radio.SetActive(true);

    }

    public void BackButton()
    {
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        _radio.SetActive(false);
    }
}
