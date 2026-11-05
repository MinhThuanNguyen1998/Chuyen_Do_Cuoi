using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    [Header("Puzzle Configuration")]
    [SerializeField] private PuzzlePieceController[] m_AllPieces; // Array of all puzzle pieces in the game

    private int m_TotalPieces;
    private int m_PlacedCount = 0;

    private void Start()
    {
        if (m_AllPieces != null)
        {
            m_TotalPieces = m_AllPieces.Length;

            // Subscribe to the placement event of each puzzle piece
            foreach (var piece in m_AllPieces)
            {
                if (piece != null)
                {
                    piece.OnPiecePlaced += HandlePiecePlaced;
                }
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (m_AllPieces != null)
        {
            foreach (var piece in m_AllPieces)
            {
                if (piece != null)
                {
                    piece.OnPiecePlaced -= HandlePiecePlaced;
                }
            }
        }
    }

    // Handles the logic when a piece is successfully placed
    private void HandlePiecePlaced()
    {
        m_PlacedCount++;
        Debug.Log($"Correctly placed: {m_PlacedCount}/{m_TotalPieces} pieces.");

        if (m_PlacedCount >= m_TotalPieces)
        {
            Debug.Log("All puzzle pieces have been successfully placed!");
            TriggerWin();
        }
    }

    // Triggers win conditions, effects, or UI
    private void TriggerWin()
    {
        // Implement your win screen or effects here
    }
}
