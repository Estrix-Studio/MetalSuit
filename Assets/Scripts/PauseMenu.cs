using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToMainMenu(string sceneName)
    {
        SceneManager.LoadScene("MainMenuScreen");
    }

    public void SaveButton_Pressed()
    {
        SavingManager.SaveGame(GameUIStateMachine.level, GameUIStateMachine.coins, GameUIStateMachine.gems, GameUIStateMachine.data);
    }
}