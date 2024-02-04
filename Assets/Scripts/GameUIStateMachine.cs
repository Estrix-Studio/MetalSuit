using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIStateMachine : MonoBehaviour
{
    public GameObject playScreen, finishBar, WinScreen, LoseScreen;




    private void Update()
    {
        
    }

    public void ChangeToPlayScreen()
    {
        playScreen.SetActive(true);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);

        
    }

    public void ChangeToFinishScreen()
    {
        finishBar.SetActive(true);
        playScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    public void ChangeToWinScreen()
    {
        playScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(true);
        finishBar.SetActive(false);
    }

    public void ChangeToLoseScreen()
    {
        playScreen.SetActive(false);
        LoseScreen.SetActive(true);
        WinScreen.SetActive(false);
        finishBar.SetActive(false);
    }


    

    public void CloseProgram()
    {
        Application.Quit();
       
    }



}
