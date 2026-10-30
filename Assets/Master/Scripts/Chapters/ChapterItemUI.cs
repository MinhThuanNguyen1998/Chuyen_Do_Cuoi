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
        bool isUnlocked = ChapterManager.IsUnlocked(m_ChapterIndex);

        if (m_LockIcon != null)
        {
            m_LockIcon.SetActive(!isUnlocked);
        }
        //if (m_ChapterButton != null)
        //{
        //    m_ChapterButton.interactable = isUnlocked;
        //}
        if (m_HighlightBorder != null)
        {
            m_HighlightBorder.SetActive(m_ChapterIndex == ChapterManager.GetSelectedChapter());
        }
    }

    public void OnChapterButtonClicked()
    {
        if (!ChapterManager.SetSelectedChapter(m_ChapterIndex))
        {
            Debug.Log($"Chapter {m_ChapterIndex} is locked!");
            return;
        }

        OnChapterSelected?.Invoke(m_ChapterIndex); // Invoke the event to notify other listeners about the chapter selection
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
