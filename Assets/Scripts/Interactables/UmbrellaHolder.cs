using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UmbrellaHolder : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    bool _notClickable;
    RoomLightStatus _roomLightStatus;
    [SerializeField] GameObject _umbrellaHolder;
    [SerializeField] Sprite _noBlueUmbrella;      
    [SerializeField] Sprite _hasBlueUmbrella;
    bool _hasUmbrella;

    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    public void LoadData(GameData data)
    {
        _hasUmbrella = data.hasBlueUmbrella;
    }

    public void SaveData(ref GameData data)
    {
        data.hasBlueUmbrella = _hasBlueUmbrella;
    }

    void Update()
    {
        if (ScenesInGame.Instance.GetIsFlashback())
        {
            _umbrellaHolder.GetComponent<SpriteRenderer>().sprite = _hasBlueUmbrella;
            return;
        }

        if (_hasUmbrella)
        {
            _umbrellaHolder.GetComponent<SpriteRenderer>().sprite = _noBlueUmbrella;
        }
                
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;
        if (!_roomLightStatus.GetRoomHasLight() && !PlayerInventory.Instance.IsUsingFlashlight) return;
        if (ButtonsGrid.Instance.GetCurrentAction() != "Search" && ButtonsGrid.Instance.GetCurrentAction() != "Buscar") return;


        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;


        _hasUmbrella = true;
    }

    
}
