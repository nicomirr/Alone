using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameVolume : MonoBehaviour, IDataPersistence
{
    public static GameVolume Instance;

    int _volume = 10;
    int _minVolume = 0;
    int _maxVolume = 10;

    [SerializeField] Image _volumeImage;
    [SerializeField] Sprite _image1;
    [SerializeField] Sprite _image2;
    [SerializeField] Sprite _image3;
    [SerializeField] Sprite _image4;
    [SerializeField] Sprite _image5;
    [SerializeField] Sprite _image6;
    [SerializeField] Sprite _image7;
    [SerializeField] Sprite _image8;
    [SerializeField] Sprite _image9;
    [SerializeField] Sprite _image10;
    [SerializeField] Sprite _image11;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadData(GameData data)
    {
        _volume = data.volume;
    }

    public void SaveData(ref GameData data)
    {
        data.volume = _volume;
    }

    private void Update()
    {
        VolumeAppearance();
    }

    void VolumeAppearance()
    {       
        if (_volume == 0)
        {
            _volumeImage.sprite = _image1;
        }
        else if (_volume == 1)
        {
            _volumeImage.sprite = _image2;
        }
        else if (_volume == 2)
        {
            _volumeImage.sprite = _image3;
        }
        else if (_volume == 3)
        {            
            _volumeImage.sprite = _image4;
        }
        else if (_volume == 4)
        {            
            _volumeImage.sprite = _image5;
        }
        else if (_volume == 5)
        {            
            _volumeImage.sprite = _image6;
        }
        else if (_volume == 6)
        {            
            _volumeImage.sprite = _image7;
        }
        else if (_volume == 7)
        {            
            _volumeImage.sprite = _image8;
        }
        else if (_volume == 8)
        {            
            _volumeImage.sprite = _image9;
        }
        else if (_volume == 9)
        {            
            _volumeImage.sprite = _image10;
        }
        else if (_volume == 10)
        {            
            _volumeImage.sprite = _image11;
        }
    }

    public void AddVolume()
    {
        if (_volume < _maxVolume)
            _volume++;
    }

    public void LowerVolume()
    {
        if (_volume > _minVolume)
            _volume--;
    }
    
    public float CurrentVolume()
    {
        return _volume * 0.1f;
    }
}
