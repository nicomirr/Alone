using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanBeTurnedOnOff : MonoBehaviour, IPointerClickHandler, IDataPersistence 
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    bool _firstCanBeTurnedOnOrOffCheck;
    [SerializeField] bool _firstCanBeTurnedOnOrOffStatus;
    bool _canBeTurnedOnOrOff = true;
    bool _isTurnedOn;

    RoomLightStatus _roomLightStatus;

    bool _notClickable;
    
    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    public void SaveData(ref GameData data)
    {
        if(data.isTurnedOn.ContainsKey(_id)) 
        { 
            data.isTurnedOn.Remove(_id);
        }
        data.isTurnedOn.Add(_id, IsTurnedOn);

        if(data.canBeTurnedOnOrOff.ContainsKey(_id)) 
        { 
            data.canBeTurnedOnOrOff.Remove(_id);
        }
        data.canBeTurnedOnOrOff.Add(_id, _canBeTurnedOnOrOff);
       
    }
    public void LoadData(GameData data)
    {
        data.isTurnedOn.TryGetValue(_id, out _isTurnedOn);
        data.canBeTurnedOnOrOff.TryGetValue(_id, out _canBeTurnedOnOrOff);
    }

    public bool IsTurnedOn { get => _isTurnedOn; set => _isTurnedOn = value; }
    public bool CanBeTurnedOnOrOff { get => _canBeTurnedOnOrOff; set => _canBeTurnedOnOrOff = value; }

    private void Update()
    {
        FirstTimeStatus();
    }

    void FirstTimeStatus()
    {
        if (!_firstCanBeTurnedOnOrOffCheck)
        {
            _canBeTurnedOnOrOff = _firstCanBeTurnedOnOrOffStatus;
            _firstCanBeTurnedOnOrOffCheck = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;

        bool RoomHasLight = _roomLightStatus.GetRoomHasLight();

        if (!RoomHasLight && !PlayerInventory.Instance.IsUsingFlashlight) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;

        if (_notClickable) return;

        if(_canBeTurnedOnOrOff)
        {
            if ((ButtonsGrid.Instance.GetCurrentAction() == "Turn On" || ButtonsGrid.Instance.GetCurrentAction() == "Encender") && _isTurnedOn) 
            { 
                if(LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already on.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>()); 
                else if(LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está encendido.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            }
            else if ((ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar") && !_isTurnedOn) 
            { 
                if(LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already off.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>()); 
                else if(LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está apagado.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

            }
        }
        else
        { 
            if (ButtonsGrid.Instance.GetCurrentAction() == "Turn On" || ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Encender" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar")
            {
                if(LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("I can't.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("No puedo.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

            }
        }
                                
        if (ButtonsGrid.Instance.GetCurrentAction() == "Turn On" || ButtonsGrid.Instance.GetCurrentAction() == "Encender")
        {
            if(_canBeTurnedOnOrOff) 
                _isTurnedOn = true;            
        }            
        else if (ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar")
        {
            if (_canBeTurnedOnOrOff)
                _isTurnedOn = false;            
        }
    }    
}
