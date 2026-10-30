using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public const int FirstChapter = 1;
    public const int MaxChapter = 4;
    public static int GetUnlockedChapter()
    {
        return Mathf.Clamp(SaveGameService.Data.highestUnlockedChapter, FirstChapter, MaxChapter);
    }
    public static bool IsUnlocked(int chapterIndex)
    {
        return chapterIndex >= FirstChapter && chapterIndex <= GetUnlockedChapter();
    }

    public static bool CompleteChapter(int completedChapterIndex)
    {
        if (completedChapterIndex < FirstChapter || completedChapterIndex > MaxChapter)
        {
            Debug.LogWarning($"CompleteChapter: chapter {completedChapterIndex} không hợp lệ.");
            return false;
        }

        int nextChapter = Mathf.Min(completedChapterIndex + 1, MaxChapter);
        if (nextChapter <= GetUnlockedChapter()) return false;

        SaveGameService.Data.highestUnlockedChapter = nextChapter;
        SaveGameService.Save();
        return true;
    }

    public static int GetSelectedChapter()
    {
        return Mathf.Clamp(SaveGameService.Data.selectedChapterIndex, FirstChapter, GetUnlockedChapter());
    }

    public static bool SetSelectedChapter(int chapterIndex)
    {
        if (!IsUnlocked(chapterIndex)) return false;

        SaveGameService.Data.selectedChapterIndex = chapterIndex;
        SaveGameService.Save();
        return true;
    }
    public static void ResetChapterProgress()
    {
        Debug.Log("Resetting chapter progress...");
        SaveGameService.Data.highestUnlockedChapter = FirstChapter;
        SaveGameService.Data.selectedChapterIndex = FirstChapter;
        SaveGameService.Save();
    }
    public static void ResetProgress()
    {
        Debug.Log("Resetting all chapter progress...");
        SaveGameService.ClearData();
    }
}
