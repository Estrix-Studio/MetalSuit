using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // A function that handles Pause Button inside of the game scene
    private void SetPause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);
    }

    public void PauseGame()
    {
        SetPause(true);
    }

    public void ResumeGame()
    {
        SetPause(false);
    }

    public void BackToMainMenu()
    {
        var scene = SceneManager.GetSceneByName("MainMenuScreen");
        SceneManager.LoadSceneAsync(scene.ToString());
        SceneManager.SetActiveScene(scene);
        SetPause(false);
    }

    public void SaveButton()
    {
        // Sending updated player data to the SaveManager
        SaveManager.SaveGame();
    }
}