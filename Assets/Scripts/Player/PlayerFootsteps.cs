using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerFootsteps : MonoBehaviour
{
    AudioSource footstepsSound;
    PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        footstepsSound = playerController.GetComponent<AudioSource>();
    }
        
    private void Update()
    {
        Volume();

        if (playerController.GetPlayerHasSideMovement())
            footstepsSound.enabled = true;
        else
            footstepsSound.enabled = false;
    }

    void Volume()
    {
        footstepsSound.volume = 1f * GameVolume.Instance.CurrentVolume();
    }
}
