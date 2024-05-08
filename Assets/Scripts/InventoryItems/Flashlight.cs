using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    bool _isUsingFlashlight;

    public bool IsUsingFlashlight { get => _isUsingFlashlight; set => _isUsingFlashlight = value; }

    public void UseFlashlight()
    {
        if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
        {
            _isUsingFlashlight = !_isUsingFlashlight;
            PlayerController.Instance.FlashlightSound();
        }
    }

    public void ChangeFlashlightState()
    {        
        if (ButtonsGrid.Instance.GetCurrentAction() == "Turn On" || ButtonsGrid.Instance.GetCurrentAction() == "Encender")
        {
            if (_isUsingFlashlight)
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already on.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está encendida.");
            }
            else
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("I will turn it on when I am using it.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("La encenderé cuando la esté utilizando.");
            }
        }
        else if (ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar")
        {
            if (!_isUsingFlashlight)
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already off.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya está apagada.");
            }
            else
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("I will turn it off when I am not using it.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("La apagaré cuando no la esté utilizando.");
            }

        }
    }
}
