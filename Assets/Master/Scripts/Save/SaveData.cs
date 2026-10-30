using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SceneStepEntry
{
    public string sceneName;
    public int step;
}
[Serializable]
public class SaveData
{
    // Progression Chapter Data
    public int highestUnlockedChapter = 1;
    public int selectedChapterIndex = 1;


    public List<SceneStepEntry> sceneSteps = new List<SceneStepEntry>();
    public int GetStepForScene(string sceneName)
    {
        var entry = FindEntry(sceneName);
        return entry != null ? entry.step : 0;
    }
    public bool SetStepForScene(string sceneName, int stepIndex)
    {
        var entry = FindEntry(sceneName);
        if (entry == null)
        {
            sceneSteps.Add(new SceneStepEntry { sceneName = sceneName, step = stepIndex });
            return true;
        }

        if (entry.step == stepIndex) return false;
        entry.step = stepIndex;
        return true;
    }
    public void ClearTutorialProgress()
    {
        sceneSteps.Clear();
    }
    private SceneStepEntry FindEntry(string sceneName)
    {
        for (int i = 0; i < sceneSteps.Count; i++)
        {
            if (sceneSteps[i] != null && sceneSteps[i].sceneName == sceneName)
                return sceneSteps[i];
        }
        return null;
    }
}
