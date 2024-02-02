using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private InputActionReference leftStickAction;
    [SerializeField] private InputActionReference attackButtonAction;

    private void Start()
    {
        // Instantiate PlayerMovement and initialize
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.AddComponent<PlayerMovement>();
        playerMovement.Initialize();
        playerMovement.SetupMobileInput(leftStickAction, attackButtonAction);
        // Other initialization code
    }

    private void Update()
    {
        // Update game-related functionality
        // ...
    }
}
