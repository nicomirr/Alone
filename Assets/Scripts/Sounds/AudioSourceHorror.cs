using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceHorror : MonoBehaviour, IDataPersistence
{
    bool isPlaying;
    bool soundStarted;
    bool soundStopped;


    private void Start()
    {
        if(isPlaying)
            GetComponent<AudioSource>().Play();
    }

    public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData data)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if(isPlaying && !soundStarted)
        {
            GetComponent<AudioSource>().Play();
            soundStarted = true;
            soundStopped = false;
        }
        else if (!isPlaying && !soundStopped)
        {
            GetComponent<AudioSource>().Stop();
            soundStopped = true;
            soundStarted = false;
        }
    }
}
