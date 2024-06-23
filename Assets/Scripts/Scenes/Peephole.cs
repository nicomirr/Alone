using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peephole : MonoBehaviour
{
    [SerializeField] Sprite _peephole1;
    [SerializeField] Sprite _peephole2;
    [SerializeField] Sprite _peephole3;
    Image _peephole;

    private void Awake()
    {
        _peephole = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(PeepholeAnimation());
    }

    IEnumerator PeepholeAnimation()
    {
        for(int i = 0; i < 20; i++)
        {
            _peephole.sprite = _peephole1;
            yield return new WaitForSeconds(0.1f);

            _peephole.sprite = _peephole2;
            yield return new WaitForSeconds(0.1f);

            _peephole.sprite = _peephole3;
            yield return new WaitForSeconds(0.1f);
        }
                
    }
   
}
