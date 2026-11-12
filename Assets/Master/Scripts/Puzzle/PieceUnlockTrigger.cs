using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceUnlockTrigger : MonoBehaviour
{
    public static Action m_OnTriggerClickedEvent;

    private void OnEnable()
    {
        m_OnTriggerClickedEvent?.Invoke();
    }
}
