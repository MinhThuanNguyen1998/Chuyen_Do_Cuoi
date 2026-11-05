using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TutorialDialogueLine
{
    [SerializeField] private Sprite m_AvatarSprite;
    [SerializeField, TextArea(3, 5)] private string m_TextContent;
    [SerializeField] private AudioClip m_VoiceAudio;

    public Sprite AvatarSprite => m_AvatarSprite;
    public string TextContent => m_TextContent;
    public AudioClip VoiceAudio => m_VoiceAudio;
}

[CreateAssetMenu(fileName = "TutorialDialogueSO", menuName = "Tutorial/Tutorial Dialogue SO")]
public class TutorialDialogueSO : ScriptableObject
{
    [SerializeField] private List<TutorialDialogueLine> m_Lines = new List<TutorialDialogueLine>();

    public int Count => m_Lines != null ? m_Lines.Count : 0;

    public TutorialDialogueLine GetLine(int index)
    {
        if (m_Lines == null || index < 0 || index >= m_Lines.Count) return null;
        return m_Lines[index];
    }
}