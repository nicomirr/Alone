using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DadsNote : MonoBehaviour
{
    [SerializeField] GameObject noteText;

    private void Update()
    {
        Language();
    }

    void Language()
    {
        if(LanguageManager.Instance.Language == "en")
        {
            noteText.GetComponent<TextMeshProUGUI>().text = "It's been a year already since Emma left us. So young and full of life. I remember when the four of us were a happy family together. When we got married we did not expect to have such beautiful childrens. Their birth was my happiest moment.  Those vacations on the beach were our last ones all the four together. It was like a farewell trip for her. Soon after that the illness became stronger and stronger. Not much was left to be done. Emma, we really miss you around the house.";
        }
        else if(LanguageManager.Instance.Language == "es")
        {
            noteText.GetComponent<TextMeshProUGUI>().text = "Ha pasado ya un año desde que Emma nos dejó. Tan jóven y llena de vida. Recuerdo cuando los cuatro éramos una familia feliz. Cuando nos casamos no esperábamos tener unos hijos tan hermosos. Su nacimiento fue mi momento de mayor felicidad. Aquellas vacaciones en la playa fueron las últimas estando los cuatro. Fue como un viaje de despedida para ella. Al poco tiempo de volver, su enfermedad empeoró. No quedaba mucho por hacer. Emma, todos te extrañamos mucho en la casa.";
        }
    }
}
