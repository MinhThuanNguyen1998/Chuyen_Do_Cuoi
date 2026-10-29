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

    private void OnEnable()
    {
        PlayFloatingEffect();
    }

    private void OnDisable()
    {
        m_Tween?.Kill();
        transform.localPosition = Vector3.zero;
    }

    private void PlayFloatingEffect()
    {
       
        m_Tween?.Kill();

        Vector3 startPos = transform.localPosition;

        m_Tween = transform.DOLocalMoveY(startPos.y + m_MoveDistance, m_Duration)
            .SetLoops(-1, LoopType.Yoyo) 
            .SetEase(Ease.InOutSine); 
    }

}
