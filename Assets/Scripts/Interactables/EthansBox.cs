using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EthansBox : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject _boxBlackScreen;

    bool _notClickable;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScenesInGame.Instance.GetSceneIsPlaying()) return;

        if(Pause.Instance.IsPaused) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        StartCoroutine(PickUpBox());
    }

    IEnumerator PickUpBox()
    {
        ScenesInGame.Instance.SetSceneIsPlaying(true);

        _boxBlackScreen.SetActive(true);
        yield return new WaitForSeconds(2);

        PlayerController.Instance.SetHasEthansBox(true);
        ScenesInGame.Instance.SetSceneIsPlaying(false);

        this.gameObject.SetActive(false);

    }
}
