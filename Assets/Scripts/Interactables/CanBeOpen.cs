using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class CanBeOpen : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] Sprite _openSprite;
    [SerializeField] Sprite _closeSprite;
    [SerializeField] AudioClip _openSound;
    [SerializeField] AudioClip _closeSound;
    [SerializeField] bool _isOpen;
    bool _notClickable;    
    [SerializeField] GameObject _appearence;
    RoomLightStatus _roomLightStatus;


    private void Awake()
    {        
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();        
    }
    public void SaveData(ref GameData data)
    {
        if(data.isOpen.ContainsKey(_id)) 
        { 
            data.isOpen.Remove(_id);
        }
        data.isOpen.Add(_id, _isOpen);
        
    }

    public void LoadData(GameData data)
    {
        data.isOpen.TryGetValue(_id, out _isOpen);

        if(_isOpen) 
            _appearence.GetComponent<SpriteRenderer>().sprite = _openSprite;
        else
            _appearence.GetComponent<SpriteRenderer>().sprite = _closeSprite;
    }

    public bool IsOpen { get => _isOpen; set => _isOpen = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Pause.Instance.IsPaused) return;

        bool roomHasLight = _roomLightStatus.GetRoomHasLight();

        if (!roomHasLight && !PlayerInventory.Instance.IsUsingFlashlight) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;

        if (_notClickable) return;

        if (ButtonsGrid.Instance.GetCurrentAction() != "Open" && ButtonsGrid.Instance.GetCurrentAction() != "Close" && ButtonsGrid.Instance.GetCurrentAction() != "Abrir" && ButtonsGrid.Instance.GetCurrentAction() != "Cerrar") return;

        if (ButtonsGrid.Instance.GetCurrentAction() == "Open" || ButtonsGrid.Instance.GetCurrentAction() == "Abrir")
        { 
            if(!_isOpen)
            {
                _appearence.GetComponent<SpriteRenderer>().sprite = _openSprite;
                AudioSource.PlayClipAtPoint(_openSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
                _isOpen = true;
            }
            else
            {
                if(LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already open.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está abierto.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            }
                     
        }       
        else if(ButtonsGrid.Instance.GetCurrentAction() == "Close" || ButtonsGrid.Instance.GetCurrentAction() == "Cerrar")
        {
            if(_isOpen)
            {
                _appearence.GetComponent<SpriteRenderer>().sprite = _closeSprite;
                AudioSource.PlayClipAtPoint(_closeSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
                _isOpen = false;
            }
            else
            {
                if(LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already closed.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está cerrado.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            }

        }
    }    
}
