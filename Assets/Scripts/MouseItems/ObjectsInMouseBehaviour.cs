using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectsInMouseBehaviour : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    [Header("Properties")]
    [SerializeField] bool _waterFiller;
    [SerializeField] bool _canBeWatered;

    bool _firstCanBeWateredStatusCheck;
    [SerializeField] bool _canBeWateredInitialStatus;

    RoomLightStatus _roomLightStatus;
    [SerializeField] float _playerMinClickableDistance = 3;
    bool _notClickable;

    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
    }

    public void SaveData(ref GameData data)
    {
        if(data.canBeWatered.ContainsKey(_id)) 
        { 
            data.canBeWatered.Remove(_id);
        }
        data.canBeWatered.Add(_id, _canBeWatered);

        if (data.firstCanBeWateredStatusCheck.ContainsKey(_id))
        {
            data.firstCanBeWateredStatusCheck.Remove(_id);
        }
        data.firstCanBeWateredStatusCheck.Add(_id, _firstCanBeWateredStatusCheck);
    }

    public void LoadData(GameData data)
    {
        data.canBeWatered.TryGetValue(_id, out _canBeWatered);
        data.firstCanBeWateredStatusCheck.TryGetValue(_id, out _firstCanBeWateredStatusCheck);
    }

    private void Update()
    {
        FirstCanBeWateredStatus();
    }

    public void OnPointerClick(PointerEventData eventData)
    {        
        if (PlayerController.Instance.GetOnLockedDoor()) return;
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject obj = hit.collider.gameObject;
            MouseDistanceMath(obj);                        
        }

        if (_notClickable) return;
               
        bool roomHasLight = _roomLightStatus.GetRoomHasLight();

        if (PlayerInventory.Instance.IsUsingItemMouse)
        {          
            if(PlayerInventory.Instance.IsUsingGlass && _waterFiller && !PlayerInventory.Instance.GlassFilled)
            {
                if (!roomHasLight) 
                {
                    if(LanguageManager.Instance.Language == "en")
                    {
                        TextBox.Instance.ShowText("It's too dark to see.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                        return;
                    }
                    else if(LanguageManager.Instance.Language == "es")
                    {
                        TextBox.Instance.ShowText("Está muy oscuro para ver.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                        return;
                    }
                }

                PlayerInventory.Instance.GlassFilled = true;
                PlayerInventory.Instance.IsUsingGlass = false;
                PlayerInventory.Instance.IsUsingItemMouse = false;           
            }
            else if(PlayerInventory.Instance.IsUsingGlass && _canBeWatered && PlayerInventory.Instance.GlassFilled)
            {
                if (!roomHasLight)
                {
                    if (LanguageManager.Instance.Language == "en")
                    {
                        TextBox.Instance.ShowText("It's too dark to see.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                        return;
                    }
                    else if (LanguageManager.Instance.Language == "es")
                    {
                        TextBox.Instance.ShowText("Está muy oscuro para ver.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                        return;
                    }
                }

                GetComponent<Plant>().PlayWateringSound();
                GetComponent<Plant>().HasBeenWatered = true;

                if(LanguageManager.Instance.Language == "en")
                    GetComponent<ClickableObject>().LookAtText = "The soil is wet now.";
                else if (LanguageManager.Instance.Language == "es")
                    GetComponent<ClickableObject>().LookAtTextSpanish = "La tierra está ahora mojada.";

                PlayerInventory.Instance.GlassFilled = false;
                PlayerInventory.Instance.IsUsingGlass = false;
                PlayerInventory.Instance.IsUsingItemMouse = false;
                _canBeWatered = false;
            }
            else
            {
                if (roomHasLight)
                {
                    int rand = Random.Range(0, 5);

                    switch (rand)
                    {
                        case 0:

                            if(LanguageManager.Instance.Language == "en")
                                TextBox.Instance.ShowText("How?", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                            else if (LanguageManager.Instance.Language == "es")
                                TextBox.Instance.ShowText("¿Cómo?", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

                            break;

                        case 1:

                            if(LanguageManager.Instance.Language == "en")
                                TextBox.Instance.ShowText("I don't think so.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                            else if (LanguageManager.Instance.Language == "es")
                                TextBox.Instance.ShowText("No lo creo.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

                            break;

                        case 2:

                            if(LanguageManager.Instance.Language == "en")
                                TextBox.Instance.ShowText("That's not possible.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                            else if (LanguageManager.Instance.Language == "es")
                                TextBox.Instance.ShowText("Eso no es posible.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

                            break;

                        case 3:
                            
                            if(LanguageManager.Instance.Language == "en")
                                TextBox.Instance.ShowText("I can't use it here.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                            else if (LanguageManager.Instance.Language == "en")
                                TextBox.Instance.ShowText("No puedo usarlo aquí.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

                            break;

                        case 4:

                            if(LanguageManager.Instance.Language == "en")
                                TextBox.Instance.ShowText("What?", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                            else if(LanguageManager.Instance.Language == "es")
                                TextBox.Instance.ShowText("¿Qué?", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());

                            break;
                    }
                }
                else
                {
                    if(LanguageManager.Instance.Language == "en")
                        TextBox.Instance.ShowText("It's too dark to see.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                    else if (LanguageManager.Instance.Language == "es")
                        TextBox.Instance.ShowText("Está muy oscuro para ver.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
                }
            }
          
            
            
        }
    }

    void FirstCanBeWateredStatus()
    {
        if (!_firstCanBeWateredStatusCheck)
        {
            _canBeWatered = _canBeWateredInitialStatus;
            _firstCanBeWateredStatusCheck = true;
        }
    }

    void MouseDistanceMath(GameObject obj)
    {
        float objAndPlayerDistance = PlayerController.Instance.transform.position.x - obj.transform.position.x;
        _notClickable = objAndPlayerDistance > _playerMinClickableDistance || objAndPlayerDistance < -_playerMinClickableDistance;
    }

    
}
