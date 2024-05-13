using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color32 unselectedColor;
    [SerializeField] Color32 selectedColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = unselectedColor;

    }
}
