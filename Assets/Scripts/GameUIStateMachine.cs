using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class GameUIStateMachine : MonoBehaviour
{
    public GameObject playScreen, finishBar, WinScreen, LoseScreen;

    public Text coinText, gemText, dataText;

    private void Start()
    {
        
    }

    private void Update()
    {

      
            IncrementCoins();
            IncrementGems();
            IncrementData();

        
 
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


    void GamePause()
    {
        Time.timeScale = 0;
    }

    void GameResume()
    {
        Time.timeScale = 1;
    }

    public void IncrementCoins()
    {
        int coins = 0;

        coins += 1;
        coinText.text = coins.ToString();

    }
    public void IncrementGems()
    {
        int gems = 0;

        gems += 1;
        gemText.text = gems.ToString();

    }

    public void IncrementData()
    {
        int data = 0;
        data += 1;

        dataText.text = data.ToString();

    }

    public void CloseProgram()
    {
        Application.Quit();
       
    }



}
