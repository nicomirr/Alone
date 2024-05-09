using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingScreen : MonoBehaviour
{
    [SerializeField] GameObject _thanksText;

    void Update()
    {
        Language();
    }

    void Language()
    {
        if (LanguageManager.Instance.Language == "en")
            _thanksText.GetComponent<TextMeshProUGUI>().text = "Thanks for playing the demo \n\n Follow us on TikTok, Twitter, Instagram and Itch.io \n Find us as \"nikaristudios\" in all our socials" ;
        else if (LanguageManager.Instance.Language == "es")
            _thanksText.GetComponent<TextMeshProUGUI>().text = "Gracias por jugar la demo. \n\n Sigannos en TikTok, Twitter, Instagram e Itch.io \n Encuentrennos como \"nikaristudios\" en todas nuestras redes";
    }
}
