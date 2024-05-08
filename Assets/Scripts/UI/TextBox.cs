using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TextBox : MonoBehaviour
{
    public static TextBox Instance;

    TextMeshPro _textBoxText;

    float _playerTextBoxOffset = 0.3f;
    static bool _textIsShowing = false;

    static bool _hasJustExited = false;

    private void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _textBoxText = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        FollowPlayer();

        if (_textIsShowing)
        {
            if (Input.GetMouseButtonDown(0))
                ExitText();
        }
        else if (_hasJustExited)
        {
            if (Input.GetMouseButtonDown(0))
                _hasJustExited = false;
        }
    }

    public bool GetTextIsShowing() { return _textIsShowing; }
    public void SetTextIsShowing(bool value) { _textIsShowing = value; }

    public void FixPlayerPos(ClickableObject obj)
    {
        if (PlayerController.Instance.GetLookingFront() || PlayerController.Instance.GetLookingBack()) return;
        if(PlayerController.Instance.GetIsSearching()) return;

        if (PlayerController.Instance.transform.position.x - obj.transform.position.x < 0)
            PlayerController.Instance.transform.localScale = new Vector2(1, 1);
        else if (PlayerController.Instance.transform.position.x - obj.transform.position.x > 0)
            PlayerController.Instance.transform.localScale = new Vector2(-1, 1);
    }

    void FollowPlayer()
    {
        this.transform.position = new Vector2(PlayerController.Instance.transform.position.x + _playerTextBoxOffset, this.transform.position.y);
    }

    public void ShowText(string text)
    {
        if (_hasJustExited)
        {
            _hasJustExited = false;
            return;
        }

        if (!_textIsShowing)
        {           
            PlayerController.Instance.SetIsTalking(true);
            _textBoxText.text = text;
            _textIsShowing = true;
        }
    }
    public void ShowText(string text, bool isInventoryObject, ClickableObject obj)
    {
        if (_hasJustExited)
        {
            _hasJustExited = false;
            return;
        }

        if (!_textIsShowing)
        {
            if(!isInventoryObject)
                FixPlayerPos(obj);

            PlayerController.Instance.SetIsTalking(true);
            _textBoxText.text = text;
            _textIsShowing = true;
        }
    }

    void ExitText()
    {
        PlayerController.Instance.SetIsTalking(false);
        _textBoxText.text = "";
        _textIsShowing = false;
        _hasJustExited = true;
    }

}
