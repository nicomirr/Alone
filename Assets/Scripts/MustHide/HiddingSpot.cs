using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HiddingSpot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Sprite _imageOpen;
    [SerializeField] Sprite _imageHidding;
    [SerializeField] GameObject _imageToChange;
    bool _notClickable;

    private void Update()
    {
        if (this.gameObject.name == "Wardrobe") return;
        if (PlayerController.Instance.GetMustHide() || PlayerController.Instance.GetIsHidding() || ScenesInGame.Instance.GetSceneIsPlaying()) return;
        _imageToChange.GetComponent<SpriteRenderer>().sprite = null;

        if(!PlayerController.Instance.GetIsHidding() && ScenesInGame.Instance.GetSecondParentsRoomScenePlayed())
        {
            _imageToChange.GetComponent<SpriteRenderer>().sprite = _imageOpen;
        }

    }

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
