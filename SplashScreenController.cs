using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    [Header("Splash Screen Settings")]
    public Image logo1Image;            // North Harbor Image object
    public Image logo2Image;            // Xanthic Image object
    public float displayDuration = 2f;
    public float fadeDuration = 0.5f;
    public string nextSceneName;

    private bool canSkip = true;

    void Start()
    {
        // Start with both invisible
        SetImageAlpha(logo1Image, 0f);
        SetImageAlpha(logo2Image, 0f);
        
        StartCoroutine(PlaySplashSequence());
    }

    void Update()
    {
        if (canSkip && Input.anyKeyDown)
        {
            StopAllCoroutines();
            LoadNextScene();
        }
    }

    IEnumerator PlaySplashSequence()
    {
        // Show Logo 1 (North Harbor)
        yield return StartCoroutine(ShowImage(logo1Image));
        
        // Show Logo 2 (Xanthic)
        yield return StartCoroutine(ShowImage(logo2Image));
        
        // Load next scene
        LoadNextScene();
    }

    IEnumerator ShowImage(Image image)
    {
        // Fade in
        yield return StartCoroutine(FadeImage(image, 0f, 1f, fadeDuration));
        
        // Display
        yield return new WaitForSeconds(displayDuration);
        
        // Fade out
        yield return StartCoroutine(FadeImage(image, 1f, 0f, fadeDuration));
    }

    IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            SetImageAlpha(image, alpha);
            yield return null;
        }

        SetImageAlpha(image, endAlpha);
    }

    void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }
}