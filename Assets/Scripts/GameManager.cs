using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;

    [SerializeField] private InputActionReference leftStickAction;

    private void Start()
    {
        // Instantiate PlayerMovement and initialize
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.AddComponent<PlayerMovement>();
        playerStatus = player.AddComponent<PlayerStatus>();
        playerMovement.SetupMobileInput(leftStickAction);
        playerMovement.Initialize();      
        playerStatus.Initialize();
        // Other initialization code
    }

    private void Update()
    {
        // Update game-related functionality
        // ...
    }
}