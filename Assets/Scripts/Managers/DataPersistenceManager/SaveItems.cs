using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveItems : MonoBehaviour
{
    public static SaveItems Instance;

    private void Awake()
    {        
        DontDestroyOnLoad(gameObject);
        int saveItemsObjectsAmount = FindObjectsOfType<SaveItems>().Length;

        if(saveItemsObjectsAmount > 1 )
            Destroy(Instance.gameObject);

        Instance = this;
    }

}
