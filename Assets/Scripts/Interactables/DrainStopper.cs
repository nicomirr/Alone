using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrainStopper : MonoBehaviour, IPointerClickHandler
{    
    [SerializeField] GameObject _bathtub;
    [SerializeField] Sprite _drainStopperOnDrain;
    [SerializeField] AudioClip _drainStopperSound;

    public void Update()
    {        
        if(_bathtub.GetComponent<Shower>().HasDrainStopper)
        {
            GetComponent<SpriteRenderer>().sprite = _drainStopperOnDrain;

            GetComponent<ClickableObject>().LookAtText = "It's the drain stopper.";
            GetComponent<ClickableObject>().LookAtTextSpanish = "Es el tap�n.";

            GetComponent<ClickableObject>().PickupText = "No.";
            GetComponent<ClickableObject>().PickupTextSpanish = "No.";            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(PlayerInventory.Instance.IsUsingBathroomDrainStopper)
        {
            _bathtub.GetComponent<Shower>().HasDrainStopper = true;
            AudioSource.PlayClipAtPoint(_drainStopperSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
            PlayerInventory.Instance.DestroyCurrentItem();
        }
    }    
}
