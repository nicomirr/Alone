using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour,IPointerClickHandler
{
    bool _pickedUp;

    public bool PickedUp { get => _pickedUp; set => _pickedUp = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        _pickedUp = true;
    }

   
}
