using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button m_Button;

    private void Awake()
    {
        if (m_Button == null) m_Button = GetComponent<Button>();

        if (m_Button != null)
        {
            m_Button.onClick.AddListener(OnBackButtonClicked);
        }
    }

    private void OnDestroy()
    {
        if (m_Button != null)
        {
            m_Button.onClick.RemoveListener(OnBackButtonClicked);
        }
    }

    private void OnBackButtonClicked()
    {
        if (SceneLoader.HasInstance)
        {
            Debug.Log("[BackButton] SceneLoader instance found. Using it to load MainMenu scene.");
            SceneLoader.Instance.LoadSceneMainMenu();

        }
        else
        {
            Debug.LogWarning("[BackButton] SceneLoader instance not found. Loading MainMenu scene directly.");
            SceneManager.LoadScene(SceneName.MainMenu);
        }
    }
}
