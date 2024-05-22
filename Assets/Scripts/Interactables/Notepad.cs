using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Notepad : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] GameObject _computerZoom;
    [SerializeField] GameObject _notepadText;
    Animator _animator;
    bool _notepadBeenOpened;


    private void Awake()
    {
        _animator = _computerZoom.GetComponent<Animator>();
    }        

    public void SaveData(ref GameData data)
    {
        data.notepadBeenOpened = _notepadBeenOpened;
    }

    public void LoadData(GameData data)
    {
        _notepadBeenOpened = data.notepadBeenOpened;
    }

    public bool NotepadBeenOpened { get => _notepadBeenOpened; set => _notepadBeenOpened = value; }

    private void Update()
    {
        Language();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _animator.SetBool("notepadOpen", true);
        _notepadBeenOpened = true;
        _notepadText.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    void Language()
    {
        if (LanguageManager.Instance.Language == "en")
            _notepadText.GetComponent<TextMeshPro>().text = "Reminder: If a key is missing, check in plants. I have lost keys while watering them a lot of times.";
        else if (LanguageManager.Instance.Language == "es")
            _notepadText.GetComponent<TextMeshPro>().text = "Recordatorio: Si hay alguna llave perdida, buscar en las plantas. Ya perdí muchas llaves regando.";
    }
}
