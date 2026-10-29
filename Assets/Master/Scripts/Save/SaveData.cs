using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Progression Chapter Data
    public int highestUnlockedChapter = 1;
    public int selectedChapterIndex = 1;

    //Progression Scene Data
    public List<string> sceneKeys = new List<string>();
    public List<int> sceneStepValues = new List<int>();

    public int GetStepForScene(string sceneName)
    {
        int index = sceneKeys.IndexOf(sceneName);
        return index != -1 ? sceneStepValues[index] : 0;
    }
    public void SetStepForScene(string sceneName, int stepIndex)
    {
        int index = sceneKeys.IndexOf(sceneName);
        if (index != -1)
        {
            sceneStepValues[index] = stepIndex;
        }
        else
        {
            sceneKeys.Add(sceneName);
            sceneStepValues.Add(stepIndex);
        }
    }
}
