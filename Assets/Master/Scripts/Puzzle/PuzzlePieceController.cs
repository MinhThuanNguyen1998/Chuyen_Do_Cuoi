using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePieceController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action OnPiecePlaced;

    private RectTransform m_RectTransform;
    private Canvas m_Canvas;
    private CanvasGroup m_CanvasGroup;
    private RectTransform m_ParentRect;

    [Header("Target Configuration")]
    [SerializeField] private RectTransform m_TargetSlot;     // The correct slot position to snap to
    [SerializeField] private float m_SnapThreshold = 60f;    // Maximum distance allowed for snapping
    private bool m_IsPlaced = false;                         // Flag to check if the piece is already placed

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Canvas = GetComponentInParent<Canvas>();
        m_CanvasGroup = GetComponent<CanvasGroup>();

        if (m_CanvasGroup == null)
        {
            m_CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(InitializeRectReferences());
    }

    private IEnumerator InitializeRectReferences()
    {
        yield return null;
        m_ParentRect = m_RectTransform.parent as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_IsPlaced) return;
        m_CanvasGroup.blocksRaycasts = false; // Allow raycasts to pass through while dragging
        transform.SetAsLastSibling();         // Bring the piece to the front of rendering
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_IsPlaced) return;

        // Update position based on mouse/touch movement
        m_RectTransform.anchoredPosition += eventData.delta / m_Canvas.scaleFactor;

        // Clamp position to stay strictly within the parent panel bounds
        ClampPositionToParent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_IsPlaced) return;
        m_CanvasGroup.blocksRaycasts = true;

        // Calculate distance between the piece and its target slot
        float distance = Vector2.Distance(m_RectTransform.anchoredPosition, m_TargetSlot.anchoredPosition);

        // If close enough, snap to the target slot
        if (distance <= m_SnapThreshold)
        {
            m_RectTransform.anchoredPosition = m_TargetSlot.anchoredPosition;
            m_IsPlaced = true;
            m_CanvasGroup.interactable = false; // Disable further interaction

            // Notify the manager that this piece is correctly placed
            OnPiecePlaced?.Invoke();
        }
    }

    // Helper method to keep the piece inside the parent panel boundaries
    private void ClampPositionToParent()
    {
        if (m_ParentRect == null) return;

        Vector2 parentSize = m_ParentRect.rect.size;
        Vector2 pieceSize = m_RectTransform.rect.size;
        Vector2 pivot = m_RectTransform.pivot;

        // Calculate min and max bounds based on pivot and sizes
        float minX = (pieceSize.x * pivot.x) - (parentSize.x * 0.5f);
        float maxX = parentSize.x - (pieceSize.x * (1f - pivot.x)) - (parentSize.x * 0.5f);
        float minY = (pieceSize.y * pivot.y) - (parentSize.y * 0.5f);
        float maxY = parentSize.y - (pieceSize.y * (1f - pivot.y)) - (parentSize.y * 0.5f);

        // Apply clamping to anchored position
        Vector2 clampedPosition = m_RectTransform.anchoredPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        m_RectTransform.anchoredPosition = clampedPosition;
    }
}
