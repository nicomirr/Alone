using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeySound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] AudioClip keyGrabSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(ButtonsGrid.Instance.GetCurrentAction() == "Pick Up" || ButtonsGrid.Instance.GetCurrentAction() == "Agarrar")
            AudioSource.PlayClipAtPoint(keyGrabSound, Camera.main.transform.position, 0.9f);
    }
}
