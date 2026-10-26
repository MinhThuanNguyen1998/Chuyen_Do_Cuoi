using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectPanel : MonoBehaviour
{
    [SerializeField] private ChapterItemUI[] m_ChapterItems;

    private void OnEnable() => RefreshAllChapters();
    public void RefreshAllChapters()
    {
        if (m_ChapterItems == null) return;

        foreach (var m_Item in m_ChapterItems)
        {
            if (m_Item != null)
            {
                m_Item.SetupChapterUI();
            }
        }
    }
}
