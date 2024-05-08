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
                    TextBox.Instance.ShowText("Ya est� encendida.");
            }
            else
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("I will turn it on when I am using it.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("La encender� cuando la est� utilizando.");
            }
        }
        else if (ButtonsGrid.Instance.GetCurrentAction() == "Turn Off" || ButtonsGrid.Instance.GetCurrentAction() == "Apagar")
        {
            if (!_isUsingFlashlight)
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("It's already off.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("Ya est� apagada.");
            }
            else
            {
                if (LanguageManager.Instance.Language == "en")
                    TextBox.Instance.ShowText("I will turn it off when I am not using it.");
                else if (LanguageManager.Instance.Language == "es")
                    TextBox.Instance.ShowText("La apagar� cuando no la est� utilizando.");
            }

        }
    }
}
