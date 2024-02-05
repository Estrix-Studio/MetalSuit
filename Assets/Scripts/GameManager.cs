using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;

    [SerializeField] private InputActionReference leftStick;

    private void Start()
{
    // Instantiate PlayerMovement and initialize
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    playerMovement = player.AddComponent<PlayerMovement>();
    playerStatus = player.AddComponent<PlayerStatus>();
    playerMovement.SetupMobileInput(leftStick);
    playerMovement.Initialize();
    playerStatus.Initialize();
}
}