using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class CanvasPanelActivate : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject gemAndcoinPanel;
    public GameObject missionPanel;
    public GameObject loadingScreen;
    public Slider loadingBar;


    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("InitScene");
    }

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

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void ToggleSettingsExitPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleCoinAndGemPanel()
    {
        gemAndcoinPanel.SetActive(!gemAndcoinPanel.activeSelf);
    }

    public void ToggleCoinAndGemPanelExit()
    {
        gemAndcoinPanel.SetActive(false);
    }

    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(true);
    }

    public void ToggleMissionPanelExit()
    {
        missionPanel.SetActive(false);
    }
}
