using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private const float END_MARGIN = 0.05f;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_BgmAudioSource;
    [SerializeField] private AudioSource m_VoiceAudioSource;
    [SerializeField] private AudioSource m_SfxAudioSource;

    private bool m_IsVoicePaused;
    protected override void Awake()
    {
        IsPersistent = true;
        base.Awake();
    }

    protected override void OnSingletonAwake()
    {
        // Áp âm lượng đã lưu ngay khi game khởi động (không cần mở Setting mới có tác dụng)
        ApplySavedVolumes();
    }

    public void ApplySavedVolumes()
    {
        SetBgmVolume(AudioVolumeSettings.Bgm);
        SetSfxVolume(AudioVolumeSettings.Sfx);
        SetVoiceVolume(AudioVolumeSettings.Voice);
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip == null || m_VoiceAudioSource == null) return;

        if (m_IsVoicePaused)
        {
            m_VoiceAudioSource.UnPause();
        }
        else
        {
            m_VoiceAudioSource.clip = clip;
            m_VoiceAudioSource.Play();
        }

        m_IsVoicePaused = false;
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null || m_SfxAudioSource == null) return;
        m_SfxAudioSource.PlayOneShot(clip);
    }

    public void PlayBgm(AudioClip clip, bool loop = true)
    {
        if (clip == null || m_BgmAudioSource == null) return;
        m_BgmAudioSource.clip = clip;
        m_BgmAudioSource.loop = loop;
        m_BgmAudioSource.Play();
    }

    public void PauseVoice()
    {
        if (m_VoiceAudioSource == null || !m_VoiceAudioSource.isPlaying) return;

        m_VoiceAudioSource.Pause();
        m_IsVoicePaused = true;
    }
    public void StopVoice()
    {
        if (m_VoiceAudioSource == null) return;

        m_VoiceAudioSource.Stop();
        m_IsVoicePaused = false;
    }

    public void SeekVoice(float timeStep)
    {
        if (m_VoiceAudioSource == null || m_VoiceAudioSource.clip == null) return;
        if (!m_VoiceAudioSource.isPlaying && !m_IsVoicePaused) return;

        float targetTime = m_VoiceAudioSource.time + timeStep;
        float maxTime = m_VoiceAudioSource.clip.length - END_MARGIN;

        if (targetTime >= maxTime)
        {
            StopVoice();
            return;
        }

        m_VoiceAudioSource.time = Mathf.Max(0f, targetTime);
    }

    public void SetBgmVolume(float volume)
    {
        if (m_BgmAudioSource != null)
        {
            m_BgmAudioSource.volume = volume;
        }
    }

    public void SetSfxVolume(float volume)
    {
        if (m_SfxAudioSource != null)
        {
            m_SfxAudioSource.volume = volume;
        }
    }
    public void SetVoiceVolume(float volume)
    {
        if (m_VoiceAudioSource != null)
        {
            m_VoiceAudioSource.volume = volume;
        }
    }
}
