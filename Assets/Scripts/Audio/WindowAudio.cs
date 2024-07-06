using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WindowAudio : MonoBehaviour
{    
    void Update()
    {
        Volume();
    }

    void Volume()
    {
        if(this.GetComponent<AudioSource>() != null)
            this.GetComponent<AudioSource>().volume = 1f * GameVolume.Instance.CurrentVolume();
    }
}
