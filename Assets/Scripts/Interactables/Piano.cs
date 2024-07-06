using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piano : MonoBehaviour, IPointerClickHandler
{    
    bool _notClickable;
    RoomLightStatus _roomLightStatus;

    [SerializeField] GameObject _piano;
    [SerializeField] GameObject _backButton;

    [SerializeField] PianoNote _Do;
    [SerializeField] PianoNote _Re;
    [SerializeField] PianoNote _Mi;
    [SerializeField] PianoNote _Fa;
    [SerializeField] PianoNote _Sol;
    [SerializeField] PianoNote _La;
    [SerializeField] PianoNote _Si;
    [SerializeField] PianoNote _DoSos;
    [SerializeField] PianoNote _ReSos;
    [SerializeField] PianoNote _FaSos;
    [SerializeField] PianoNote _SolSos;
    [SerializeField] PianoNote _SiB;
        
    int _correctNotesPlayed = 0;
    int _neededCorrectNotes = 6;

    TextMeshPro _playerText;

    [SerializeField] GameObject _glitchEffect;
    [SerializeField] AudioClip _glitchSound;

    [SerializeField] SpriteRenderer _blurEffect;

    private void Awake()
    {
        _roomLightStatus = FindObjectOfType<RoomLightStatus>();
        _playerText = GameObject.Find("PlayerText").GetComponent<TextMeshPro>();
    }    

    public int GetCorrectNotesPlayed() { return _correctNotesPlayed; }

    private void Update()
    {
        if(_correctNotesPlayed == _neededCorrectNotes && !PlayerController.Instance.GetSongPlayed())
        {
            PlayerController.Instance.SetSongPlayed(true);
            StartCoroutine(PianoSongPlayed());
        }
    }

    public void AddCorrectNote()
    {
        _correctNotesPlayed++;
    }

    public void RestartCorrectNotes()
    {
        _correctNotesPlayed = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Pause.Instance.IsPaused) return;
        
        LightsOutStatus();
        if (LightControl.LightsOut) return;

        if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
        {                       
            EnablePiano();
        }
    }

    void LightsOutStatus()
    {
        if (!LightControl.LightsOut) return;

        if (ButtonsGrid.Instance.GetCurrentAction() == "Use" || ButtonsGrid.Instance.GetCurrentAction() == "Usar")
        {
            if (LanguageManager.Instance.Language == "en")
                TextBox.Instance.ShowText("It doesn't work without power.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
            else if (LanguageManager.Instance.Language == "es")
                TextBox.Instance.ShowText("No funciona sin electricidad.", GetComponent<ClickableObject>().InventoryObject, GetComponent<ClickableObject>());
        }        
    }

    void EnablePiano()
    {
        if(!_roomLightStatus.GetRoomHasLight() && !PlayerInventory.Instance.IsUsingFlashlight) return;

        _notClickable = MouseBehaviour.Instance.NotClickable;
        if (_notClickable) return;

        PlayerController.Instance.SetIsInteractingWithEnviroment(true);

        _piano.SetActive(true);
        _backButton.SetActive(true);
    }
   
    public void BackButton()
    {
        PlayerController.Instance.SetIsInteractingWithEnviroment(false);

        _piano.SetActive(false);
        _backButton.SetActive(false);
    }

    IEnumerator PianoSongPlayed()
    {
        UnityEngine.Cursor.visible = false;

        ScenesInGame.Instance.SetSceneIsPlaying(true);
        PlayerController.Instance.GetComponent<Animator>().SetBool("isLookingBack", true);

        _Do.enabled = false;
        _Re.enabled = false;
        _Mi.enabled = false;
        _Fa.enabled = false;
        _Sol.enabled = false;
        _La.enabled = false;
        _Si.enabled = false;
        _DoSos.enabled = false;
        _ReSos.enabled = false;
        _FaSos.enabled = false;
        _SolSos.enabled = false;
        _SiB.enabled = false;

        //_backButton.GetComponent<Button>().enabled = false;

        yield return new WaitForSeconds(1.2f);
        BackButton();

        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "That song...";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Esa canción...";
        yield return new WaitForSeconds(3.0f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "Emma used to play it all the time.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Emma solía tocarla todo el tiempo.";
        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.3f);

        _blurEffect.material.SetFloat("_BlurAmount", 0.002f);
        yield return new WaitForSeconds(1.5f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I'm feeling dizzy...";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Me estoy sintiendo mareado...";
        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        _glitchEffect.SetActive(true);
        AudioSource.PlayClipAtPoint(_glitchSound, PlayerController.Instance.transform.position, 1f * GameVolume.Instance.CurrentVolume());
        yield return new WaitForSeconds(0.4f);

        _glitchEffect.SetActive(false);
        _blurEffect.material.SetFloat("_BlurAmount", 0f);
        yield return new WaitForSeconds(2.2f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "What was that?";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué fue eso?";
        yield return new WaitForSeconds(2.8f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "Something feels different.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Algo se siente diferente.";
        yield return new WaitForSeconds(2.8f);

        _playerText.text = "";
        _Do.enabled = true;
        _Re.enabled = true;
        _Mi.enabled = true;
        _Fa.enabled = true;
        _Sol.enabled = true;
        _La.enabled = true;
        _Si.enabled = true;
        _DoSos.enabled = true;
        _ReSos.enabled = true;
        _FaSos.enabled = true;
        _SolSos.enabled = true;
        _SiB.enabled = true;
        //_backButton.GetComponent<Button>().enabled = true;

        ScenesInGame.Instance.SetSceneIsPlaying(false);

        UnityEngine.Cursor.visible = true;
    }
    
}
