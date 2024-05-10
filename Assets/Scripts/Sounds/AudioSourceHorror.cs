using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceHorror : MonoBehaviour, IDataPersistence
{
    public static AudioSourceHorror Instance;

    bool _isPlaying;
    bool _soundStarted;
    bool _soundStopped;

    private void Awake()
    {
        if(Instance != this &&  Instance != null) 
        { 
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if(_isPlaying)
            GetComponent<AudioSource>().Play();
    }

    public void LoadData(GameData data)
    {
        _isPlaying = data.horrorSoundIsPlaying;
        _soundStarted = data.horrorSoundStarted;
        _soundStopped = data.horrorSoundStopped;
    }

    public void SaveData(ref GameData data)
    {
        data.horrorSoundIsPlaying = _isPlaying;
        data.horrorSoundStarted = _soundStarted;
        data.horrorSoundStopped = _soundStopped;
    }

    public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }

    private void Update()
    {
        if(_isPlaying && !_soundStarted)
        {
            GetComponent<AudioSource>().Play();
            _soundStarted = true;
            _soundStopped = false;
        }
        else if (!_isPlaying && !_soundStopped)
        {
            GetComponent<AudioSource>().Stop();
            _soundStopped = true;
            _soundStarted = false;
        }
    }
}
