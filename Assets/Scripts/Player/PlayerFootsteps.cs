using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (playerController.GetPlayerHasSideMovement())
            footstepsSound.enabled = true;
        else
            footstepsSound.enabled = false;
    }
}
