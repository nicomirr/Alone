using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadBackButton : MonoBehaviour
{
    [SerializeField] GameObject backButton;

    private void Update()
    {
        Language();
    }

    public void IsReadingBackButton()
    {
        PlayerController.Instance.SetIsReading(false);
        GameObject.Find("DadsNote").transform.GetChild(0).gameObject.SetActive(false);
    }

    void Language()
    {
        if (LanguageManager.Instance.Language == "en")
            backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        else if (LanguageManager.Instance.Language == "es")
            backButton.GetComponentInChildren<TextMeshProUGUI>().text = "Atrás";
    }
}
