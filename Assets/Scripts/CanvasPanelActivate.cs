using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPanelActivate : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject gemAndcoinPanel;
    public GameObject missionPanel;
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("InitScene");
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
