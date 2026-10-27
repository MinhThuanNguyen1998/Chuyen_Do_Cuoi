using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    private const string UNLOCKED_CHAPTER_KEY = "HighestUnlockedChapter";
    private const string SELECTED_CHAPTER_KEY = "SelectedChapterIndex";

    public static int GetUnlockedChapter()
    {
        return PlayerPrefs.GetInt(UNLOCKED_CHAPTER_KEY, 1); // Default to chapter 1 if not set
    }
    public static void CompleteChapter(int m_CompletedChapterIndex)
    {
        int m_CurrentHighest = GetUnlockedChapter();
        int m_NextChapter = m_CompletedChapterIndex + 1;

        if (m_NextChapter > m_CurrentHighest)
        {
            PlayerPrefs.SetInt(UNLOCKED_CHAPTER_KEY, m_NextChapter);
            PlayerPrefs.Save();
        }
    }
    public static int GetSelectedChapter()
    {
        return PlayerPrefs.GetInt(SELECTED_CHAPTER_KEY, 1);
    }

    public static void SetSelectedChapter(int chapterIndex)
    {
        PlayerPrefs.SetInt(SELECTED_CHAPTER_KEY, chapterIndex);
        PlayerPrefs.Save();
    }
    public static void ResetProgress()
    {
        Debug.Log("Resetting all chapter progress...");
        PlayerPrefs.DeleteKey(UNLOCKED_CHAPTER_KEY);
        PlayerPrefs.DeleteKey(SELECTED_CHAPTER_KEY);
        PlayerPrefs.Save();
    }
}
