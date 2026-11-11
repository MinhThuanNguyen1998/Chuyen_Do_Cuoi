using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvelopeEffectPanel : MonoBehaviour
{
    [Header("UI GameObjects (Envelope States)")]
    [SerializeField] private GameObject m_Obj1Closed;     // Close_Envelope GameObject
    [SerializeField] private GameObject m_Obj2OpenFlap;   // Open_Envelope GameObject

    [Header("Delay Settings (Seconds)")]
    [SerializeField] private float m_DelayStep1To2 = 0.3f; // Delay before opening the flap

    private Tween m_DelayTween;

    private void OnEnable()
    {
        // Step 1: Initialize initial states (Show closed envelope, hide open envelope)
        if (m_Obj1Closed != null) m_Obj1Closed.SetActive(true);
        if (m_Obj2OpenFlap != null) m_Obj2OpenFlap.SetActive(false);

        // Ensure any previous running tween is killed to prevent logic conflicts
        m_DelayTween?.Kill();

        // Step 2: Switch to open state after the specified delay
        m_DelayTween = DOVirtual.DelayedCall(m_DelayStep1To2, () =>
        {
            if (m_Obj1Closed != null) m_Obj1Closed.SetActive(false);
            if (m_Obj2OpenFlap != null) m_Obj2OpenFlap.SetActive(true);
        });
    }

    private void OnDisable()
    {
        // Kill active tween when the panel is disabled to release resources and avoid leaks
        m_DelayTween?.Kill();
    }
}
