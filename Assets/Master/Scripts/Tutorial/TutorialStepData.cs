using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TutorialDialogueLine
{
    public Sprite avatarSprite;
    [TextArea(3, 5)] public string textContent;
    public AudioClip voiceAudio;
}

[Serializable]
public class TutorialStepData
{
    public string stepName;
    public Transform targetTransform;
    public InteractiveObject2D interactiveObject;
    public TutorialDialogueLine dialogue;
    public UnityEvent onStepCompleted;
}