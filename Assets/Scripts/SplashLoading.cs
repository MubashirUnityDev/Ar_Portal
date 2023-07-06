using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SplashLoading : MonoBehaviour
{
    public string nextSceneName;
    public TextMeshProUGUI loadingText;
    public Image ProgressBarImage;

    private void Start()
    {
        // Start the scene loading in the background
        StartCoroutine(LoadNextSceneAsync());
    }

    private IEnumerator LoadNextSceneAsync()
    {
        // Create an operation to load the next scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);

        // Disable automatic scene activation
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Update the loading progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // Normalize progress to 0-1 range
            loadingText.text = $"Loading... {Mathf.RoundToInt(progress * 100)}%";
            ProgressBarImage.fillAmount = progress;

            // Check if the operation has reached the minimum scene activation progress
            if (progress >= 0.9f)
            {
                // Allow the scene to activate
                yield return new WaitForSeconds(2);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
