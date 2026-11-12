using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickupPiece : MonoBehaviour, IPointerClickHandler
{
    public static Action<PickupPiece> m_OnPieceCollectedEvent;

    private Image m_Image;
    private bool m_IsCollected = false;
    private bool m_IsInteractable = false;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        SetInteractable(false);
    }

    public void SetInteractable(bool interactable)
    {
        m_IsInteractable = interactable;
        if (m_Image != null)
        {
            m_Image.raycastTarget = interactable;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!m_IsCollected && m_IsInteractable)
        {
            m_IsCollected = true;
            m_OnPieceCollectedEvent?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
