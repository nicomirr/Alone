using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbackWhiteLayer : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Color color = _spriteRenderer.color;

        if (ScenesInGame.Instance.GetIsFlashback())
        {
            color.a = 0.098f;
            _spriteRenderer.color = color;
        }
        else
        {
            color.a = 0f;
            _spriteRenderer.color = color;
        }
    }

}
