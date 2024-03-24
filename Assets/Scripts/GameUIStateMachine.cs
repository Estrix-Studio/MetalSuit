using UnityEngine;
using UnityEngine.UI;

public class GameUIStateMachine : MonoBehaviour
{
    public GameObject playScreen, finishBar, WinScreen, LoseScreen;


    public Text coinText, gemText, dataText;

    public static int level = 0;
    public static int coins = 0;
    public static int gems = 0;
    public static int data = 0;


    private void Start()
    {
        level = SavingManager.LoadLevel();
        coins = SavingManager.LoadCoins();
        gems = SavingManager.LoadGem();
        data = SavingManager.LoadData();
    }

    private void Update()
    {

      
            //IncrementCoins();
            //IncrementGems();
            //IncrementData();

        
 
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
        GamePause();

        playScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(true);
        finishBar.SetActive(false);
    }

    public void ChangeToLoseScreen()
    {
        GamePause();
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
        
        coins += 1;
        coinText.text = coins.ToString();
    }

    public void IncrementGems()
    {
        

        gems += 1;
        gemText.text = gems.ToString();
    }

    public void IncrementData()
    {
        
        data += 1;

        dataText.text = data.ToString();
    }

    public void CloseProgram()
    {
        Application.Quit();
    }
}