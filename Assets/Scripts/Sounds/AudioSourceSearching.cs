using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioSourceSearching : MonoBehaviour
{    
    void Update()
    {
        Volume();
    }

    void Volume()
    {
        this.GetComponent<AudioSource>().volume = 1f * GameVolume.Instance.CurrentVolume();
    }
}
