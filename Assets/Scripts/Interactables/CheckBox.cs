using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public static CheckBox Instance;
    bool boxCheck;  

    private void Awake()
    {
        Instance = this;
    }

    public bool GetBoxCheck() { return boxCheck; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && ScenesInGame.Instance.GetSceneIsPlaying())
            boxCheck = true;
    }
}


