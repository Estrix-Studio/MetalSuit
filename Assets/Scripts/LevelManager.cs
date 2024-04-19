using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class LevelManager : MonoBehaviour
{
    // TODO: Implement LevelManager
    [SerializeField] private Object finishPlanePrefab;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LoadingScene")) 
            LoadLevel(PlayerData.Level);
    }

    // Start is called before the first frame update
    private void Start()
    {
        finishPlanePrefab = GameObject.Find("WinningArea");
        if (finishPlanePrefab == null)
            Debug.Log("Winning Area is not assigned or renamed! Currently: " + finishPlanePrefab);
    }

    // Update is called once per frame
    private void LoadLevel(int level)
    {
        PlayerData.Level = level;
        SceneManager.LoadSceneAsync("InitScene");
    }
    
    private void GenerateNewLevel()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}