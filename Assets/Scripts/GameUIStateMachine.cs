using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUIStateMachine : MonoBehaviour
{
    public GameObject playScreen, finishScreen, WinScreen, LoseScreen, loadingScreen;
    public Slider loadingBar;

    public void LoadScene(int levelIndex)
    {
       StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }
    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false; 

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            
            if (operation.progress >= 0.9f)
            {
                
                yield return new WaitForSeconds(1f);

               
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void Update()
    {
        
    }

    public void ChangeToPlayScreen()
    {
        

        
    }

    public void ChangeToFinishScreen()
    {
       
    }

    public void ChangeToWinScreen()
    {

      
    }

    public void ChangeToLoseScreen()
    {
      
    }


    

    public void CloseProgram()
    {
        Application.Quit();
       
    }



}
