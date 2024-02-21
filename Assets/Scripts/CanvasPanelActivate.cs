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
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleCoinAndGemPanel()
    {
        gemAndcoinPanel.SetActive(!gemAndcoinPanel.activeSelf);
    }

    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(!missionPanel.activeSelf);
    }
}
