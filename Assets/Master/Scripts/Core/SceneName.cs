using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneName
{
    public const string MainMenu = "MainMenu";

    private static readonly string[] Chapters =
    {
        "Chapter1",
        "Chapter2",
        "Chapter3",
        "Chapter4",
    };

    public static int ChapterCount => Chapters.Length;

    public static string GetChapterScene(int chapterIndex)
    {
        int i = chapterIndex - 1;
        if (i < 0 || i >= Chapters.Length) return null;
        return Chapters[i];
    }
}
   
