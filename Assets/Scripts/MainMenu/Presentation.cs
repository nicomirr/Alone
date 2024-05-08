using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presentation : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject presentation;
    [SerializeField] GameObject AudioSource;

    public void DisablePresentation()
    {
        presentation.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        AudioSource.SetActive(true);
    }

}
