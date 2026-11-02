using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_VoiceAudioSource; 
    [SerializeField] private AudioSource m_SfxAudioSource;  
    public void PlayVoice(AudioClip clip)
    {
        if (clip == null) return;

        if (m_VoiceAudioSource != null)
        {
            m_VoiceAudioSource.Stop();
            m_VoiceAudioSource.clip = clip;
            m_VoiceAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioManager: Chưa gán m_VoiceAudioSource trên Inspector!");
        }
    }
    public void StopVoice()
    {
        if (m_VoiceAudioSource != null && m_VoiceAudioSource.isPlaying)
        {
            m_VoiceAudioSource.Stop();
        }
    }
}
