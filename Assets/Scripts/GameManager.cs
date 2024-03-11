using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionReference contactAction;
    [SerializeField] private InputActionReference positionAction;
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;

    private void Start()
    {
        // Instantiate PlayerMovement and initialize
        var player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.AddComponent<PlayerMovement>();
        playerStatus = player.AddComponent<PlayerStatus>();
        playerMovement.SetupMobileInput(contactAction, positionAction);
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