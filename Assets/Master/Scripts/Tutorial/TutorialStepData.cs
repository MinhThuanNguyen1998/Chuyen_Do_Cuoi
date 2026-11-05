using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[Serializable]
public class TutorialStepData
{
    public string stepName;
    public Transform targetTransform;
    public InteractiveObject2D interactiveObject;
    public List<GameObject> panels = new List<GameObject>();
    public UnityEvent onStepCompleted;
}