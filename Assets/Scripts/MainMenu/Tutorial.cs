using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] bool lookingFront;
    [SerializeField] bool lookingBack;
    [SerializeField] bool walking;
            
    
    void Update()
    {
        if(lookingFront)        
            GetComponent<Animator>().SetBool("isLookingFront", true);
        else if (lookingBack)
            GetComponent<Animator>().SetBool("isLookingBack", true);
        else if(walking)
            GetComponent<Animator>().SetBool("isWalking", true);

    }
}
