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
    [SerializeField] private CanvasGroup m_FadeCanvasGroup; // must be a child of the SceneLoader object

    private bool m_IsLoading;

    // IMPORTANT: Singleton.Awake() checks IsPersistent BEFORE calling OnSingletonAwake(),
    // so IsPersistent must be set here, before base.Awake(), for DontDestroyOnLoad to be applied.
    protected override void Awake()
    {
        IsPersistent = true;
        base.Awake();
    }

    public void LoadSceneByName(string sceneName)
    {
        if (m_IsLoading) return;
        StartCoroutine(CoroutineTransitionAndLoad(sceneName));
    }

    public void LoadSceneMainMenu()
    {
        LoadSceneByName(SceneName.MainMenu);
    }

    private IEnumerator CoroutineTransitionAndLoad(string sceneName)
    {
        m_IsLoading = true;

        // 1. Fade the screen to black
        if (m_FadeCanvasGroup != null)
        {
            m_FadeCanvasGroup.blocksRaycasts = true;
            yield return m_FadeCanvasGroup.DOFade(1f, m_FadeDuration).WaitForCompletion();
        }

        yield return new WaitForSeconds(0.4f);

        // 2. Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        if (asyncLoad == null)
        {
            Debug.LogError($"[SceneLoader] Could not load scene '{sceneName}'. " +
                           "Check the scene name and Build Settings.");
            if (m_FadeCanvasGroup != null)
            {
                m_FadeCanvasGroup.alpha = 0f;
                m_FadeCanvasGroup.blocksRaycasts = false;
            }
            m_IsLoading = false;
            yield break;
        }

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 3. Fade back in
        if (m_FadeCanvasGroup != null)
        {
            yield return m_FadeCanvasGroup.DOFade(0f, m_FadeDuration).WaitForCompletion();
            m_FadeCanvasGroup.blocksRaycasts = false; // do not block clicks on buttons in the new scene
        }

        m_IsLoading = false;
    }
}
