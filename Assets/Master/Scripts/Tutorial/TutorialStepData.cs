using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TutorialDialogueTrigger
{
    OnObjectClicked = 0,
    Never = 1,
    Immediately = 2,
}
[Serializable]
public class TutorialStepData
{
    public string stepName;
    public Transform targetTransform;
    public InteractiveObject2D interactiveObject;
    public TutorialDialogueTrigger dialogueTrigger = TutorialDialogueTrigger.OnObjectClicked;

    public List<GameObject> panels = new List<GameObject>();
    public UnityEvent onStepCompleted;

}