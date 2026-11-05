using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDialogueSO", menuName = "Tutorial/Tutorial Dialogue SO")]
public class TutorialDialogueSO : ScriptableObject
{
    [SerializeField] private Sprite m_AvatarSprite;
    [SerializeField, TextArea(3, 5)] private string m_TextContent;
    [SerializeField] private AudioClip m_VoiceAudio;

    public Sprite AvatarSprite => m_AvatarSprite;
    public string TextContent => m_TextContent;
    public AudioClip VoiceAudio => m_VoiceAudio;
}
