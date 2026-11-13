using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollection : MonoBehaviour
{
    [Header("Pieces Configuration")]
    [SerializeField] private List<PickupPiece> m_Pieces;

    [Header("UI")]
    [SerializeField] private GameObject m_PanelPuzzleJigsaw;

    private int m_CollectedCount = 0;

    private void Start()
    {
        if (m_PanelPuzzleJigsaw != null)
        {
            m_PanelPuzzleJigsaw.SetActive(false);
        }
        PieceUnlockTrigger.m_OnTriggerClickedEvent += HandleUnlockTriggered;
        PickupPiece.m_OnPieceCollectedEvent += HandlePieceCollected;
        PuzzleJigsaw.OnPuzzleCompleted += HandlePuzzleCompleted;
    }

    private void OnDestroy()
    {
        PieceUnlockTrigger.m_OnTriggerClickedEvent -= HandleUnlockTriggered;
        PickupPiece.m_OnPieceCollectedEvent -= HandlePieceCollected;
        PuzzleJigsaw.OnPuzzleCompleted -= HandlePuzzleCompleted;
    }

    private void HandleUnlockTriggered()
    {
        foreach (var piece in m_Pieces)
        {
            if (piece != null)
            {
                piece.SetInteractable(true);
            }
        }
        Debug.Log("All puzzle pieces unlocked!");
    }
    private void HandlePuzzleCompleted()
    {
        if (m_PanelPuzzleJigsaw != null)
        {
            m_PanelPuzzleJigsaw.SetActive(false);
        }
    }
    private void HandlePieceCollected(PickupPiece piece)
    {
        if (m_Pieces.Contains(piece))
        {
            m_CollectedCount++;

            if (m_CollectedCount >= m_Pieces.Count)
            {
                if (m_PanelPuzzleJigsaw != null)
                {
                    m_PanelPuzzleJigsaw.SetActive(true);
                    Debug.Log("Panel_Puzzle activated!");
                }
            }
        }
    } 
}
