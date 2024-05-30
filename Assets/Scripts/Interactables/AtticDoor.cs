using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AtticDoor : MonoBehaviour, IPointerClickHandler
{
    TextMeshPro _playerText;

    bool _notClickable;
    float _playerMinClickableDistance = 3;
    [SerializeField] GameObject _umbrellaBlackScreen;
    [SerializeField] AudioClip _umbrellaBroken;
    [SerializeField] GameObject _umbrellaHandle;
    [SerializeField] AudioSource _kettleSound;
    [SerializeField] GameObject _screenBlur;

    private void Awake()
    {
        _playerText = GameObject.Find("PlayerText").GetComponent<TextMeshPro>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MouseDistanceMath();
        if (_notClickable) return;

        if (PlayerInventory.Instance.IsUsingUmbrella)
        {
            ScenesInGame.Instance.SetSceneIsPlaying(true);
            StartCoroutine(UsingUmbrella());
        }
    }

    IEnumerator UsingUmbrella()
    {
        PlayerController.Instance.GetComponent<AudioSource>().Stop();
        PlayerController.Instance.GetComponent<Animator>().SetBool("isWalking", false);
        PlayerController.Instance.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        _umbrellaBlackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);

        PlayerController.Instance.transform.position = new Vector2(12.431f, PlayerController.Instance.transform.position.y);
        PlayerController.Instance.GetComponent<Animator>().SetBool("isUsingUmbrella", true);
        yield return new WaitForSeconds(2.2f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I think I can reach it with the umbrella.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Creo que puedo alcanzarlo con el paraguas.";
        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "Almost got it.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Ya casi.";
        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_umbrellaBroken, PlayerController.Instance.transform.position, 0.4f);
        PlayerController.Instance.GetComponent<Animator>().SetBool("isUsingUmbrella", false);
        PlayerInventory.Instance.DestroyCurrentItem();
        PlayerInventory.Instance.AddItem(_umbrellaHandle);
        yield return new WaitForSeconds(3.5f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "Shit. I broke the handle.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Mierda. Rompi la manija.";
        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "It's useless now.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "No sirve más.";
        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I will keep the handle. \nIt may come in handy.";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Conservaré la manija. \n Tal vez me sirva para algo.";
        yield return new WaitForSeconds(3.5f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.6f);

        _kettleSound.Play();

        yield return new WaitForSeconds(1.6f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "What's happening...?";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "¿Qué está pasando...?";
        yield return new WaitForSeconds(3f);

        _playerText.text = "";
        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "It's that sound again...";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Es ese sonido otra vez...";
        yield return new WaitForSeconds(3.4f);

        _playerText.text = "";
        _screenBlur.GetComponent<SpriteRenderer>().material.SetFloat("_BlurAmount", 0.002f);
        PlayerController.Instance.GetComponent<Animator>().SetBool("isDizzy", true);

        yield return new WaitForSeconds(1.3f);

        if (LanguageManager.Instance.Language == "en")
            _playerText.text = "I'm filling dizzy...";
        else if (LanguageManager.Instance.Language == "es")
            _playerText.text = "Me estoy sintiendo mareado...";
        yield return new WaitForSeconds(3f);

        _screenBlur.GetComponent<SpriteRenderer>().material.SetFloat("_BlurAmount", 0.003f);
        _playerText.text = "";
        yield return new WaitForSeconds(2.0f);

        PlayerController.Instance.GetComponent<Animator>().SetBool("isFalling", true);
        yield return new WaitForSeconds(7.0f);

        PlayerController.Instance.SetSceneToLoad("PlayersRoom");

        Color color = PlayerController.Instance.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        PlayerController.Instance.GetComponent<SpriteRenderer>().color = color;
        PlayerController.Instance.transform.position = new Vector2(-3.4f, PlayerController.Instance.transform.position.y);

        _kettleSound.Stop();
        ScenesInGame.Instance.SetIsFlashback(true);
        ScenesInGame.Instance.SetFirstPlayersRoomFlashbackScene(true);        
        SceneManager.LoadScene("PlayersRoom");

    }

    void MouseDistanceMath()
    {
        float objAndPlayerDistance = PlayerController.Instance.transform.position.x - this.transform.position.x;
        _notClickable = objAndPlayerDistance > _playerMinClickableDistance || objAndPlayerDistance < -_playerMinClickableDistance;
    }
}
