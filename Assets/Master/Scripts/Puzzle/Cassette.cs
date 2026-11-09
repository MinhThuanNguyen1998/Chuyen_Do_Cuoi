using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassette : MonoBehaviour
{
    [SerializeField] private AudioClip m_CassetteClip;
    [SerializeField] private float m_SeekStep = 5f;

    [Header("Cooldown Settings")]
    [SerializeField] private float m_SeekCooldown = 0.15f; // Thời gian chờ giữa 2 lần bấm
    private float m_NextSeekTime = 0f;

    public void OnButtonPlayClick()
    {
        AudioManager.Instance.PlayVoice(m_CassetteClip);
    }

    public void OnButtonPauseClick()
    {
        AudioManager.Instance.PauseVoice();
    }

    public void OnButtonStopClick()
    {
        AudioManager.Instance.StopVoice();
    }

    public void OnButtonNextClick()
    {
        TrySeek(m_SeekStep);
    }

    public void OnButtonPrevClick()
    {
        TrySeek(-m_SeekStep);
    }

    private void TrySeek(float step)
    {
        if (Time.unscaledTime < m_NextSeekTime) return;
        m_NextSeekTime = Time.unscaledTime + m_SeekCooldown;

        AudioManager.Instance.SeekVoice(step);
    }
}
