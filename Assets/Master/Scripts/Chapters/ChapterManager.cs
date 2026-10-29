using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static int GetUnlockedChapter()
    {
        return SaveGameService.Data.highestUnlockedChapter;
    }

    public static void CompleteChapter(int m_CompletedChapterIndex)
    {
        int m_CurrentHighest = GetUnlockedChapter();
        int m_NextChapter = m_CompletedChapterIndex + 1;

        if (m_NextChapter > m_CurrentHighest)
        {
            SaveGameService.Data.highestUnlockedChapter = m_NextChapter;
            SaveGameService.Save();
        }
    }

    public static int GetSelectedChapter()
    {
        return SaveGameService.Data.selectedChapterIndex;
    }

    public static void SetSelectedChapter(int chapterIndex)
    {
        SaveGameService.Data.selectedChapterIndex = chapterIndex;
        SaveGameService.Save();
    }

    public static void ResetProgress()
    {
        Debug.Log("Resetting all chapter progress...");
        SaveGameService.ClearData();
    }
}
