using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasPanelActivate : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject gemAndCoinPanel;
    public GameObject missionPanel;
    public GameObject skinsPanel;
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

    private IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        var operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);

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
        gemAndCoinPanel.SetActive(!gemAndCoinPanel.activeSelf);
    }

    public void ToggleCoinAndGemPanelExit()
    {
        gemAndCoinPanel.SetActive(false);
    }

    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(true);
    }

    public void ToggleMissionPanelExit()
    {
        missionPanel.SetActive(false);
    }
    
    public void ToggleSkinsPanel()
    {
        skinsPanel.SetActive(true);
    }

    public void ToggleSkinsExit()
    {
        skinsPanel.SetActive(false);
    }
}






