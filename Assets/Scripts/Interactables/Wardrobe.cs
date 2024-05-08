using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wardrobe : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Sprite _wardrobeClosed;
    [SerializeField] GameObject _image;
    bool _notClickable;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(PlayerController.Instance.GetMustHide())
        {
            _notClickable = MouseBehaviour.Instance.NotClickable;
            if (_notClickable) return;

            _image.GetComponent<SpriteRenderer>().sprite = _wardrobeClosed;
            
            Color color = PlayerController.Instance.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            PlayerController.Instance.GetComponent<SpriteRenderer>().color = color;
            PlayerController.Instance.SetIsHidding(true);

        }
    }
}
