using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PianoNote : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Piano _piano;
    [SerializeField] AudioClip _pianoNote;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        string name = gameObject.name;

        if(Pause.Instance.IsPaused) return;
        AudioSource.PlayClipAtPoint(_pianoNote, PlayerController.Instance.transform.position);
        StartCoroutine(KeyPressed());
        
    }

    IEnumerator KeyPressed()
    {
        if (gameObject.name == "Do" && _piano.GetCorrectNotesPlayed() == 0)
            _piano.AddCorrectNote();
        else if (gameObject.name == "Fa" && (_piano.GetCorrectNotesPlayed() == 1 || _piano.GetCorrectNotesPlayed() == 5))
            _piano.AddCorrectNote();
        else if (gameObject.name == "Sol" && _piano.GetCorrectNotesPlayed() == 2)
            _piano.AddCorrectNote();
        else if (gameObject.name == "La#" && _piano.GetCorrectNotesPlayed() == 3)
            _piano.AddCorrectNote();
        else if (gameObject.name == "La" && _piano.GetCorrectNotesPlayed() == 4)
            _piano.AddCorrectNote();
        else
            _piano.RestartCorrectNotes();

        if (name.Contains("#"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(51,51,51,255);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        yield return new WaitForSeconds(0.5f);

        if(name.Contains("#"))
        {            
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
