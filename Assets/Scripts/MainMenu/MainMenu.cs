using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text progressText;
    

    public void StartGame()
    {
        // SceneManager.LoadScene(1);
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        var scene = SceneManager.LoadSceneAsync(1);
        scene.allowSceneActivation = true;

        do
        {
            var progress = Mathf.Clamp01(scene.progress / 0.9f);
            progressBar.transform.localScale = new Vector3(progress, 1, 1);
            yield return null;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
    }
}
