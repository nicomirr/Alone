using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseBehaviour : MonoBehaviour, IDataPersistence
{
    public static MouseBehaviour Instance;

    [Header("Mouse appearences")]
    [SerializeField] Texture2D _blackPointer;
    [SerializeField] Texture2D _redPointer;
    [SerializeField] Texture2D _glassEmptyMouse;
    [SerializeField] Texture2D _glassFullMouse;
    [SerializeField] Texture2D _parentsRoomKey;
    [SerializeField] Texture2D _bathroomDrainStopper;
    [SerializeField] Texture2D _underStairsRoomKey;
    [SerializeField] Texture2D _umbrella;
    [SerializeField] Texture2D _umbrellaHandle;

    [Header("Click Distance")]
    [SerializeField] float _playerMinClickableDistance = 3;
    bool _notClickable;

    bool _hasJustUsedObject;

    GameObject obj;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveData(ref GameData data)
    {
        data.notClickable = _notClickable;
        data.hasJustusedObject = _hasJustUsedObject;
    }
    public void LoadData(GameData data)
    {
        _notClickable = data.notClickable;
        _hasJustUsedObject = data.hasJustusedObject;
    }

    public bool NotClickable { get => _notClickable; set => _notClickable = value; }
    public GameObject Obj { get => obj; set => obj = value; }
    public float PlayerMinClickableDistance { get => _playerMinClickableDistance; set => _playerMinClickableDistance = value; }

    private void Update()
    {
        if (Pause.Instance.IsPaused || ScenesInGame.Instance.GetSceneIsPlaying() || PlayerController.Instance.GetIsHidding() || PlayerController.Instance.GetIsReading() || PlayerController.Instance.GetGameOver()) 
        {
            UnityEngine.Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);
            return;
        }

        if (PlayerInventory.Instance.IsUsingGlass)
        {
            if(!PlayerInventory.Instance.GlassFilled)
                UnityEngine.Cursor.SetCursor(_glassEmptyMouse, new Vector2(_glassEmptyMouse.width / 2, _glassEmptyMouse.height / 2), CursorMode.Auto);
            else
                UnityEngine.Cursor.SetCursor(_glassFullMouse, new Vector2(_glassFullMouse.width / 2, _glassFullMouse.height / 2), CursorMode.Auto);

            _hasJustUsedObject = true;
            return;
        }
        else if(PlayerInventory.Instance.IsUsingParentsKey)
        {
            UnityEngine.Cursor.SetCursor(_parentsRoomKey, new Vector2(_parentsRoomKey.width / 2, _parentsRoomKey.height / 2), CursorMode.Auto);
            _hasJustUsedObject = true;
            return;
        }
        else if(PlayerInventory.Instance.IsUsingBathroomDrainStopper)
        {
            UnityEngine.Cursor.SetCursor(_bathroomDrainStopper, new Vector2(_bathroomDrainStopper.width / 2, _bathroomDrainStopper.height / 2), CursorMode.Auto);
            _hasJustUsedObject = true;
            return;
        }
        else if (PlayerInventory.Instance.IsUsingKeyUnderStairs)
        {
            UnityEngine.Cursor.SetCursor(_underStairsRoomKey, new Vector2(_underStairsRoomKey.width / 2, _underStairsRoomKey.height / 2), CursorMode.Auto);
            _hasJustUsedObject = true;
            return;
        }
        else if (PlayerInventory.Instance.IsUsingUmbrella)
        {
            UnityEngine.Cursor.SetCursor(_umbrella, new Vector2(_umbrella.width / 2, _umbrella.height / 2), CursorMode.Auto);
            _hasJustUsedObject = true;
            return;
        }
        else if (PlayerInventory.Instance.IsUsingUmbrellaHandle)
        {
            UnityEngine.Cursor.SetCursor(_umbrellaHandle, new Vector2(_umbrellaHandle.width / 2, _umbrellaHandle.height / 2), CursorMode.Auto);
            _hasJustUsedObject = true;
            return;
        }

        if (!PlayerInventory.Instance.IsUsingItemMouse && _hasJustUsedObject || ButtonsGrid.Instance.GetCurrentAction() != "Use" && _hasJustUsedObject)
        {
            UnityEngine.Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);
            _hasJustUsedObject = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            obj = hit.collider.gameObject;
            MouseDistanceMath(obj);

            if (obj.name == "ComputerZoom")
            {
                UnityEngine.Cursor.SetCursor(null, new Vector2(0,0), CursorMode.Auto);
                return;
            }

            if (obj.GetComponent<ClickableObject>() != null && !_notClickable)
                UnityEngine.Cursor.SetCursor(_redPointer, new Vector2(_redPointer.width / 2, _redPointer.height / 2), CursorMode.Auto);           
        }
        else
            UnityEngine.Cursor.SetCursor(_blackPointer, new Vector2(_blackPointer.width / 2, _blackPointer.height / 2), CursorMode.Auto);
    }

    void MouseDistanceMath(GameObject objt)
    {
        float objAndPlayerDistance = PlayerController.Instance.transform.position.x - objt.transform.position.x;
        _notClickable = objAndPlayerDistance > _playerMinClickableDistance || objAndPlayerDistance < -_playerMinClickableDistance;
    }
      
}
