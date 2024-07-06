using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Padlock : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<Button> buttons;

    [SerializeField] AudioClip _inputSound;
    [SerializeField] AudioClip _padlockUnlockSound;

    [SerializeField] Sprite _padlockOpen;
    [SerializeField] Image _padlockImage;

    [SerializeField] Sprite _noPadlockMetalCabinet;
    [SerializeField] SpriteRenderer _imageToChange;
    
    bool _notClickable;    

    [SerializeField] GameObject _padlock;
    [SerializeField] GameObject _backButton;

    string _password = "436";  //ORIGINAL: 364
    string _passwordInput;
    int _passwordTotalNumbers = 3;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GetComponent<ClickableObject>().CanBeSearched) return;

        if (Pause.Instance.IsPaused) return;
        if (!PlayerInventory.Instance.IsUsingFlashlight) return;
        if (ButtonsGrid.Instance.GetCurrentAction() != "Search" && ButtonsGrid.Instance.GetCurrentAction() != "Busar") return;
        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        EnablePadlock();
    }

    void EnablePadlock()
    {       
        PlayerController.Instance.SetIsInteractingWithEnviroment(true);

        _padlock.SetActive(true);
        _backButton.SetActive(true);    
    }

    

    public void BackButton()
    {
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        _padlock.SetActive(false);
        _backButton.SetActive(false);
        _passwordInput = "";
    }

    public void InputNumber(string num)
    {
        AudioSource.PlayClipAtPoint(_inputSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
        _passwordInput = _passwordInput + num;

        if(_passwordInput == _password)
        {
            _imageToChange.sprite = _noPadlockMetalCabinet;
            GetComponent<ClickableObject>().CanBeSearched = false;

            StartCoroutine(PadlockUnlock());           
            return;
        }

        if (_passwordInput.Length >= _passwordTotalNumbers)
            _passwordInput = "";
    }

    IEnumerator PadlockUnlock()
    {
        _padlockImage.sprite = _padlockOpen;
        AudioSource.PlayClipAtPoint(_padlockUnlockSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
        DisablePadlockButtons();                
        yield return new WaitForSeconds(1.3f);
              
        BackButton();
        

        if (LanguageManager.Instance.Language == "en")
        {
            TextBox.Instance.ShowText("I unlocked it.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
        }
        else if (LanguageManager.Instance.Language == "es")
        {
            TextBox.Instance.ShowText("Lo desbloqueé.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
        }
                
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D));

        yield return new WaitForSeconds(0.1f);

        GetComponent<ClickableObject>().HasObject = true;

    }

    void DisablePadlockButtons()
    {
        foreach(Button button in buttons)
        {
            button.enabled = false;
        }

        _backButton.GetComponentInChildren<Button>().enabled = false;
    }

}
