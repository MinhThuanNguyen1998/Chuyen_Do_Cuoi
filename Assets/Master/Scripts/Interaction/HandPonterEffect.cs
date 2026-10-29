using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPonterEffect : MonoBehaviour
{
    [Header("Tween Settings")]
    [SerializeField] private float m_MoveDistance = 20f;
    [SerializeField] private float m_Duration = 0.5f;

    private Tweener m_Tween;
    private Vector3 m_InitialLocalPos;
    private bool m_IsInitialized;

    private void Awake()
    {
        m_InitialLocalPos = transform.localPosition;
        m_IsInitialized = true;
    }

    private void OnEnable()
    {
        if (!m_IsInitialized)
        {
            m_InitialLocalPos = transform.localPosition;
            m_IsInitialized = true;
        }

        PlayFloatingEffect();
    }

    private void OnDisable()
    {
        if (m_Tween != null && m_Tween.IsActive())
        {
            m_Tween.Kill();
        }
        transform.localPosition = m_InitialLocalPos;
    }

    private void PlayFloatingEffect()
    {
        transform.localPosition = m_InitialLocalPos;

        m_Tween?.Kill();

        m_Tween = transform.DOLocalMoveY(m_InitialLocalPos.y + m_MoveDistance, m_Duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }

}
