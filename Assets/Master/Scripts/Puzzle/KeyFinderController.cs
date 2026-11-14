using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyFinderController : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Panel displayed when the key is found.")]
    [SerializeField] private GameObject m_PanelKey;

    [Header("Key Image Result")]
    [SerializeField] private Button m_ImageKeyButton;


    [Header("Interactive Spots")]
    [Tooltip("List of buttons (containing Image + Button) corresponding to the spots.")]
    [SerializeField] private List<Button> m_SpotButtons = new List<Button>();

    [Header("Key Visuals (Ready in Scene)")]
    [Tooltip("List of 2 Parent_key GameObjects (initially disabled). One will be randomly activated.")]
    [SerializeField] private List<GameObject> m_KeyVisualObjects = new List<GameObject>();


    private int m_CorrectSpotIndex = -1;
    private bool m_IsKeyFound = false;

    private void Awake()
    {
        InitializeSpots();
    }

    private void OnEnable()
    {
        PuzzleJigsaw.OnPuzzleCompleted += HandlePuzzleCompleted;
    }
    private void OnDisable()
    {
        PuzzleJigsaw.OnPuzzleCompleted -= HandlePuzzleCompleted;
    }

    private void InitializeSpots()
    {
        if (m_PanelKey != null)
        {
            m_PanelKey.SetActive(false);
        }

        if (m_ImageKeyButton != null)
        {
            m_ImageKeyButton.gameObject.SetActive(false);
            m_ImageKeyButton.onClick.RemoveAllListeners();
            m_ImageKeyButton.onClick.AddListener(OnImageKeyButtonClickedHandler);
        }

        if (m_SpotButtons == null || m_SpotButtons.Count == 0)
        {
            Debug.LogWarning($"{name}: m_SpotButtons list is empty!", this);
            return;
        }

        m_CorrectSpotIndex = UnityEngine.Random.Range(0, m_SpotButtons.Count);

        for (int i = 0; i < m_KeyVisualObjects.Count; i++)
        {
            if (m_KeyVisualObjects[i] != null)
            {
                m_KeyVisualObjects[i].SetActive(i == m_CorrectSpotIndex);
            }
        }

        for (int i = 0; i < m_SpotButtons.Count; i++)
        {
            Button btn = m_SpotButtons[i];
            if (btn == null) continue;

            btn.interactable = false;

            int capturedIndex = i;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnSpotClicked(capturedIndex));
        }
    }

    private void HandlePuzzleCompleted()
    {
        if (m_PanelKey != null)
        {
            m_PanelKey.SetActive(true);
        }
        foreach (var btn in m_SpotButtons)
        {
            if (btn != null)
            {
                btn.interactable = true;
            }
        }
    }

    private void OnSpotClicked(int index)
    {
        if (m_IsKeyFound) return;

        if (index == m_CorrectSpotIndex)
        {
            m_IsKeyFound = true;
            Debug.Log("The key has been successfully found!");

            if (m_ImageKeyButton != null)
            {
                m_ImageKeyButton.gameObject.SetActive(true);
            }

            foreach (var btn in m_SpotButtons)
            {
                if (btn != null) btn.interactable = false;
            }
        }
        else
        {
            Debug.Log("This spot does not contain the key.");
        }
    }
    private void OnImageKeyButtonClickedHandler()
    {
        m_ImageKeyButton.gameObject.SetActive(false);
        if (m_KeyVisualObjects != null)
        {
            foreach (var keyObj in m_KeyVisualObjects)
            {
                if (keyObj != null)
                {
                    keyObj.SetActive(false);
                }
            }
        }
    }

}