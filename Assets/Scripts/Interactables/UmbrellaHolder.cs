using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaHolder : MonoBehaviour
{
    [SerializeField] GameObject _umbrellaHolder;
    [SerializeField] Sprite _noBlueUmbrella;      
    [SerializeField] Sprite _hasBlueUmbrella;      
    

    void Update()
    {
        if (ScenesInGame.Instance.GetIsFlashback())
        {
            _umbrellaHolder.GetComponent<SpriteRenderer>().sprite = _hasBlueUmbrella;
            return;
        }

        if (!GetComponent<ClickableObject>().HasObject)
        {
            _umbrellaHolder.GetComponent<SpriteRenderer>().sprite = _noBlueUmbrella;
        }

        
    }
}
