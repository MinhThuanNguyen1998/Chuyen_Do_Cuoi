using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleJigsaw : MonoBehaviour
{
    [Header("Puzzle Configuration")]
    [SerializeField] private PuzzlePieceController[] m_AllPieces; // Array of all puzzle pieces in the game

    private readonly List<PuzzlePieceController> m_SubscribedPieces = new List<PuzzlePieceController>();
    private int m_TotalPieces;
    private int m_PlacedCount;
    private bool m_IsCompleted;

    public static event Action OnPuzzleCompleted;

    private void Start()
    {
        if (m_AllPieces == null || m_AllPieces.Length == 0)
        {
            Debug.LogWarning($"{name}: m_AllPieces is empty, the puzzle can never be completed.", this);
            return;
        }

        // Only count valid, unique pieces (ignore null / duplicated entries)
        var unique = new HashSet<PuzzlePieceController>();
        foreach (var piece in m_AllPieces)
        {
            if (piece == null || !unique.Add(piece)) continue;

            piece.OnPiecePlaced += HandlePiecePlaced;
            m_SubscribedPieces.Add(piece);
        }

        m_TotalPieces = m_SubscribedPieces.Count;

        if (m_TotalPieces != m_AllPieces.Length)
        {
            Debug.LogWarning($"{name}: m_AllPieces contains null or duplicate entries. " +
                             $"Only {m_TotalPieces}/{m_AllPieces.Length} valid pieces are counted.", this);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        foreach (var piece in m_SubscribedPieces)
        {
            if (piece != null)
            {
                piece.OnPiecePlaced -= HandlePiecePlaced;
            }
        }
        m_SubscribedPieces.Clear();
    }

    // Handles the logic when a piece is successfully placed
    private void HandlePiecePlaced()
    {
        if (m_IsCompleted) return;

        m_PlacedCount++;
        Debug.Log($"Correctly placed: {m_PlacedCount}/{m_TotalPieces} pieces.");

        if (m_PlacedCount >= m_TotalPieces)
        {
            Debug.Log("All puzzle pieces have been successfully placed!");
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        m_IsCompleted = true;
        OnPuzzleCompleted?.Invoke();
    }
}
