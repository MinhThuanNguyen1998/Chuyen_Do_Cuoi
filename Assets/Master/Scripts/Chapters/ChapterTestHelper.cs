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

        if (GetKeyDown(KeyCode.Alpha1)) CompleteChapterForTest(1);
        else if (GetKeyDown(KeyCode.Alpha2)) CompleteChapterForTest(2);
        else if (GetKeyDown(KeyCode.Alpha3)) CompleteChapterForTest(3);
        else if (GetKeyDown(KeyCode.R)) ResetChapterProgress();
        else if (GetKeyDown(KeyCode.Delete)) ResetAllProgress();
    }

    private bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }


    public void CompleteChapterForTest(int chapterIndex)
    {
        bool changed = ChapterManager.CompleteChapter(chapterIndex);
        Debug.Log($"<color=green>[Test Helper] Complete Chapter {chapterIndex} " +
                  $"(changed: {changed}, unlocked: {ChapterManager.GetUnlockedChapter()})</color>");
        Refresh();
    }
    public void ResetChapterProgress()
    {
        ChapterManager.ResetChapterProgress();
        Refresh();
    }
    public void ResetTutorialProgress()
    {
        SaveGameService.ResetTutorialProgress();
        Debug.Log("<color=green>[Test Helper] Tutorial progress reset</color>");
    }

    public void ResetAllProgress()
    {
        ChapterManager.ResetProgress();
        Refresh();
    }
    private void Refresh()
    {
        if (m_ChapterSelectPanel != null)
        {
            m_ChapterSelectPanel.RefreshAllChapters();
        }
    }
#endif

}
