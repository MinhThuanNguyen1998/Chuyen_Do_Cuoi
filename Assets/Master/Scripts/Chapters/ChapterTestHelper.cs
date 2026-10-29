using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTestHelper : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Test Settings")]
    [SerializeField] private bool m_EnableKeyboardShortcut = true;
    [SerializeField] private ChapterSelectPanel m_ChapterSelectPanel;

    private void Update()
    {
        if (!m_EnableKeyboardShortcut) return;

        if (GetKeyDown(KeyCode.Alpha1))
        {
            SetUnlockedChapter(1);
        }
        else if (GetKeyDown(KeyCode.Alpha2))
        {
            SetUnlockedChapter(2);
        }
        else if (GetKeyDown(KeyCode.Alpha3))
        {
            SetUnlockedChapter(3);
        }
        else if (GetKeyDown(KeyCode.R))
        {
            ResetAllProgress();
        }
    }

    private bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public void SetUnlockedChapter(int chapterIndex)
    {
      
        int targetChapter = chapterIndex + 1;
        SaveGameService.Data.highestUnlockedChapter = targetChapter;
        SaveGameService.Save();

        Debug.Log($"<color=green>[Test Helper] Done Chapter {chapterIndex} (Unlock to Chapter {targetChapter})</color>");

        if (m_ChapterSelectPanel != null)
        {
            m_ChapterSelectPanel.RefreshAllChapters();
        }
    }

    public void ResetAllProgress()
    {
        ChapterManager.ResetProgress(); 
        if (m_ChapterSelectPanel != null)
        {
            m_ChapterSelectPanel.RefreshAllChapters();
        }
    }
#endif

}
