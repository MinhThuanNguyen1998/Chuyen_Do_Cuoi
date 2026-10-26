using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTestHelper : MonoBehaviour
{
    [Header("Test Settings")]
    [Tooltip("Nhấn phím số để set nhanh chapter đã mở khóa cao nhất")]
    [SerializeField] private bool m_EnableKeyboardShortcut = true;

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
        PlayerPrefs.SetInt("HighestUnlockedChapter", targetChapter);
        PlayerPrefs.Save();

        Debug.Log($"<color=green>[Test Helper] Đã set trạng thái: Đã qua Chapter {chapterIndex} (Mở khóa đến Chapter {targetChapter})</color>");

        ChapterSelectPanel panel = FindObjectOfType<ChapterSelectPanel>();
        if (panel != null)
        {
            panel.RefreshAllChapters();
        }
    }

    public void ResetAllProgress()
    {
        ChapterManager.ResetProgress();

        ChapterSelectPanel panel = FindObjectOfType<ChapterSelectPanel>();
        if (panel != null)
        {
            panel.RefreshAllChapters();
        }
    }
}
