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
    public GameObject creditsScreen;
    //public Slider loadingBar;

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("InitScene");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void ToggleCreditsPanel()
    {
        creditsScreen.SetActive(true);
    }

    public void ToggleCreditsExitPanel()
    {
        creditsScreen.SetActive(false);
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