using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIStateMachine : MonoBehaviour
{
    public static PlayerData PlayerData;
    public GameObject playScreen, finishBar, WinScreen, LoseScreen;
    public Text coinText, gemText, dataText;


    private void Start()
    {
        //PlayerData = SaveManager.LoadGame();
        if (PlayerData == null) return;
        coinText.text = PlayerData.Coins.ToString();
        gemText.text = PlayerData.Gems.ToString();
        dataText.text = PlayerData.Data.ToString();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
    }

    private void Update()
    {
    }

    public void ChangeToPlayScreen()
    {
        playScreen.SetActive(true);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void AddProgressBar()
    {
        finishBar.SetActive(true);
        playScreen.SetActive(true);
    }

    public void ChangeToWinScreen()
    {
        //GamePause();

        playScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(true);
        finishBar.SetActive(false);
    }

    public void ChangeToLoseScreen()
    {
        //GamePause();
        playScreen.SetActive(false);
        LoseScreen.SetActive(true);
        WinScreen.SetActive(false);
        finishBar.SetActive(false);
    }


    private void GamePause()
    {
        Time.timeScale = 0;
    }

    private void GameResume()
    {
        Time.timeScale = 1;
    }

    public void IncrementCoins()
    {
        PlayerData.Coins += 1;
        coinText.text = PlayerData.Coins.ToString();
        SaveManager.SaveGame(PlayerData);
    }

    public void IncrementGems()
    {
        PlayerData.Gems += 1;
        gemText.text = PlayerData.Gems.ToString();
        SaveManager.SaveGame(PlayerData);
    }

    public void IncrementData()
    {
        PlayerData.Data += 1;
        dataText.text = PlayerData.Data.ToString();
        SaveManager.SaveGame(PlayerData);
    }

    public void CloseProgram()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("InitScene");
    }
}