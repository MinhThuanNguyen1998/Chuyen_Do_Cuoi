using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TutorialDialogueTrigger
{
    Auto = 0,
    Never = 1,
    Immediately = 2,
    OnObjectClicked = 3,
}
[Serializable]
public class TutorialStepData
{
    public string stepName;
    public Transform targetTransform;
    public InteractiveObject2D interactiveObject;
    public TutorialDialogueTrigger dialogueTrigger = TutorialDialogueTrigger.Auto;

    public List<GameObject> panels = new List<GameObject>();
    public UnityEvent onStepCompleted;

    public TutorialDialogueTrigger ResolvedDialogueTrigger
    {
        get
        {
            if (dialogueTrigger != TutorialDialogueTrigger.Auto) return dialogueTrigger;
            return interactiveObject != null
                ? TutorialDialogueTrigger.OnObjectClicked
                : TutorialDialogueTrigger.Never;
        }
    }
}