using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantZoomed : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] string _id;

    [ContextMenu("Generate guid for id")]
    void GenerateGuid()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] Sprite _groundRemovedOne;
    [SerializeField] Sprite _groundRemovedTwo;
    
    [SerializeField] GameObject _key;
    [SerializeField] bool _hasKey;
    bool _firstHasKeyStatusCheck;
    [SerializeField] bool _firstHasKeyStatus;

    [SerializeField] AudioClip _removeGround;

    [SerializeField] int _imageNum;

    bool _firstImageUpdated;
    bool _secondImageUpdated;


    public void SaveData(ref GameData data)
    {
        if(data.imageNum.ContainsKey(_id))
        {
            data.imageNum.Remove(_id);
        }
        data.imageNum.Add(_id, _imageNum);

        if(data.firstPlantHasKeyStatusCheck.ContainsKey(_id)) 
        { 
            data.firstPlantHasKeyStatusCheck.Remove(_id);
        }
        data.firstPlantHasKeyStatusCheck.Add(_id, _firstHasKeyStatusCheck);

        if(data.plantHasKey.ContainsKey(_id)) 
        { 
            data.plantHasKey.Remove(_id);
        }
        data.plantHasKey.Add(_id, _hasKey);

        if (data.firstImageUpdated.ContainsKey(_id))
        {
            data.firstImageUpdated.Remove(_id);
        }
        data.firstImageUpdated.Add(_id, _firstImageUpdated);

        if (data.secondImageUpdated.ContainsKey(_id))
        {
            data.secondImageUpdated.Remove(_id);
        }
        data.secondImageUpdated.Add(_id, _secondImageUpdated);

    }

    public void LoadData(GameData data)
    {
        data.imageNum.TryGetValue(_id, out _imageNum);
        data.firstPlantHasKeyStatusCheck.TryGetValue(_id, out _firstHasKeyStatusCheck);
        data.plantHasKey.TryGetValue(_id, out _hasKey);
        data.firstImageUpdated.TryGetValue(_id, out _firstImageUpdated);
        data.secondImageUpdated.TryGetValue(_id, out _secondImageUpdated);
    }
    public GameObject Key { get => _key; set => _key = value; }

    private void Update()
    {
        FirstHasKeyStatus();
        UpdatePlantImage();
    }

    void FirstHasKeyStatus()
    {
        if (!_firstHasKeyStatusCheck)
        {
            _hasKey = _firstHasKeyStatus;
            _firstHasKeyStatusCheck = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((ButtonsGrid.Instance.GetCurrentAction() == "Search" || ButtonsGrid.Instance.GetCurrentAction() == "Buscar") && _imageNum < 3)
            _imageNum++;                    
    }

    void UpdatePlantImage()
    {
        if (_imageNum == 1)
        {
            if (_firstImageUpdated) return; 

            AudioSource.PlayClipAtPoint(_removeGround, PlayerController.Instance.transform.position);
            GetComponent<SpriteRenderer>().sprite = _groundRemovedOne;      
            
            _firstImageUpdated = true;
        }
        else if (_imageNum == 2)
        {
            if(_secondImageUpdated) return;

            AudioSource.PlayClipAtPoint(_removeGround, PlayerController.Instance.transform.position);
            GetComponent<SpriteRenderer>().sprite = _groundRemovedTwo;

            if (_key != null && _hasKey)
            {
                if(GetComponent<BoxCollider2D>().enabled)
                {
                    _key.SetActive(true);

                    if (_key.GetComponent<Key>().PickedUp)
                        _hasKey = false;
                }                
                else                            
                    _key.SetActive(false);
                
            }

            
           
            _imageNum = 3;

            _secondImageUpdated = true;  
        }
        else if(_imageNum == 3)
        {
            GetComponent<SpriteRenderer>().sprite = _groundRemovedTwo;

            if(Key != null)
            {
                if (GetComponent<BoxCollider2D>().enabled)
                {
                    if (_hasKey)
                    {
                        _key.SetActive(true);
                        if (_key.GetComponent<Key>().PickedUp)
                            _hasKey = false;
                    }
                }
                else
                {
                    _key.SetActive(false);
                }
            }           
                       
            GetComponent<ClickableObject>().CanBeSearched = false;
        }
    }
   
}
