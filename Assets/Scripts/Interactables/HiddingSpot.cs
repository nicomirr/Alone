using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HiddingSpot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Sprite _imageHidding;
    [SerializeField] GameObject _imageToChange;
    bool _notClickable;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerController.Instance.GetMustHide())
        {
            _notClickable = MouseBehaviour.Instance.NotClickable;
            if (_notClickable) return;

            _imageToChange.GetComponent<SpriteRenderer>().sprite = _imageHidding;

            Color color = PlayerController.Instance.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            PlayerController.Instance.GetComponent<SpriteRenderer>().color = color;

            PlayerController.Instance.SetMustHide(false);
            PlayerController.Instance.SetIsHidding(true);

        }
    }
}
