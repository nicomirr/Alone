using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHolder : MonoBehaviour
{
    [SerializeField] GameObject _umbrellaHolder;
    [SerializeField] Sprite _noBlueUmbrella;      
    

    void Update()
    {
        if(!GetComponent<ClickableObject>().HasObject)
        {
            _umbrellaHolder.GetComponent<SpriteRenderer>().sprite = _noBlueUmbrella;
        }
    }
}
