using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PopupType
{
    None,
    Setting,
    ChapterSelect,
    QuitConfirm
}
public class MainMenuManager : MonoBehaviour
{
    [Header("UI Popups")]
    [SerializeField] private GameObject m_SettingPopup;
    [SerializeField] private GameObject m_ChapterSelectPopup;
    [SerializeField] private GameObject m_QuitConfirmPopup;

    private PopupType m_CurrentPopup = PopupType.None;

    private void Start() => CloseAllPopups();

    public void OnClickOpenSetting() => OpenPopup(PopupType.Setting);
    public void OnClickOpenClickChapterSelect() => OpenPopup(PopupType.ChapterSelect);
    public void OnClickOpenQuitConfirm() => OpenPopup(PopupType.QuitConfirm);

    public void OpenPopup(PopupType type)
    {
        if (m_CurrentPopup == type) return;

        CloseAllPopups();

        switch (type)
        {
            case PopupType.Setting:
                if (m_SettingPopup) m_SettingPopup.SetActive(true);
                break;
            case PopupType.ChapterSelect:
                if (m_ChapterSelectPopup) m_ChapterSelectPopup.SetActive(true);
                break;
            case PopupType.QuitConfirm:
                if (m_QuitConfirmPopup) m_QuitConfirmPopup.SetActive(true);
                break;
        }

        m_CurrentPopup = type;
    }
    public void CloseAllPopups()
    {
        if (m_SettingPopup) m_SettingPopup.SetActive(false);
        if (m_ChapterSelectPopup) m_ChapterSelectPopup.SetActive(false);
        if (m_QuitConfirmPopup) m_QuitConfirmPopup.SetActive(false);

        m_CurrentPopup = PopupType.None;
    }
    public void ConfirmQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
