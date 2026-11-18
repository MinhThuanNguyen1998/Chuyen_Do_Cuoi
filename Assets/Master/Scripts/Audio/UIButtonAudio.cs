using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAudio : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip m_ClickSound;

    private Button m_Button;
    private void Awake()
    {
        m_Button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (m_Button != null)
        {
            m_Button.onClick.AddListener(PlayClickSound);
        }
    }

    private void OnDisable()
    {
        if (m_Button != null)
        {
            m_Button.onClick.RemoveListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        //Debug.Log("PlayClickSound called");
        if (m_ClickSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySfx(m_ClickSound);
        }
    }
}
