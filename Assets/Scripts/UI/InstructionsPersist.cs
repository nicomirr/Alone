using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsPersist : MonoBehaviour
{
    static InstructionsPersist Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
