using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class SceneLoader : Singleton<SceneLoader>
{
    [Header("Config")]
    [SerializeField] private float m_FadeDuration = 1.0f;

    [Header("UI References")]
    [SerializeField] private CanvasGroup m_FadeCanvasGroup;
    protected override void OnSingletonAwake()
    {
        // Ensure that the SceneLoader persists across scene loads
        IsPersistent = true;
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(CoroutineTransitionAndLoad(sceneName));
    }

    private IEnumerator CoroutineTransitionAndLoad(string sceneName)
    {
        // 1. Use DOTween to fade the screen to black before loading the new scene
        if (m_FadeCanvasGroup != null)
        {
            yield return m_FadeCanvasGroup.DOFade(1f, m_FadeDuration).WaitForCompletion();
        }

        // 2. Stop all audio sources in the current scene to prevent audio from overlapping
        yield return new WaitForSeconds(0.4f);

        // 3. Load the new scene asynchronously to avoid freezing the game during the load
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 4. Use DOTween to fade the screen back in after the new scene has loaded
        if (m_FadeCanvasGroup != null)
        {
            yield return m_FadeCanvasGroup.DOFade(0f, m_FadeDuration).WaitForCompletion();
        }
    }
}
