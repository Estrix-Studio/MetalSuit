using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadScene : MonoBehaviour
{
    //public Slider loadingBar;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("InitScene");
        //StartCoroutine(LoadSceneAsynchronously());
        //SceneManager.LoadSceneAsync("InitScene");
    }

    private IEnumerator LoadSceneAsynchronously()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("InitScene");
        //var operation = SceneManager.LoadSceneAsync("InitScene");
        //operation.allowSceneActivation = false;



        //while (!operation.isDone)
        //{
        //    var progress = Mathf.Clamp01(operation.progress / 0.9f);
        //    loadingBar.value = progress;

        //    if (operation.progress >= 0.9f)
        //    {
        //        yield return new WaitForSeconds(1f);
        //        operation.allowSceneActivation = true;
        //    }

        //    yield return null;
        //}
    }
}
