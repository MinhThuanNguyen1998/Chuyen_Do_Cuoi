using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterItemUI : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int m_ChapterIndex;

    [Header("UI References")]
    [SerializeField] private GameObject m_LockIcon;
    [SerializeField] private GameObject m_HighlightBorder;
    [SerializeField] private Button m_ChapterButton;
    public static event Action<int> OnChapterSelected;
    
    public int ChapterIndex => m_ChapterIndex;
    private void OnEnable()
    {
        OnChapterSelected += HandleChapterSelected;
    }

    private void OnDisable()
    {
        OnChapterSelected -= HandleChapterSelected;
    }
    public void SetupChapterUI()
    {
        int m_UnlockedLevel = ChapterManager.GetUnlockedChapter();
        bool m_IsUnlocked = (m_ChapterIndex <= m_UnlockedLevel);

        if (m_LockIcon != null)
        {
            m_LockIcon.SetActive(!m_IsUnlocked);
        }
        //if (m_ChapterButton != null)
        //{
        //    m_ChapterButton.interactable = m_IsUnlocked;
        //}
        if (m_HighlightBorder != null)
        {
            int m_SavedSelectedChapter = ChapterManager.GetSelectedChapter();
            m_HighlightBorder.SetActive(m_ChapterIndex == m_SavedSelectedChapter);
        }
    }

    public void OnChapterButtonClicked()
    {
        int m_UnlockedLevel = ChapterManager.GetUnlockedChapter();

        if (m_ChapterIndex > m_UnlockedLevel)
        {
            Debug.Log($"Chapter {m_ChapterIndex} is locked!");
            return;
        }
        ChapterManager.SetSelectedChapter(m_ChapterIndex);
        OnChapterSelected?.Invoke(m_ChapterIndex);

        Debug.Log($"Successfully selected chapter {m_ChapterIndex}");
    }

    private void HandleChapterSelected(int selectedIndex)
    {
        if (m_HighlightBorder != null)
        {
            m_HighlightBorder.SetActive(selectedIndex == m_ChapterIndex);
        }
    }


}
