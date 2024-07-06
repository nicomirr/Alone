using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RadioDial : MonoBehaviour
{   
    [SerializeField] AudioClip _dialSound;
    [SerializeField] GameObject _dial1;
    [SerializeField] GameObject _dial2;

    public void RotateDial1()
    {        
        this.transform.rotation *= Quaternion.Inverse(Quaternion.Euler(0, 0, 36));
        _dial2.transform.rotation *= Quaternion.Inverse(Quaternion.Euler(0, 0, 36));
        AudioSource.PlayClipAtPoint(_dialSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());        
    }
    
    public void RotateDial3()
    {
        this.transform.rotation *= Quaternion.Inverse(Quaternion.Euler(0, 0, 36));       
        _dial1.transform.rotation *= Quaternion.Inverse(Quaternion.Euler(0, 0, 72));        
        AudioSource.PlayClipAtPoint(_dialSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());

       
    }
}
